namespace UtilityLibrary.Test
{
    public class Plugin : NetScriptFramework.Plugin
    {
        public override string Key => "utility.library.test";
        public static string PluginName => "Utility Library (TEST)";
        public override string Name => PluginName;
        public override int Version => 1;

        public override string Author => "erri120";
        public override string Website => "https://github.com/erri120/netscriptframework-utility";

        public override int RequiredFrameworkVersion => 9;
        public override int RequiredLibraryVersion => 13;

        protected override bool Initialize(bool loadedAny)
        {
            var utilityLibrary = NetScriptFramework.PluginManager.GetPlugin("utility.library");
            if (utilityLibrary == null) return false;
            if (!utilityLibrary.IsInitialized) return false;
            if (!loadedAny) return false;

            Events.OnCrafting.Register(e =>
            {
                Utils.Log($"Crafting an item: {e.TemperForm.CreatedItem.Name}, getting {e.XP} XP");
            });

            Events.OnTempering.Register(e =>
            {
                Utils.Log(
                    $"Tempering {e.Item.Name}, old quality: {e.OldQuality}, new quality: {e.NewQuality}, getting {e.XP} XP");
            });

            return true;
        }
    }
}
