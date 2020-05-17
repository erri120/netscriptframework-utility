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
            AddressLibrary.GetEnchantmentFunc = NetScriptFramework.Main.GameInfo.GetAddressOf(14411);
            AddressLibrary.GetCurrentGameTimeFunc = NetScriptFramework.Main.GameInfo.GetAddressOf(56475);
            AddressLibrary.GetRealHoursPassedFunc = NetScriptFramework.Main.GameInfo.GetAddressOf(54842);
            AddressLibrary.GetSoulTypeFunc = NetScriptFramework.Main.GameInfo.GetAddressOf(11561);

            Events.OnMainMenu.Register(e =>
            {
                UtilityLibrary.IsInMainMenu = e.Entering;
            }, -1);

            return true;
        }
    }
}
