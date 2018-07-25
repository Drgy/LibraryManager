using Microsoft.Test.Apex.VisualStudio;

namespace Microsoft.Web.LibraryManager.IntegrationTest.Services
{
    public class AddClientSideLibrariesDialogVerifier : VisualStudioInProcessTestExtensionVerifier
    {
        protected new AddClientSideLibrariesDialogTestExtension TestExtension
        {
            get
            {
                return base.TestExtension as AddClientSideLibrariesDialogTestExtension;
            }
        }
    }
}
