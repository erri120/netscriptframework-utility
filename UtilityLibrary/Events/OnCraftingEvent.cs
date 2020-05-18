using NetScriptFramework;
using NetScriptFramework.SkyrimSE;

namespace UtilityLibrary
{
    public static partial class Events
    {
        public class CraftingEventArgs : HookedEventArgs
        {
            /// <summary>
            /// The item to be constructed
            /// </summary>
            public BGSConstructibleObject TemperForm { get; set; }

            /// <summary>
            /// The XP to be gained
            /// </summary>
            public float XP { get; set; }
        }

        internal static EventHookParameters<CraftingEventArgs> CraftingEventHookParameters =>
            new EventHookParameters<CraftingEventArgs>(AddressLibrary.SmithingCrafting, 6, 6, "FF 90 B8 07 00 00",
                ctx =>
                {
                    var args = new CraftingEventArgs();
                    var temperForm = MemoryObject.FromAddress<BGSConstructibleObject>(ctx.SI);

                    args.TemperForm = temperForm;
                    args.XP = ctx.XMM2f;

                    return args;
                }, (ctx, args) =>
                {
                    Memory.WritePointer(ctx.BX + 0x10, args.TemperForm.Cast<BGSConstructibleObject>());
                    ctx.XMM2f = args.XP;
                });
    }
}
