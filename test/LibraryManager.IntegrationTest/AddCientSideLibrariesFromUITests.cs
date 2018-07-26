using System.IO;
using Microsoft.Test.Apex.VisualStudio.Shell.ToolWindows;
using Microsoft.Test.Apex.VisualStudio.Solution;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Web.LibraryManager.IntegrationTest.Services;

namespace Microsoft.Web.LibraryManager.IntegrationTest
{
    [TestClass]
    public class AddCientSideLibrariesFromUITests : VisualStudioLibmanHostTest
    {
        ProjectTestExtension _webProject;
        ProjectItemTestExtension _libManConfig;
        const string _projectName = @"TestProjectCore20";
        const string _libman = "libman.json";
        private string _initialLibmanFileContent;
        private string _pathToLibmanFile;

        [TestInitialize]
        public void Initialize()
        {
            _webProject = Solution[_projectName];
            _libManConfig = _webProject[_libman];
            _pathToLibmanFile = Path.Combine(SolutionRootPath, _projectName, _libman);
            _initialLibmanFileContent = File.ReadAllText(_pathToLibmanFile);

            if (File.Exists(_libManConfig.FullPath))
            {
                string projectPath = Path.Combine(SolutionRootPath, _projectName);
                _libManConfig.Delete();
                Helpers.FileIO.WaitForDeletedFile(projectPath, Path.Combine(projectPath, _libman), caseInsensitive: false);
            }
        }

        [TestMethod]
        public void InstallClientSideLibraries_FromProjectRoot()
        {
            SolutionExplorerItemTestExtension projectNode = SolutionExplorer.RootItem[_projectName];
            InstallDialogTestService installDialogTestService = VisualStudio.Get<InstallDialogTestService>();
            InstallDialogTestExtension installDialogTestExtenstion = installDialogTestService.OpenDialog(projectNode);

            installDialogTestExtenstion.SetLibrary("jquery-validate@1.17.0");
            installDialogTestExtenstion.WaitForFileSelections();
            installDialogTestExtenstion.ClickInstall();

            string pathToLibrary = Path.Combine(SolutionRootPath, _projectName, "wwwroot", "lib", "jquery-validate");
            string[] expectedFiles = new[]
            {
                Path.Combine(pathToLibrary, "jquery.validate.js"),
                Path.Combine(pathToLibrary, "localization", "messages_ar.js"),
            };

            string manifestContents = @"{
  ""version"": ""1.0"",
  ""defaultProvider"": ""cdnjs"",
  ""libraries"": [
    {
      ""library"": ""jquery-validate@1.17.0"",
      ""destination"": ""wwwroot/lib/jquery-validate/""
    }
  ]
}";
            Helpers.FileIO.WaitForRestoredFiles(pathToLibrary, expectedFiles, caseInsensitive: true, timeout: 20000);
            Assert.AreEqual(manifestContents, File.ReadAllText(_pathToLibmanFile));
        }

        [TestCleanup]
        public void Cleanup()
        {
            _libManConfig = _webProject[_libman];

            if (_libManConfig != null)
            {
                _libManConfig.Open();

                Editor.Selection.SelectAll();
                Editor.KeyboardCommands.Backspace();
                Editor.Edit.InsertTextInBuffer(_initialLibmanFileContent);

                _libManConfig.Save();
            }
        }
    }
}
