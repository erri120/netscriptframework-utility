using NetScriptFramework;
using NetScriptFramework.SkyrimSE;

namespace UtilityLibrary
{
    public static partial class Events
    {
        /// <summary>
        /// Arguments for the <see cref="OnApplyPoison"/> event
        /// </summary>
        public class ApplyPoisonEventArgs : HookedEventArgs
        {
            /// <summary>
            /// The weapon to be applied with poison
            /// </summary>
            public ExtraContainerChanges.ItemEntry ItemEntry { get; internal set; }

            /// <summary>
            /// The poison to be used
            /// </summary>
            public AlchemyItem Poison { get; internal set; }
        }

        internal static EventHookParameters<ApplyPoisonEventArgs> ApplyPoisonEventHookParameters =>
            new EventHookParameters<ApplyPoisonEventArgs>(AddressLibrary.ApplyPoison, 0x13, 0, "E8 72 4C B3 FF 48 85 C0",
                ctx =>
                {
                    var args = new ApplyPoisonEventArgs();
                    var item = MemoryObject.FromAddress<ExtraContainerChanges.ItemEntry>(ctx.CX);
                    args.ItemEntry = item;

                    var poison = MemoryObject.FromAddress<AlchemyItem>(ctx.BX);
                    if (poison == null)
                    {
                        poison = MemoryObject.FromAddress<AlchemyItem>(Memory.ReadPointer(ctx.SP));
                        if (poison == null)
                        {
                            poison = MemoryObject.FromAddress<AlchemyItem>(ctx.R14);
                        }
                    }

                    args.Poison = poison;

                    //NetScriptFramework.Main.WriteNativeCrashLog(ctx, int.MinValue, "Data\\on-applypoison-event.txt");

                    return args;
                }, (registers, args) => { });
    }
}
