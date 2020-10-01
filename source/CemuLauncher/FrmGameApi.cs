using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace CemuLauncher
{
    public partial class FrmGameApi : Form
    {
        private long gameTitleId;
        private readonly int desiredStartLocationX;
        private readonly int desiredStartLocationY;

        public FrmGameApi()
        {
            InitializeComponent();
        }

        public FrmGameApi(long gameTitleId, int x, int y) : this()
        {
            this.gameTitleId = gameTitleId;
            this.desiredStartLocationX = x;
            this.desiredStartLocationY = y;
        }

        private void FrmGameApi_Load(object sender, EventArgs e)
        {
            // set form position to pointer location 
            SetDesktopLocation(desiredStartLocationX, desiredStartLocationY);

            // load in the cemulaunchersettings.xml file and read current api value
            // read in settings.xml & parse it
            int currentGameApi = -1;    // -1 = unset
            XmlDocument doc = new XmlDocument();
            doc.Load(Properties.Resources.CemuLauncherSettingsFile);
            var nodes = doc.DocumentElement.SelectNodes("//*[local-name()='game']/*[local-name()='entry']");
            foreach (XmlNode node in nodes)
            {
                // load in the value of the current game title id
                if (long.Parse(node["title_id"].InnerText) == gameTitleId)
                {
                    currentGameApi = int.Parse(node["api"].InnerText);
                }
            }
            cboGameApi.SelectedIndex = currentGameApi;
        }

        private void FrmGameApi_FormClosing(object sender, FormClosingEventArgs e)
        {
            int api = cboGameApi.SelectedIndex;

            // save api settings to xml file 
            // handle it here instead of on the dropdown event to reduce disk writes 
            XmlDocument doc = new XmlDocument();
            doc.Load(Properties.Resources.CemuLauncherSettingsFile);
            var nodes = doc.DocumentElement.SelectNodes("//*[local-name()='game']/*[local-name()='entry']");
            foreach (XmlNode node in nodes)
            {
                // find the api node of the current title id
                if (long.Parse(node["title_id"].InnerText) == gameTitleId)
                {
                    node["api"].InnerText = api.ToString();
                }
            }
            doc.Save(Properties.Resources.CemuLauncherSettingsFile);
        }
    }
}