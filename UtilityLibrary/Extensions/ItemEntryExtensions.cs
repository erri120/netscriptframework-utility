using System;
using System.Linq;
using JetBrains.Annotations;
using NetScriptFramework;
using NetScriptFramework.SkyrimSE;

namespace UtilityLibrary
{
    public static partial class UtilityLibrary
    {
        /// <summary>
        /// Tries to get the base enchantment of an item. The base enchantment is different
        /// from an extra enchantment in the way that the extra enchantment is what you apply
        /// when enchanting the item using the enchantment table and the base enchantment being
        /// the enchantment set in the actual form.
        /// <para>THIS WILL CRASH YOUR GAME IF THE ITEM DOES NOT HAVE AN BASE ENCHANTMENT!</para>
        /// </summary>
        /// <param name="itemEntry">The item</param>
        /// <param name="enchantment">The pointer to the base enchantment if return true, else <see cref="IntPtr.Zero"/></param>
        /// <returns>True if getting the base enchantment was successful, false if not</returns>
        public static bool TryGetBaseEnchantment([NotNull] this ExtraContainerChanges.ItemEntry itemEntry, out IntPtr enchantment)
        {
            enchantment = IntPtr.Zero;
            var item = itemEntry.Template;
            if (item == null)
                return false;

            var formPtr = item.Cast<TESForm>();
            if (formPtr == IntPtr.Zero)
                return false;

            var baseEnchantmentPtr = Memory.InvokeCdecl(AddressLibrary.GetEnchantmentFunc, formPtr);
            enchantment = baseEnchantmentPtr;
            return baseEnchantmentPtr != IntPtr.Zero;
        }

        /// <summary>
        /// Tries to get the extra enchantment of an item. The extra enchantment is different
        /// from a base enchantment in the way that the extra enchantment is what you apply
        /// when enchanting the item using the enchantment table and the base enchantment being
        /// the enchantment set in the actual form.
        /// </summary>
        /// <param name="itemEntry">The item</param>
        /// <param name="enchantment">The <see cref="ExtraEnchantment"/> of an item if return true</param>
        /// <returns>True if getting the extra enchantment as successful, false if not</returns>
        public static bool TryGetExtraEnchantment([NotNull] this ExtraContainerChanges.ItemEntry itemEntry,
            out ExtraEnchantment enchantment)
        {
            enchantment = null;

            var item = itemEntry.Template;
            if (item == null)
                return false;

            BSSimpleList<BSExtraDataList> extraData = itemEntry.ExtraData;
            if (extraData == null)
                return false;

            ExtraEnchantment extraEnchantment = null;

            extraData.Where(x => x != null).Do(x =>
            {
                var n = x.First;
                while (n != null)
                {
                    if (n is ExtraEnchantment nEnchantment)
                    {
                        extraEnchantment = nEnchantment;
                        break;
                    }

                    n = n.Next;
                }
            });

            enchantment = extraEnchantment;
            return extraEnchantment != null;
        }

        /// <summary>
        /// Enum for the type of soul in a soul gem.
        /// </summary>
        public enum SoulType : byte
        {
            /// <summary>
            /// None meaning no soul or something went wrong
            /// </summary>
            None = 0,
            /// <summary>
            /// Petty soul
            /// </summary>
            Petty = 1,
            /// <summary>
            /// Lesser soul
            /// </summary>
            Lesser = 2,
            /// <summary>
            /// Common soul
            /// </summary>
            Common = 3,
            /// <summary>
            /// Greater soul
            /// </summary>
            Greater = 4,
            /// <summary>
            /// Grand soul, also used in black soul gems
            /// </summary>
            Grand = 5
        }

        /// <summary>
        /// Get the type of soul in a soul gem. Will return <see cref="SoulType.None"/>
        /// if there is no soul in the soul gem.
        /// </summary>
        /// <param name="itemEntry">The soul gem</param>
        /// <returns>The soul in the given soul gem</returns>
        public static SoulType GetSoulType([NotNull] this ExtraContainerChanges.ItemEntry itemEntry)
        {
            SoulType result = 0;

            var item = itemEntry.Template;
            if (item == null)
                return result;

            var formPtr = item.Cast<TESForm>();
            if (formPtr == IntPtr.Zero)
                return result;


            if (item.FormType != FormTypes.SoulGem)
                return result;

            var baseSoul = Memory.ReadUInt8(formPtr + 0x108);

            BSSimpleList<BSExtraDataList> extraData = itemEntry.ExtraData;
            if (extraData == null)
                return (SoulType)baseSoul;

            extraData.Where(x => x != null).Do(x =>
            {
                var ptr = x.Cast<BSExtraDataList>();
                if (ptr == IntPtr.Zero)
                    return;

                var soul = Memory.InvokeCdecl(AddressLibrary.GetSoulTypeFunc, ptr).ToUInt8();
                if (soul == 0)
                    soul = baseSoul;

                result = (SoulType)soul;
            });

            return result;
        }
    }
}
