using System;
using NetScriptFramework;
using NetScriptFramework.SkyrimSE;

namespace UtilityLibrary
{
    public static partial class Events
    {
        public class DisenchantingEventArgs : HookedEventArgs
        {
            public TESForm Item { get; internal set; }
            public IntPtr Enchantment { get; internal set; }
            public float EnchantmentValue { get; internal set; }
            public float XP { get; set; }
        }

        internal static EventHookParameters<DisenchantingEventArgs> DisenchantingEventHookParameters =>
            new EventHookParameters<DisenchantingEventArgs>(AddressLibrary.Disenchanting, 6, 6, "FF 90 B8 07 00 00",
                ctx =>
                {
                    var args = new DisenchantingEventArgs();

                    var item = MemoryObject.FromAddress<TESForm>(Memory.ReadPointer(ctx.R14));
                    var enchantment = ctx.SI;

                    args.Item = item;
                    args.Enchantment = enchantment;

                    var enchantmentValue = Memory.InvokeCdeclF(AddressLibrary.GetDisenchantmentValue, enchantment, 0);

                    args.EnchantmentValue = enchantmentValue;
                    args.XP = ctx.XMM2f;

                    return args;
                }, (ctx, args) =>
                {
                    ctx.XMM2f = args.XP;
                });
    }
}
