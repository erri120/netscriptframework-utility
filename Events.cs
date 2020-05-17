using NetScriptFramework;

namespace UtilityLibrary
{
    public static partial class Events
    {
        /// <summary>
        /// Event is raised when tempering Armor.
        /// </summary>
        public static Event<TemperingEventArgs> OnTempering { get; internal set; }

        public static Event<CraftingEventArgs> OnCrafting { get; internal set; }
    }
}
