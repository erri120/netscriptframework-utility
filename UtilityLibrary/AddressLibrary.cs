using System;
using NetScriptFramework;
using NetScriptFramework.SkyrimSE;

namespace UtilityLibrary
{
    public static class AddressLibrary
    {
        /// <summary>
        /// <para>Use <see cref="Memory.InvokeCdecl"/></para>
        ///
        /// <para>
        /// This function will return a pointer to the current enchantment on an item.
        /// It will return <see cref="IntPtr.Zero"/> if the item does not have an enchantment.
        /// </para>
        ///
        /// <para>
        /// As Input use the the form pointer you get from <see cref="IMemoryObject.Cast{T}"/> when <c>cast</c> is called on the
        /// <see cref="ExtraContainerChanges.ItemEntry.Template"/> to get the base enchantment.
        /// </para>
        ///
        /// <para>
        /// If you want to get the extra enchantment use both the form pointer and a pointer to the current
        /// <see cref="BSExtraDataList"/> when iterating over <see cref="ExtraContainerChanges.ItemEntry.ExtraData"/>.
        /// </para>
        /// </summary>
        public static IntPtr GetEnchantmentFunc { get; internal set; }

        /// <summary>
        /// <para>Use <see cref="Memory.InvokeCdeclF"/></para>
        /// 
        /// <para>
        /// This function will return the current game time as a <see cref="long"/>. Do note
        /// that the number you will get might be extremely small so use <c>* 24.0 * 3600000.0</c>
        /// or <see cref="UtilityLibrary.ToGameTime"/>
        /// </para>
        /// </summary>
        public static IntPtr GetCurrentGameTimeFunc { get; internal set; }

        /// <summary>
        /// <para>Use <see cref="Memory.InvokeCdeclF"/></para>
        ///
        /// <para>
        /// This function will return the amount of milliseconds passed since game start.
        /// </para>
        /// </summary>
        public static IntPtr GetRealHoursPassedFunc { get; internal set; }

        /// <summary>
        /// <para>Use <see cref="Memory.InvokeCdecl"/></para>
        ///
        /// <para>
        /// This function will return a <see cref="byte"/> (use <c>ToUInt8</c>) for the
        /// type of soul in a soul gem.
        /// </para>
        ///
        /// <para>
        /// Input the pointer of the current <see cref="BSExtraDataList"/> when iterating over
        /// <see cref="ExtraContainerChanges.ItemEntry.ExtraData"/>.
        /// </para>
        ///
        /// <para>See <see cref="UtilityLibrary.SoulType"/> for all return types.</para>
        /// </summary>
        public static IntPtr GetSoulTypeFunc { get; internal set; }

        public static IntPtr SmithingTempering { get; internal set; }
        public static IntPtr SmithingCrafting { get; internal set; }
        public static IntPtr Enchanting { get; internal set; }
        public static IntPtr Disenchanting { get; internal set; }
        public static IntPtr GetDisenchantmentValue { get; internal set; }
        public static IntPtr Alchemy { get; internal set; }
    }
}
