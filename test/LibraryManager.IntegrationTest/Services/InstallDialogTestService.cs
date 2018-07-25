using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Microsoft.Test.Apex.Services;
using Microsoft.Test.Apex.VisualStudio;
using Microsoft.Test.Apex.VisualStudio.Shell;
using Microsoft.Test.Apex.VisualStudio.Shell.ToolWindows;
using Microsoft.Web.LibraryManager.Vsix.UI;

namespace Microsoft.Web.LibraryManager.IntegrationTest.Services
{
    /// <summary>
    /// Test service for add client side libraries dialog.
    /// </summary>
    [Export(typeof(InstallDialogTestService))]
    public class InstallDialogTestService : VisualStudioTestService
    {
        public AddClientSideLibrariesDialogTestExtension OpenDialog(SolutionExplorerItemTestExtension parent)
        {
            Guid guid = Guid.Parse("44ee7bda-abda-486e-a5fe-4dd3f4cefac1");
            uint commandId = 0x0100;

            parent.Select();

            Task.Factory.StartNew(() =>
            {
                this.UIInvoke(() =>
                {
                    this.CommandingService.ExecuteCommand(guid, commandId, null);
                });
            });

            return this.WaitForDialog(TimeSpan.FromSeconds(5));
        }

        /// <summary>
        /// Gets or sets the synchronization service reference.
        /// </summary>
        [Import]
        private ISynchronizationService SynchronizationService
        {
            get;
            set;
        }

        private AddClientSideLibrariesDialogTestExtension GetInstallDialogTestExtension()
        {
            IAddClientSideLibrariesDialogTestContract addClientSideLibrariesDialogTestContract = AddClientSideLibrariesDialogTestContract.window;

            if (addClientSideLibrariesDialogTestContract != null)
            {
                return this.CreateRemotableInstance<AddClientSideLibrariesDialogTestExtension>(addClientSideLibrariesDialogTestContract);
            }

            return null;
        }

        private AddClientSideLibrariesDialogTestExtension WaitForDialog(TimeSpan timeout)
        {
            AddClientSideLibrariesDialogTestExtension installDialogExtension = this.GetInstallDialogTestExtension();

            if (!AddClientSideLibrariesDialogTestContract.windowIsUp.WaitOne(TimeSpan.FromMilliseconds(timeout.TotalMilliseconds * this.SynchronizationService.TimeoutMultiplier)))
            {
                throw new TimeoutException("Add -> Client Side Libraries dialog didn't pop up");
            }

            installDialogExtension = this.GetInstallDialogTestExtension();

            return installDialogExtension;
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
