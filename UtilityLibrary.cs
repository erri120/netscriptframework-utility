using NetScriptFramework.SkyrimSE;

namespace UtilityLibrary
{
    public static partial class UtilityLibrary
    {
        /// <summary>
        /// Whether or not the game is in the main menu
        /// </summary>
        public static bool IsInMainMenu { get; internal set; }

        /// <summary>
        /// Whether or not the player is in game. This means the player is not
        /// in the main menu, <see cref="Main.Instance"/> is not <c>null</c> and
        /// the game is not paused.
        /// </summary>
        public static bool IsInGame => !IsInMainMenu && Main.Instance != null && !Main.Instance.IsGamePaused;
    }
}
