using NetScriptFramework;
using NetScriptFramework.SkyrimSE;

namespace UtilityLibrary
{
    public static partial class Events
    {
        public class EnchantingEventArgs : HookedEventArgs
        {
            public TESForm Item { get; internal set; }
            public TESForm SoulGem { get; internal set; }
            public float XP { get; set; }
        }

        internal static EventHookParameters<EnchantingEventArgs> EnchantingEventHookParameters =>
            new EventHookParameters<EnchantingEventArgs>(AddressLibrary.Enchanting, 6, 6, "FF 90 B8 07 00 00", ctx =>
            {
                var args = new EnchantingEventArgs();

                var item = MemoryObject.FromAddress<TESForm>(Memory.ReadPointer(ctx.R15));
                var soulGem = MemoryObject.FromAddress<TESForm>(
                    Memory.ReadPointer(Memory.ReadPointer(ctx.BX + 0x18)));
                var xp = ctx.XMM2f;

                args.Item = item;
                args.SoulGem = soulGem;
                args.XP = xp;

                return args;
            }, (ctx, args) =>
            {
                ctx.XMM2f = args.XP;
            });
    }
}
