using NetScriptFramework;

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
            AddressLibrary.GetEnchantmentFunc = Main.GameInfo.GetAddressOf(14411);
            AddressLibrary.GetCurrentGameTimeFunc = Main.GameInfo.GetAddressOf(56475);
            AddressLibrary.GetRealHoursPassedFunc = Main.GameInfo.GetAddressOf(54842);
            AddressLibrary.GetSoulTypeFunc = Main.GameInfo.GetAddressOf(11561);

            AddressLibrary.SmithingTempering = Main.GameInfo.GetAddressOf(50477, 0x115, 6);
            AddressLibrary.SmithingCrafting = Main.GameInfo.GetAddressOf(50476, 0x91, 6);
            AddressLibrary.Enchanting = Main.GameInfo.GetAddressOf(50450, 0x275, 6);
            AddressLibrary.Disenchanting = Main.GameInfo.GetAddressOf(50459, 0xBA, 6);
            AddressLibrary.Alchemy = Main.GameInfo.GetAddressOf(50449, 0x207, 6);

            Events.OnTempering = new EventHook<Events.TemperingEventArgs>(EventHookFlags.None, "utility-library.events.tempering", Events.TemperingEventHookParameters);
            //Events.OnCrafting = new EventHook<Events.CraftingEventArgs>(EventHookFlags.None, "utility-library.events.crafting", Events.CraftingEventHookParameters);
            Events.OnEnchanting = new EventHook<Events.EnchantingEventArgs>(EventHookFlags.None, "utility-library.events.enchanting", Events.EnchantingEventHookParameters);

            NetScriptFramework.SkyrimSE.Events.OnMainMenu.Register(e =>
            {
                UtilityLibrary.IsInMainMenu = e.Entering;
            }, -1);

            return true;
        }
    }
}
