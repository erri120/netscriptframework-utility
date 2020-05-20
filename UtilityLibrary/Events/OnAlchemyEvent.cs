using System.Collections.Generic;
using NetScriptFramework;
using NetScriptFramework.SkyrimSE;

namespace UtilityLibrary
{
    public static partial class Events
    {
        /// <summary>
        /// Arguments for the <see cref="OnAlchemy"/> event
        /// </summary>
        public class AlchemyEventArgs : HookedEventArgs
        {
            /// <summary>
            /// The ingredients used to create the potion
            /// </summary>
            public IReadOnlyList<TESForm> Ingredients { get; internal set; }

            /// <summary>
            /// The potion to be created
            /// </summary>
            public MagicItem Potion { get; internal set; }

            /// <summary>
            /// The XP to be gained
            /// </summary>
            public float XP { get; set; }
        }

        internal static EventHookParameters<AlchemyEventArgs> AlchemyEventHookParameters =>
            new EventHookParameters<AlchemyEventArgs>(AddressLibrary.Alchemy, 6, 6, "FF 90 B8 07 00 00", ctx =>
            {
                var args = new AlchemyEventArgs();

                var menu = ctx.R13;
                var menuArray = menu + 280;

                var ingredients = new List<TESForm>();

                using (var alloc = Memory.Allocate(0x20))
                {
                    Memory.InvokeCdecl(AddressLibrary.AlchemyFunc1, menuArray, alloc.Address);
                    Memory.InvokeCdecl(AddressLibrary.AlchemyFunc2, menuArray, alloc.Address + 0x10);
                    var v96 = Memory.ReadPointer(alloc.Address);
                    var v28 = Memory.ReadPointer(alloc.Address + 0x10);
                    while (v28 != v96)
                    {
                        var v30 = Memory.ReadPointer(Memory.ReadPointer(Memory.ReadPointer(menu + 256) + 16 * Memory.ReadInt32(v96)));

                        var item = MemoryObject.FromAddress<TESForm>(v30);
                        ingredients.Add(item);

                        v96 += 4;
                    }
                }

                var potion = MemoryObject.FromAddress<MagicItem>(Memory.ReadPointer(ctx.SP + 0x50));
                var xp = ctx.XMM2f;

                args.Ingredients = ingredients;
                args.Potion = potion;
                args.XP = xp;

                //NetScriptFramework.Main.WriteNativeCrashLog(ctx, int.MinValue, "Data\\on-alchemy-event.txt");

                return args;
            }, (ctx, args) =>
            {
                ctx.XMM2f = args.XP;
            });
    }
}
