using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Microsoft.Test.Apex.VisualStudio;
using Microsoft.Test.Apex.VisualStudio.Shell;
using Microsoft.Web.LibraryManager.Vsix.UI;

namespace Microsoft.Web.LibraryManager.IntegrationTest.Services
{
    [Export(typeof(InstallDialogTestService))]
    public class InstallDialogTestService : VisualStudioTestService
    {
        public void OpenDialog(Guid guid, uint commandId)
        {
            Task.Factory.StartNew(() =>
            {
                this.UIInvoke(() =>
                {
                    this.CommandingService.ExecuteCommand(guid, commandId, null);
                });
            });
        }

        internal void SetFields(string library)
        {
            InstallDialogTestExtension testExtension = this.CreateRemotableInstance<InstallDialogTestExtension>();
            InstallDialog dialog = null;

            if (testExtension == null)
            {
                return;
            }

            if (testExtension != null)
            {
                testExtension.SetFields(library);
            }
        }

        /// <summary>
        /// Gets or sets the Commanding service.
        /// </summary>
        [Import(AllowDefault = true)]
        private Lazy<CommandingService> LazyCommandingService
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the Commanding service.
        /// </summary>
        private CommandingService CommandingService
        {
            get
            {
                return this.LazyCommandingService.Value;
            }
        }
    }
}
