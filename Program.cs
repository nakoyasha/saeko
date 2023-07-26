using NativeFileDialogSharp;
using Sharprompt;

namespace Saeko
{
    public class Saeko {
        public static void Main()
        {
            var DirectoriesToBackup = Prompt.MultiSelect("Directories to backup:", new[] {
                "Downloads",
                "Documents",
                "Pictures",
                "Videos",
                "Music",
                "Desktop",
            }, pageSize: 3);

            Console.WriteLine("Please select the output directory");
            var OutputDirectory = Dialog.FolderPicker();

            if (OutputDirectory.IsOk)
            {
                var BackupInstance = new Backuper(OutputDirectory.Path, DirectoriesToBackup);
                Console.WriteLine($"Backup begin: {OutputDirectory.Path}");

                BackupInstance.Backup();
                Console.WriteLine($"Backup complete: {OutputDirectory.Path}");
            } else if (OutputDirectory.IsError)
            {
                Console.WriteLine($"An error has occurred while opening the output directory: {OutputDirectory.ErrorMessage}");
                Console.WriteLine("Aborting..");
            }
        }
    }

}