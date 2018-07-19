using Microsoft.Test.Apex.VisualStudio;
using Microsoft.Web.LibraryManager.Vsix.UI;

namespace Microsoft.Web.LibraryManager.IntegrationTest.Services
{
    public class InstallDialogTestExtension : VisualStudioInProcessTestExtension<object, InstallDialogVerifier>
    {
        internal IAddClientSideLibrariesDialogTestContract InstallDialog
        {
            get
            {
                return this.ObjectUnderTest as IAddClientSideLibrariesDialogTestContract;
            }
        }

        public void SetLibrary(string library)
        {
            UIInvoke(() => InstallDialog.Library = library);
        }
    }
}
