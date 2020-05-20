using NetScriptFramework;

namespace UtilityLibrary
{
    /// <summary>
    /// Additional Events that complement the already existing <see cref="NetScriptFramework.SkyrimSE.Events"/>
    /// </summary>
    public static partial class Events
    {
        /// <summary>
        /// Events is raised when saving
        /// </summary>
        public static Event<SaveEventArgs> OnSave { get; internal set; }

        /// <summary>
        /// Event is raised when loading
        /// </summary>
        public static Event<LoadEventArgs> OnLoad { get; internal set; }

        /// <summary>
        /// Event is raised when tempering Armor.
        /// </summary>
        public static Event<TemperingEventArgs> OnTempering { get; internal set; }

        //public static Event<CraftingEventArgs> OnCrafting { get; internal set; }

        /// <summary>
        /// Event is raised when enchanting items
        /// </summary>
        public static Event<EnchantingEventArgs> OnEnchanting { get; internal set; }

        /// <summary>
        /// Event is raised when disenchanting items
        /// </summary>
        public static Event<DisenchantingEventArgs> OnDisenchanting { get; internal set; }

        /// <summary>
        /// Event is raised when creating potions
        /// </summary>
        public static Event<AlchemyEventArgs> OnAlchemy { get; internal set; }

        /// <summary>
        /// Event is raised when applying poison to a weapon
        /// </summary>
        public static Event<ApplyPoisonEventArgs> OnApplyPoison { get; internal set; }
    }
}
