using System;
using System.Threading.Tasks;
using Microsoft.Test.Apex.VisualStudio.Shell.ToolWindows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Web.LibraryManager.IntegrationTest.Services;
using Microsoft.Web.LibraryManager.Vsix.UI;

namespace Microsoft.Web.LibraryManager.IntegrationTest
{
    [TestClass]
    public class AddCientSideLibrariesFromUITests : VisualStudioLibmanHostTest
    {
        [TestMethod]
        public void UITest()
        {
            SolutionExplorerItemTestExtension projNode = SolutionExplorer.RootItem["TestProjectCore20"];

            InstallDialogTestService installDialogTestService = VisualStudio.Get<InstallDialogTestService>();
            InstallDialogTestExtension installDialogTestExtenstion = installDialogTestService.OpenDialog(projNode);

            installDialogTestExtenstion.SetLibrary("jquery@3.2.0");
            
        }
    }
}
