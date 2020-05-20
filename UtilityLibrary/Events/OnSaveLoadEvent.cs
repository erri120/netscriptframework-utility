using NetScriptFramework;
using NetScriptFramework.SkyrimSE;

namespace UtilityLibrary
{
    public static partial class Events
    {
        public class SaveEventArgs : HookedEventArgs
        {
            //public string SaveFile { get; internal set; }

            /// <summary>
            /// The acting <see cref="BGSSaveLoadManager"/>.
            /// </summary>
            public BGSSaveLoadManager SaveLoadManager { get; internal set; }
        }

        internal static EventHookParameters<SaveEventArgs> SaveEventHookParameters => new EventHookParameters<SaveEventArgs>(AddressLibrary.SaveInternal, 5, 5, "44 89 44 24 18 55 56 57",
            ctx =>
            {
                var args = new SaveEventArgs();

                var saveLoadManager = MemoryObject.FromAddress<BGSSaveLoadManager>(ctx.CX);
                args.SaveLoadManager = saveLoadManager;

                //Main.WriteNativeCrashLog(ctx, int.MinValue, "Data\\on-save-event.txt");
                return args;
            }, (ctx, args) =>
            {

            });
    }
}
