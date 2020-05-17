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
        /// Tries to get the <see cref="ExtraEnchantment"/> on an armor.
        /// </summary>
        /// <param name="itemEntry">The armor</param>
        /// <param name="enchantment">The <see cref="ExtraEnchantment"/> of the armor or null if returned false</param>
        /// <returns>True if getting the armor enchantment was successful, false if not</returns>
        public static bool TryGetArmorEnchantment([NotNull] this ExtraContainerChanges.ItemEntry itemEntry,
            out ExtraEnchantment enchantment)
        {
            enchantment = null;

            var item = itemEntry.Template;

            BSSimpleList<BSExtraDataList> extraData = itemEntry.ExtraData;
            if (extraData == null)
                return false;

            if (item.FormType != FormTypes.Armor) return false;

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
        /// Tries to get the enchantment of a weapon. This returns an <see cref="IntPtr"/>
        /// to the enchantment and not a <see cref="ExtraEnchantment"/>.
        /// </summary>
        /// <param name="itemEntry">The Weapon</param>
        /// <param name="enchantment">The <see cref="IntPtr"/> of the enchantment if returned true</param>
        /// <returns>True if getting the weapon enchantment was successful, false if not</returns>
        public static bool TryGetWeaponEnchantment([NotNull] this ExtraContainerChanges.ItemEntry itemEntry,
            out IntPtr enchantment)
        {
            enchantment = IntPtr.Zero;
            var item = itemEntry.Template;

            BSSimpleList<BSExtraDataList> extraData = itemEntry.ExtraData;
            if (extraData == null)
                return false;

            if (item.FormType != FormTypes.Weapon) return false;

            var enchantmentPtr = IntPtr.Zero;

            extraData.Where(x => x != null).Do(x =>
            {
                var ptr = x.Cast<BSExtraDataList>();
                if (ptr == IntPtr.Zero)
                    return;

                var formPtr = item.Cast<TESForm>();
                if (formPtr == IntPtr.Zero)
                    return;

                enchantmentPtr = Memory.InvokeCdecl(AddressLibrary.GetEnchantmentFunc, formPtr, ptr);
            });

            enchantment = enchantmentPtr;
            return enchantmentPtr != IntPtr.Zero;
        }

        /// <summary>
        /// Checks if an <see cref="ExtraContainerChanges.ItemEntry"/> is enchanted or not.
        /// Only works for items of form type <see cref="FormTypes.Armor"/> and <see cref="FormTypes.Weapon"/>.
        /// </summary>
        /// <param name="itemEntry">The Item</param>
        /// <returns><c>true</c> if enchanted, <c>false</c> if not</returns>
        public static bool IsEnchanted([NotNull] this ExtraContainerChanges.ItemEntry itemEntry)
        {
            var item = itemEntry.Template;

            if (item.FormType == FormTypes.Armor)
                return itemEntry.TryGetArmorEnchantment(out _);

            return item.FormType == FormTypes.Weapon && itemEntry.TryGetWeaponEnchantment(out _);
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

            if (item.FormType == FormTypes.Armor)
            {
                if (!itemEntry.TryGetArmorEnchantment(out var armorEnchantment))
                    return false;

                return armorEnchantment.Enchantment == enchantment;
            }

            if (item.FormType != FormTypes.Weapon) return false;

            if (!itemEntry.TryGetWeaponEnchantment(out var weaponEnchantment))
                return false;

            return weaponEnchantment == enchantment.Address;

        }
    }
}
