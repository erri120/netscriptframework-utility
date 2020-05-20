using NetScriptFramework;
using NetScriptFramework.SkyrimSE;
using Main = NetScriptFramework.Main;

namespace UtilityLibrary.Example
{
    public class Plugin : NetScriptFramework.Plugin
    {
        public override string Key => "utility.library.example";
        public static string PluginName => "Utility Library (EXAMPLE)";
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

            Events.OnTempering.Register(e =>
            {
                Utils.Log($"Tempering {e.Item.Name}, old quality: {e.OldQuality}, new quality: {e.NewQuality}, getting {e.XP} XP");
            });

            Events.OnEnchanting.Register(e =>
            {
                Utils.Log($"Enchanting {e.Item.Name}, soul gem: {e.SoulGem.Name}, xp: {e.XP}");
            });

            Events.OnDisenchanting.Register(e =>
            {
                Utils.Log(
                    $"Disenchanting {e.Item.Name}, enchantment: {e.Enchantment.Name}, value: {e.EnchantmentValue} xp: {e.XP}");
            });

            Events.OnAlchemy.Register(e =>
            {
                Utils.Log($"Creating potion: {e.Potion.Name} with ingredients:");
                for (var i = 0; i < e.Ingredients.Count; i++)
                {
                    Utils.Log($"Ingredient {i}: {e.Ingredients[i].Name}");
                }
            });

            Events.OnApplyPoison.Register(e =>
            {
                Utils.Log($"Item: {e.ItemEntry.Template?.Name}, Poison: {e.Poison.Name}");
                UtilityLibrary.PlaySound(Main.GameInfo.GetAddressOf(269252));
            });

            /*Events.OnCrafting.Register(e =>
            {
                Utils.Log($"Crafting an item: {e.TemperForm.CreatedItem.Name}, getting {e.XP} XP");
            });*/

            return true;
        }
    }
}
