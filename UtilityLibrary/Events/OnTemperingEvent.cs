using System;
using NetScriptFramework;
using NetScriptFramework.SkyrimSE;

namespace UtilityLibrary
{
    public partial class Events
    {
        /// <summary>
        /// Arguments for the <see cref="OnTempering"/> event
        /// </summary>
        public class TemperingEventArgs : HookedEventArgs
        {
            /// <summary>
            /// The item that gets tempered
            /// </summary>
            public TESForm Item { get; internal set; }

            /// <summary>
            /// The old quality of the item
            /// </summary>
            public float OldQuality { get; set; }

            /// <summary>
            /// The new quality of the item
            /// </summary>
            public float NewQuality { get; set; }

            /// <summary>
            /// Temper information
            /// </summary>
            public BGSConstructibleObject TemperForm { get; set; }

            /// <summary>
            /// XP gained from tempering
            /// </summary>
            public float XP { get; set; }
        }

        internal static EventHookParameters<TemperingEventArgs> TemperingEventHookParameters =>
            new EventHookParameters<TemperingEventArgs>(AddressLibrary.SmithingTempering, 6, 6, "FF 90 B8 07 00 00",
                ctx =>
                {
                    var args = new TemperingEventArgs();
                    var ptr = Memory.ReadPointer(ctx.BX);
                    if (ptr != IntPtr.Zero)
                    {
                        ptr = Memory.ReadPointer(ptr);
                        args.Item = MemoryObject.FromAddress<TESForm>(ptr);
                    }

                    args.OldQuality = Memory.ReadFloat(ctx.BX + 0x18);
                    args.NewQuality = Memory.ReadFloat(ctx.BX + 0x1C);
                    args.TemperForm = MemoryObject.FromAddress<BGSConstructibleObject>(Memory.ReadPointer(ctx.BX + 0x10));
                    args.XP = ctx.XMM2f;

                    return args;
                }, (ctx, args) =>
                {
                    Memory.WriteFloat(ctx.BX + 0x18, args.OldQuality);
                    Memory.WriteFloat(ctx.BX + 0x1C, args.NewQuality);
                    Memory.WritePointer(ctx.BX + 0x10, args.TemperForm.Cast<BGSConstructibleObject>());

                    ctx.XMM2f = args.XP;
                });
    }
}
