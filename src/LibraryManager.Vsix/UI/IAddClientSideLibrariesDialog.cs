namespace Microsoft.Web.LibraryManager.Vsix.UI
{
    public interface IAddClientSideLibrariesDialogTestContract
    {
        void SetLibrary(string library);
        string Library { get; set; }

        void ClickInstall();
    }
}
