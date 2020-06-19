using NetScriptFramework;
#pragma warning disable 1591

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
            #region Function Addressess

            //MagicItem
            AddressLibrary.GetDisenchantmentValue = Main.GameInfo.GetAddressOf(11213);
            //BSExtraDataList
            AddressLibrary.GetSoulTypeFunc = Main.GameInfo.GetAddressOf(11561);
            //TESForm
            AddressLibrary.GetEnchantmentFunc = Main.GameInfo.GetAddressOf(14411);
            //ConsoleFunc::handler
            //AddressLibrary.ToggleFlyCam = Main.GameInfo.GetAddressOf(22436);
            AddressLibrary.ToggleGodMode = Main.GameInfo.GetAddressOf(22339);
            AddressLibrary.ToggleMenus = Main.GameInfo.GetAddressOf(22347);
            //papyrus::utility
            AddressLibrary.GetCurrentGameTimeFunc = Main.GameInfo.GetAddressOf(56475);
            //papyrus::Game
            AddressLibrary.GetRealHoursPassedFunc = Main.GameInfo.GetAddressOf(54842);
            AddressLibrary.QuitToMainMenu = Main.GameInfo.GetAddressOf(54857);
            //unk
            AddressLibrary.AlchemyFunc1 = Main.GameInfo.GetAddressOf(50424);
            AddressLibrary.AlchemyFunc2 = Main.GameInfo.GetAddressOf(50463);
            AddressLibrary.PlaySoundFunc = Main.GameInfo.GetAddressOf(52054);

            #endregion

            #region Event Addresses

            //event addresses
            AddressLibrary.SaveInternal = Main.GameInfo.GetAddressOf(34818, 0, 0, "44 89 44 24 18 55 56 57");
            AddressLibrary.LoadInternal = Main.GameInfo.GetAddressOf(34819, 0xA, 0, "EC60010000488BDA");
            Utils.Log(AddressLibrary.LoadInternal.GetPattern());

            AddressLibrary.SmithingTempering = Main.GameInfo.GetAddressOf(50477, 0x115, 0, "FF 90 B8 07 00 00");
            //AddressLibrary.SmithingCrafting = Main.GameInfo.GetAddressOf(50476, 0x91, 0, "FF 90 B8 07 00 00");
            AddressLibrary.Enchanting = Main.GameInfo.GetAddressOf(50450, 0x275, 0, "FF 90 B8 07 00 00");
            AddressLibrary.Disenchanting = Main.GameInfo.GetAddressOf(50459, 0xBA, 0, "FF 90 B8 07 00 00");
            AddressLibrary.Alchemy = Main.GameInfo.GetAddressOf(50449, 0x207, 0, "FF 90 B8 07 00 00");
            AddressLibrary.ApplyPoison = Main.GameInfo.GetAddressOf(39406, 0x89, 0, "E8 72 4C B3 FF 48 85 C0");

            #endregion

            //events
            Events.OnSave = new EventHook<Events.SaveEventArgs>(EventHookFlags.None, "utility-library.events.save", Events.SaveEventHookParameters);
            Events.OnLoad = new EventHook<Events.LoadEventArgs>(EventHookFlags.None, "utility-library.events.load", Events.LoadEventHookParameters);
            
            Events.OnTempering = new EventHook<Events.TemperingEventArgs>(EventHookFlags.None, "utility-library.events.tempering", Events.TemperingEventHookParameters);
            //Events.OnCrafting = new EventHook<Events.CraftingEventArgs>(EventHookFlags.None, "utility-library.events.crafting", Events.CraftingEventHookParameters);
            Events.OnEnchanting = new EventHook<Events.EnchantingEventArgs>(EventHookFlags.None, "utility-library.events.enchanting", Events.EnchantingEventHookParameters);
            Events.OnDisenchanting = new EventHook<Events.DisenchantingEventArgs>(EventHookFlags.None, "utility-library.events.disenchanting", Events.DisenchantingEventHookParameters);
            Events.OnAlchemy = new EventHook<Events.AlchemyEventArgs>(EventHookFlags.None, "utility-library.events.alchemy", Events.AlchemyEventHookParameters);
            Events.OnApplyPoison = new EventHook<Events.ApplyPoisonEventArgs>(EventHookFlags.None, "utility-library.events.applypoison", Events.ApplyPoisonEventHookParameters);

            NetScriptFramework.SkyrimSE.Events.OnMainMenu.Register(e =>
            {
                UtilityLibrary.IsInMainMenu = e.Entering;
            }, -1);

            return true;
        }
    }
}
