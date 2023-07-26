namespace Saeko
{
    public enum BackupStatus
    {
        Success,
        Fail,
    }

    public static class DirectoryInfoExtensions
    {
        public static void DeepCopy(this DirectoryInfo directory, string destinationDir)
        {
            foreach (string dir in Directory.GetDirectories(directory.FullName, "*", SearchOption.AllDirectories))
            {
                string dirToCreate = dir.Replace(directory.FullName, destinationDir);
                Directory.CreateDirectory(dirToCreate);
            }

            foreach (string newPath in Directory.GetFiles(directory.FullName, "*.*", SearchOption.AllDirectories))
            {
                Console.WriteLine($"[Backuper] {newPath} > {destinationDir}");
                File.Copy(newPath, newPath.Replace(directory.FullName, destinationDir), true);
            }

        }
    }

    public class Backuper
    {
        private const string DefaultDirectory = "C:/SaekoBackup";
        public string OutputDirectory = DefaultDirectory;

        public IEnumerable<string> DirectoriesToBackup;

        public Backuper(string OutputDirectory = DefaultDirectory, IEnumerable<string>? DirectoriesToBackup = null)
        {
            this.OutputDirectory = OutputDirectory;
            this.DirectoriesToBackup = DirectoriesToBackup;
        }

        public BackupStatus Backup()
        {

            foreach (var DirectoryToBackup in this.DirectoriesToBackup)
            {
                Console.WriteLine($"[Backuper] Backuping up { DirectoryToBackup}"); 
                var UserProfilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

                var OldPath = $"{UserProfilePath}\\{DirectoryToBackup}";
                var NewPath = $"{OutputDirectory}\\{DirectoryToBackup}";


                Directory.CreateDirectory(NewPath);
                var sourceDir = new DirectoryInfo(OldPath);
                sourceDir.DeepCopy(NewPath);
            }

            return BackupStatus.Success;
        }

        public void SetOutputDirectory(string OutputDirectory)
        {
            this.OutputDirectory = OutputDirectory;
        }
    }
}
