using System;
using NetScriptFramework;
using NetScriptFramework.SkyrimSE;

namespace UtilityLibrary
{
    public static partial class Events
    {
        /// <summary>
        /// Arguments for the <see cref="OnDisenchanting"/> event
        /// </summary>
        public class DisenchantingEventArgs : HookedEventArgs
        {
            /// <summary>
            /// The item to be disenchanted
            /// </summary>
            public TESForm Item { get; internal set; }

            /// <summary>
            /// The enchantment to be learned
            /// </summary>
            public IntPtr Enchantment { get; internal set; }
            
            /// <summary>
            /// The enchantment value
            /// </summary>
            public float EnchantmentValue { get; internal set; }

            /// <summary>
            /// The XP to be gained
            /// </summary>
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
