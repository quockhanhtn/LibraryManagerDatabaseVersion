namespace LibraryManager.Utility.Interfaces
{
    public interface ICopyInfo
    {
        /// <summary>
        /// Copy Id đưa vào Clipboard
        /// </summary>
        System.Windows.Input.ICommand CopyIdCommand { get; set; }
        /// <summary>
        /// Copy Name đưa vào Clipboard
        /// </summary>
        System.Windows.Input.ICommand CopyNameCommand { get; set; }
    }
}
