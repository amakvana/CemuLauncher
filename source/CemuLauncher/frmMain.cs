using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace CemuLauncher
{
    public partial class FrmMain : Form
    {
        private const int GamesListItemMargin = 5;   // size of each lstGames item
        private const string CemuSettingsPath = "settings.xml";
        private const string CemuExePath = "cemu.exe";
        private readonly List<Game> games = new List<Game>();

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // check for updates
            // if latest version, continue loading
            // if not, prompt user to download & exit
            try
            {
                string onlineVersion = "";
                using (var client = new WebClient())
                using (Stream stream = client.OpenRead("https://raw.githubusercontent.com/amakvana/CemuLauncher/master/version"))
                using (StreamReader reader = new StreamReader(stream))
                {
                    onlineVersion = reader.ReadToEnd();
                }
                if (Application.ProductVersion != onlineVersion)
                {
                    MessageBox.Show("New version of CemuLauncher available, please download from https://github.com/amakvana/CemuLauncher", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Process.Start("https://github.com/amakvana/CemuLauncher");
                    Application.Exit();
                }
            }
            catch
            {
                MessageBox.Show("New version of CemuLauncher available, please download from https://github.com/amakvana/CemuLauncher", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Process.Start("https://github.com/amakvana/CemuLauncher");
                Application.Exit();
            }

            // check if Cemu settings have been created.
            // If not, tell user to configure cemu then close this app 
            if (!File.Exists(CemuSettingsPath))
            {
                MessageBox.Show("Cemu settings.xml file not found, please configure Cemu before running the Cemu Launcher", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            } 
            else
            {
                // read in settings.xml & parse it
                XmlDocument doc = new XmlDocument();
                doc.Load(CemuSettingsPath);
                int cemuUserSetApi = int.Parse(doc.DocumentElement.SelectSingleNode(CemuSettingsNodes.DefaultApi).InnerText);
                var gameEntryNodes = doc.DocumentElement.SelectNodes(CemuSettingsNodes.DefaultGameEntries);
                foreach (XmlNode node in gameEntryNodes)
                {
                    Game g = new Game
                    {
                        Name = node["name"].InnerText,
                        TitleId = long.Parse(node["title_id"].InnerText),
                        GameVersion = int.Parse(node["version"].InnerText),
                        DlcVersion = int.Parse(node["dlc_version"].InnerText),
                        Path = node["path"].InnerText
                    };

                    games.Add(g);
                    lstGames.Items.Add($"{g.Name}\nVersion: {g.GameVersion}, DLC: {g.DlcVersion}");
                }

                // check if cemulaunchersettings.xml exists
                // if not then create it & populate it with the title_id's & default api set inside cemu 
                if (!File.Exists(Properties.Resources.CemuLauncherSettingsFile))
                {
                    using (var sw = new StreamWriter(Properties.Resources.CemuLauncherSettingsFile, true))
                    {
                        var sb = new StringBuilder();
                        sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                        sb.AppendLine("<game>");
                        foreach (Game g in games)
                        {
                            sb.AppendLine("<entry>");
                            sb.AppendLine($"<title_id>{g.TitleId}</title_id>");
                            sb.AppendLine($"<api>{cemuUserSetApi}</api>");
                            sb.AppendLine("</entry>");
                        }
                        sb.Append("</game>");
                        sw.Write(sb.ToString());
                    }
                }

                // setup UI defaults 
                fullscreenToolStripMenuItem.Enabled = true;
                fullscreenToolStripMenuItem.Checked = bool.Parse(doc.DocumentElement.SelectSingleNode(CemuSettingsNodes.DefaultFullScreen).InnerText);
                playToolStripMenuItem.Enabled = true;
                pauseToolStripMenuItem.Enabled = false;
                stopToolStripMenuItem.Enabled = false;
                lstGames.Enabled = true;
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            KillCemu();
        }

        private void EditGameAPIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new FrmGameApi(games[lstGames.SelectedIndex].TitleId, Cursor.Position.X, Cursor.Position.Y))
            {
                f.ShowDialog(this);
            }
        }

        private void LstGames_MouseDown(object sender, MouseEventArgs e)
        {
            lstGames.SelectedIndex = lstGames.IndexFromPoint(e.X, e.Y);
        }

        private void LstGames_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            string txt = lstGames.Items[e.Index].ToString();
            SizeF txtSize = e.Graphics.MeasureString(txt, this.Font);

            // set the required size
            e.ItemHeight = (int)txtSize.Height + 2 * GamesListItemMargin;
            e.ItemWidth = (int)txtSize.Width;
        }

        private void LstGames_DrawItem(object sender, DrawItemEventArgs e)
        {
            // get item
            if (e.Index != -1)
            {
                string txt = lstGames.Items[e.Index].ToString();

                // draw the item
                e.DrawBackground();
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    e.Graphics.DrawString(txt, this.Font, SystemBrushes.HighlightText, e.Bounds.Left, e.Bounds.Top + GamesListItemMargin);
                }
                else
                {
                    using (SolidBrush br = new SolidBrush(e.ForeColor))
                    {
                        e.Graphics.DrawString(txt, this.Font, br, e.Bounds.Left, e.Bounds.Top + GamesListItemMargin);
                    }
                }
                e.DrawFocusRectangle();
            }
        }

        private void LstGames_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // run the game as long as its selected 
            if (lstGames.SelectedIndex != -1)
            {
                RunGame(lstGames.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Please select a game to run", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // run game as long as it's selected 
            if (lstGames.SelectedIndex != -1)
            {
                RunGame(lstGames.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Please select a game to run", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (pauseToolStripMenuItem.Text)
            {
                case "Pause":
                    ProcessHandler.SuspendProcess("Cemu");
                    pauseToolStripMenuItem.Text = "Resume";
                    break;

                case "Resume":
                    ProcessHandler.ResumeProcess("Cemu");
                    pauseToolStripMenuItem.Text = "Pause";
                    break;
            }
        }

        private void StopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KillCemu();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new FrmAbout())
            {
                f.ShowDialog();
            }
        }
        
        private void CemuWebsiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://cemu.info/");
        }

        private void GitHubRepositoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/amakvana/CemuLauncher");
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            // if cemu is not running, reset UI defaults
            // this is to handle if user closes Cemu without going to Emulation > Stop
            if (Process.GetProcessesByName("Cemu").Length == 0)
            {
                playToolStripMenuItem.Enabled = true;
                pauseToolStripMenuItem.Enabled = false;
                stopToolStripMenuItem.Enabled = false;
                fullscreenToolStripMenuItem.Enabled = true;
                lstGames.Enabled = true;
            }
        }

        private void KillCemu()
        {
            // end cemu tasks if running 
            var processes = Process.GetProcessesByName("Cemu");
            if (processes.Length > 0)
            {
                foreach (Process p in processes)
                {
                    p.Kill();
                }
            }
        }

        private void RunGame(int index)
        {
            int currentGameApi = -1;    // -1 = unset
            Game selectedGame = games[index];
            long selectedGameTitleId = selectedGame.TitleId;

            // read in the game api value set for currently selected game
            XmlDocument doc = new XmlDocument();
            doc.Load(Properties.Resources.CemuLauncherSettingsFile);
            var nodes = doc.DocumentElement.SelectNodes("//*[local-name()='game']/*[local-name()='entry']");
            foreach (XmlNode node in nodes)
            {
                // load in the value of the current game title id
                if (long.Parse(node["title_id"].InnerText) == selectedGameTitleId)
                {
                    currentGameApi = int.Parse(node["api"].InnerText);
                }
            }

            // amend settings.xml with api value 
            doc.Load(CemuSettingsPath);
            doc.DocumentElement.SelectSingleNode(CemuSettingsNodes.DefaultApi).InnerText = currentGameApi.ToString();
            doc.Save(CemuSettingsPath);

            // execute cemu with correct params 
            using (var p = new Process())
            {
                p.StartInfo.FileName = CemuExePath;
                p.StartInfo.Arguments = $"-g \"{selectedGame.Path}\"";
                p.Start();
            }

            // change UI  
            playToolStripMenuItem.Enabled = false;
            pauseToolStripMenuItem.Enabled = true;
            stopToolStripMenuItem.Enabled = true;
            fullscreenToolStripMenuItem.Enabled = false;
            lstGames.Enabled = false;
        }

        private void FullscreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // alter fullscreen value depending if option is checked 
            XmlDocument doc = new XmlDocument();
            doc.Load(CemuSettingsPath);
            doc.DocumentElement.SelectSingleNode(CemuSettingsNodes.DefaultFullScreen).InnerText = fullscreenToolStripMenuItem.Checked ? "true" : "false";
            doc.Save(CemuSettingsPath);
        }
    }
}