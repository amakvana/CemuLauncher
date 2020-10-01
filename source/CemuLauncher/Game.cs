using System;
using System.Collections.Generic;

namespace CemuLauncher
{
    class Game : IEquatable<Game>
    {
        public Game() { }

        public Game(long titleId, string name, int gameVersion, int dlcVersion, string path)
        {
            TitleId = titleId;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            GameVersion = gameVersion;
            DlcVersion = dlcVersion;
            Path = path ?? throw new ArgumentNullException(nameof(path));
        }

        public long TitleId { get; set; }
        public string Name { get; set; }
        public int GameVersion { get; set; }
        public int DlcVersion { get; set; }
        public string Path { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Game);
        }

        public bool Equals(Game other)
        {
            return other != null &&
                   TitleId == other.TitleId &&
                   Name == other.Name &&
                   GameVersion == other.GameVersion &&
                   DlcVersion == other.DlcVersion &&
                   Path == other.Path;
        }

        public override int GetHashCode()
        {
            var hashCode = 346366327;
            hashCode = hashCode * -1521134295 + TitleId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + GameVersion.GetHashCode();
            hashCode = hashCode * -1521134295 + DlcVersion.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Path);
            return hashCode;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
