using System;
using System.Linq;
using NetScriptFramework;
using NetScriptFramework.SkyrimSE;
using Main = NetScriptFramework.SkyrimSE.Main;

namespace UtilityLibrary
{
    /// <summary>
    /// Library containing utility functions
    /// </summary>
    public static partial class UtilityLibrary
    {
        /// <summary>
        /// Utility function to get the byte pattern in hex format at a given address
        /// </summary>
        /// <param name="address">The Address</param>
        /// <param name="length">Length of the pattern</param>
        /// <returns></returns>
        public static string GetPatternAtAddress(IntPtr address, int length = 8)
        {
            var result = "";

            byte[] bytes = Memory.ReadBytes(address, length);
            if (bytes == null || bytes.Length == 0 || bytes.Length != length)
                return result;

            result = string.Concat(bytes.Select(x => x.ToString("X2")));

            return result;
        }

        /// <summary>
        /// Utility function to get the byte pattern in hex format at a given address
        /// </summary>
        /// <param name="address">The Address</param>
        /// <param name="length">Length of the pattern</param>
        /// <returns></returns>
        public static string GetPattern(this IntPtr address, int length = 8)
        {
            return GetPatternAtAddress(address);
        }

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

        /// <summary>
        /// This will toggle god mode for the player, same as the <c>tgm</c> console command.
        /// </summary>
        public static void ToggleGodMode()
        {
            Memory.InvokeCdecl(AddressLibrary.ToggleGodMode);
        }

        /// <summary>
        /// This will toggle the menus, same as the <c>tm</c> console command.
        /// </summary>
        public static void ToggleMenus()
        {
            Memory.InvokeCdecl(AddressLibrary.ToggleMenus);
        }
    }
}
