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
                InstallDialog.Library = library;
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
            }, TimeSpan.FromSeconds(20), conditionDescription: "File list nopt loaded");
        }

        public void ClickInstall()
        {
            UIInvoke(async() =>
            {
                await InstallDialog.ClickInstallAsync();
            }).Wait();
        }
    }
}
