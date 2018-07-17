using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Test.Apex.VisualStudio;

namespace Microsoft.Web.LibraryManager.IntegrationTest.Services
{
    public class InstallDialogVerifier : VisualStudioInProcessTestExtensionVerifier
    {
        protected new InstallDialogTestExtension TestExtension
        {
            get
            {
                return base.TestExtension as InstallDialogTestExtension;
            }
        }
    }
}
