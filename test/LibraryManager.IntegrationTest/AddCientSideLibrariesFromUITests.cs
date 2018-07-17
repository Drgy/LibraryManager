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
            projNode.Select();
            Guid guid = Guid.Parse("44ee7bda-abda-486e-a5fe-4dd3f4cefac1");
            uint commandId = 0x0100;

            InstallDialogTestService installDialogTestService = VisualStudio.Get<InstallDialogTestService>();
            installDialogTestService.OpenDialog(guid, commandId);

            installDialogTestService.SetFields("jquery@3.2.0");
        }
    }
}
