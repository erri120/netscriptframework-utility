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
        /// Checks if the item has the given enchantment. Only works with weapons and
        /// armors.
        /// </summary>
        /// <param name="itemEntry">The Item</param>
        /// <param name="enchantment">The Enchantment</param>
        /// <returns>True if the item has the enchantment, false if not</returns>
        public static bool HasEnchantment([NotNull] this ExtraContainerChanges.ItemEntry itemEntry,
            EnchantmentItem enchantment)
        {
            var item = itemEntry.Template;
            if (item == null)
                return false;

            if (itemEntry.TryGetBaseEnchantment(out var baseEnchantment))
            {
                return enchantment.Address == baseEnchantment;
            }

            if (itemEntry.TryGetExtraEnchantment(out var extraEnchantment))
            {
                return extraEnchantment.Enchantment == enchantment;
            }

            return false;
        }

        /// <summary>
        /// Enum for Soul Gem types. Do note that
        /// <see cref="None"/> can mean multiple things
        /// as you can get <c>0</c> if something failed.
        /// <see cref="Grand"/> is also used by black soul gems.
        /// </summary>
        public enum SoulGemType : byte
        {
            None = 0,
            Petty = 1,
            Lesser = 2,
            Common = 3,
            Greater = 4,
            Grand = 5
        }

        public static byte GetSoulGemType([NotNull] this ExtraContainerChanges.ItemEntry itemEntry)
        {
            byte result = 0;

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
                return baseSoul;

            extraData.Where(x => x != null).Do(x =>
            {
                var ptr = x.Cast<BSExtraDataList>();
                if (ptr == IntPtr.Zero)
                    return;

                var soul = Memory.InvokeCdecl(AddressLibrary.GetSoulTypeFunc, ptr).ToUInt8();
                if (soul == 0)
                    soul = baseSoul;

                result = soul;
            });

            return result;
        }
    }
}
