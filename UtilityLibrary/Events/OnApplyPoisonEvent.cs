using NetScriptFramework;
using NetScriptFramework.SkyrimSE;

namespace UtilityLibrary
{
    public static partial class Events
    {
        public class ApplyPoisonEventArgs : HookedEventArgs
        {
            /// <summary>
            /// The weapon to be applied with poison
            /// </summary>
            public ExtraContainerChanges.ItemEntry ItemEntry { get; internal set; }
        }

        internal static EventHookParameters<ApplyPoisonEventArgs> ApplyPoisonEventHookParameters =>
            new EventHookParameters<ApplyPoisonEventArgs>(AddressLibrary.ApplyPoison, 0x13, 0, "E8 ? ? ? ? 48 85 C0",
                ctx =>
                {
                    var args = new ApplyPoisonEventArgs();
                    var item = MemoryObject.FromAddress<ExtraContainerChanges.ItemEntry>(ctx.CX);
                    args.ItemEntry = item;

                    return args;
                }, (registers, args) => { });
    }
}
