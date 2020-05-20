using NetScriptFramework;
using NetScriptFramework.SkyrimSE;

namespace UtilityLibrary
{
    public static partial class Events
    {
        /// <summary>
        /// Arguments for the <see cref="OnEnchanting"/> event
        /// </summary>
        public class EnchantingEventArgs : HookedEventArgs
        {
            /// <summary>
            /// The item to be enchanted
            /// </summary>
            public TESForm Item { get; internal set; }

            /// <summary>
            /// The soul gem used
            /// </summary>
            public TESSoulGem SoulGem { get; internal set; }

            /// <summary>
            /// XP gained from enchanting
            /// </summary>
            public float XP { get; set; }
        }

        internal static EventHookParameters<EnchantingEventArgs> EnchantingEventHookParameters =>
            new EventHookParameters<EnchantingEventArgs>(AddressLibrary.Enchanting, 6, 6, "FF 90 B8 07 00 00", ctx =>
            {
                var args = new EnchantingEventArgs();

                var item = MemoryObject.FromAddress<TESForm>(Memory.ReadPointer(ctx.R15));
                /*var soulGem = MemoryObject.FromAddress<TESForm>(
                    Memory.ReadPointer(Memory.ReadPointer(ctx.BX + 0x18)));
                    */
                var soulGem = MemoryObject.FromAddress<TESSoulGem>(ctx.DI);

                var xp = ctx.XMM2f;

                args.Item = item;
                args.SoulGem = soulGem;
                args.XP = xp;

                //NetScriptFramework.Main.WriteNativeCrashLog(ctx, int.MinValue, "Data\\on-enchanting-event.txt");

                return args;
            }, (ctx, args) =>
            {
                ctx.XMM2f = args.XP;
            });
    }
}
