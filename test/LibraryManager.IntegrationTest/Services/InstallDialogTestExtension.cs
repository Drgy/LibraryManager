using System;
using Microsoft.Test.Apex.Services;
using Microsoft.Test.Apex.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.Web.LibraryManager.Vsix.UI;

namespace Microsoft.Web.LibraryManager.IntegrationTest.Services
{
    public class InstallDialogTestExtension : VisualStudioInProcessTestExtension<object, InstallDialogVerifier>
    {
        /// <summary>
        /// InstallDialog test extension for interaction with Visual Studio inprocess types
        /// </summary>
        internal IAddClientSideLibrariesDialogTestContract InstallDialog
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
                this.InstallDialog.Library = library;
            });

            WaitForFileSelections();
        }

        private void WaitForFileSelections()
        {
            WaitFor.IsTrue(() =>
            {
                return UIInvoke(() =>
                {
                    return this.InstallDialog.IsAnyFileSelected;
                });
            }, TimeSpan.FromSeconds(30), conditionDescription: "File list not loaded");
        }

        public void ClickInstall()
        {
            UIInvoke(() =>
            {
                ThreadHelper.JoinableTaskFactory.Run(async() =>
                {
                    await this.InstallDialog.ClickInstallAsync();
                });
            });
        }
    }
}
