namespace CemuLauncher
{
    public static class CemuSettingsNodes
    {
        public const string DefaultApi = "//*[local-name()='content']/*[local-name()='Graphic']/*[local-name()='api']";
        public const string DefaultGameEntries = "//*[local-name()='content']/*[local-name()='GameCache']/*[local-name()='Entry']";
        public const string DefaultFullScreen = "//*[local-name()='content']/*[local-name()='fullscreen']";
    }
}
