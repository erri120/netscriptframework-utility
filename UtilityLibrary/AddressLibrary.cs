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

        /// <summary>
        /// Address for <see cref="Events.OnTempering"/>
        /// </summary>
        internal static IntPtr SmithingTempering { get; set; }

        /// <summary>
        /// Address for <c>Events.OnCrafting</c>
        /// </summary>
        internal static IntPtr SmithingCrafting { get; set; }

        /// <summary>
        /// Address for <see cref="Events.OnEnchanting"/>
        /// </summary>
        internal static IntPtr Enchanting { get; set; }

        /// <summary>
        /// Address for <see cref="Events.OnDisenchanting"/>
        /// </summary>
        internal static IntPtr Disenchanting { get; set; }

        /// <summary>
        /// <para>Use <see cref="Memory.InvokeCdeclF"/></para>
        ///
        /// <para>
        /// Used in <see cref="Events.OnDisenchanting"/> to get the
        /// disenchantment value. Arguments are <see cref="CPURegisters.SI"/> and <c>0</c>.
        /// </para>
        /// </summary>
        internal static IntPtr GetDisenchantmentValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        internal static IntPtr Alchemy { get; set; }
    }
}
