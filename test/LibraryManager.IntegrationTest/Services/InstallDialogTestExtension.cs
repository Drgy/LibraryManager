using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Test.Apex.Services;
using Microsoft.Test.Apex.VisualStudio;
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
        }

        public void WaitForFileSelections()
        {
            WaitFor.IsTrue(() =>
            {
                return UIInvoke(() =>
                {
                    return InstallDialog.IsAnyFileSelected;
                });
            }, TimeSpan.FromSeconds(20), conditionDescription: "File list not loaded");
        }

        public void ClickInstall()
        {
            Task.Run(async () =>
            {
                await UIInvoke(async () =>
                {
                    await InstallDialog.ClickInstallAsync();
                });
            }).Wait();

            return;
        }
    }
}
