using NetScriptFramework.SkyrimSE;

namespace UtilityLibrary
{
    public class Plugin : NetScriptFramework.Plugin
    {
        public override string Key => "utility.library";
        public static string PluginName => "Utility Library";
        public override string Name => PluginName;
        public override int Version => 1;

        public override string Author => "erri120";
        public override string Website => "https://github.com/erri120/netscriptframework-utility";

        public override int RequiredFrameworkVersion => 9;
        public override int RequiredLibraryVersion => 13;

        protected override bool Initialize(bool loadedAny)
        {
            Events.OnMainMenu.Register(e =>
            {
                UtilityLibrary.IsInMainMenu = e.Entering;
            });

            return true;
        }
    }
}
