using Microsoft.Test.Apex.VisualStudio;
using Microsoft.Web.LibraryManager.Vsix.UI;

namespace Microsoft.Web.LibraryManager.IntegrationTest.Services
{
    public class InstallDialogTestExtension : VisualStudioInProcessTestExtension<object, InstallDialogVerifier>
    {
        internal InstallDialog InstallDialog
        {
            get
            {
                return this.ObjectUnderTest as InstallDialog;
            }
        }

        internal void SetFields(string library)
        {
            InstallDialog.SetLibrary(library);
        }
    }
}
