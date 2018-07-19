using System.Threading;

namespace Microsoft.Web.LibraryManager.Vsix.UI
{
    public class AddClientSideLibrariesDialogTestContract
    {
        public static IAddClientSideLibrariesDialogTestContract window;
        public static EventWaitHandle windowIsUp = new EventWaitHandle(false, EventResetMode.ManualReset);
    }
}
