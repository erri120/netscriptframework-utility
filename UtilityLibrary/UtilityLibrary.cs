using System;
using NetScriptFramework;
using NetScriptFramework.SkyrimSE;
using Main = NetScriptFramework.SkyrimSE.Main;

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
        /// in the main menu, <see cref="NetScriptFramework.SkyrimSE.Main.Instance"/> is not <c>null</c> and
        /// the game is not paused.
        /// </summary>
        public static bool IsInGame => !IsInMainMenu && Main.Instance != null && !Main.Instance.IsGamePaused;

        /// <summary>
        /// Returns the current game time.
        /// </summary>
        /// <returns></returns>
        public static long GetCurrentGameTime()
        {
            var time = (long) (Memory.InvokeCdeclF(AddressLibrary.GetCurrentGameTimeFunc) * 24.0 * 360000.0);
            return time;
        }

        /// <summary>
        /// <c>ms * 24.0 * 360000.0</c>
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public static long ToGameTime(long ms)
        {
            return (long) (ms * 24.0 * 360000.0);
        }

        /// <summary>
        /// Tries to get a <see cref="TESForm"/> from a plugin file.
        /// </summary>
        /// <typeparam name="T">Type of <see cref="TESForm"/> you want to get</typeparam>
        /// <param name="formID">Form ID as <c>0x1234</c> of the <see cref="TESForm"/></param>
        /// <param name="fileName">Name of the plugin</param>
        /// <param name="value">The resulting value if return true</param>
        /// <returns>True if successful, false if not</returns>
        public static bool TryGetFormFromFile<T>(uint formID, string fileName, out T value) where T : TESForm
        {
            value = default;

            var form = TESForm.LookupFormFromFile(formID, fileName);
            if (form == null)
                return false;

            if (!(form is T tForm)) return false;

            value = tForm;
            return true;

        }

        /// <summary>
        /// Plays the given sound
        /// </summary>
        /// <param name="sound">Pointer to the sound</param>
        public static void PlaySound(IntPtr sound)
        {
            Memory.InvokeCdecl(AddressLibrary.PlaySoundFunc, sound);
        }
    }
}
