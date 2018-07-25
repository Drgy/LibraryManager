using System;
using Microsoft.Test.Apex.Services;
using Microsoft.Test.Apex.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.Web.LibraryManager.Vsix.UI;

namespace Microsoft.Web.LibraryManager.IntegrationTest.Services
{
    public class AddClientSideLibrariesDialogTestExtension : VisualStudioInProcessTestExtension<object, AddClientSideLibrariesDialogVerifier>
    {
        /// <summary>
        /// Add client side libraries dialog test extension for interaction with visual studio inprocess types
        /// </summary>
        public IAddClientSideLibrariesDialogTestContract AddClientSideLibrariesDialog
        {
            get
            {
                return this.ObjectUnderTest as IAddClientSideLibrariesDialogTestContract;
            }
        }

        public void SetLibrary(string library)
        {
            UIInvoke(() =>
            {
                this.AddClientSideLibrariesDialog.Library = library;
            });
        }

        public void WaitForFileSelections()
        {
            WaitFor.IsTrue(() =>
            {
                return UIInvoke(() =>
                {
                    return this.AddClientSideLibrariesDialog.IsAnyFileSelected;
                });
            }, TimeSpan.FromSeconds(30), conditionDescription: "File list not loaded");
        }

        public void ClickInstall()
        {
            UIInvoke(() =>
            {
                ThreadHelper.JoinableTaskFactory.Run(async() =>
                {
                    await this.AddClientSideLibrariesDialog.ClickInstallAsync();
                });
            });
        }
    }
}
