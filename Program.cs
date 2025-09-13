using System.Diagnostics;

namespace FileOrganizer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fullProcessPath = Process.GetCurrentProcess().MainModule.FileName;

            string processFolder = Path.GetDirectoryName(fullProcessPath);

            SortingConfig config = SortingConfig.Get(processFolder);

            List<SortingFolder> folders = config.GetFolders();

            for (int i = 0; i < folders.Count; i++)
            {
                string folder = folders[i].GetFolder();

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
            }

            List<string> filesToSort = GetFilesToSort(fullProcessPath);

            Random random = new();

            for (int i = 0; i < filesToSort.Count; i++)
            {
                string newPath = FindNewFilePath(Path.GetFileName(filesToSort[i]), folders);

                string uniquePath = UniquePath(newPath, random);

                try
                {
                    File.Move(filesToSort[i], uniquePath);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                    Console.WriteLine($"FROM {filesToSort[i]} TO {newPath} AND UNIQUE {uniquePath}");
                }


            }

            List<string> foldersToSort = GetDirectoriesToSort(fullProcessPath, folders);

            for (int i = 0; i < foldersToSort.Count; i++)
            {
                string newPath = FindNewDirPath(foldersToSort[i]);

                try
                {
                    Directory.Move(foldersToSort[i], newPath);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine($"Old: {foldersToSort[i]} and new {newPath}");
                }
            }

            Console.WriteLine("Success");

            Console.ReadKey();
        }

        private static List<string> GetFilesToSort(string process)
        {
            string folder = Path.GetDirectoryName(process);

            List<string> items = Directory.GetFiles(folder).ToList();

            items.Remove(process);
            items.Remove(Path.Combine(folder, SortingConfig.configName));

            return items;
        }

        private static List<string> GetDirectoriesToSort(string process, List<SortingFolder> folders)
        {
            string folder = Path.GetDirectoryName(process);

            List<string> items = Directory.GetDirectories(folder).ToList();

            items.RemoveAll(x => folders.Select(y => y.GetFolder()).Contains(Path.GetFileName(x)));

            return items;
        }

        private static string FindNewFilePath(string fileName, List<SortingFolder> folders)
        {
            string extension = Path.GetExtension(fileName);

            for (int i = 0; i < folders.Count; i++)
            {
                if (folders[i].CompareExtension(extension))
                {
                    return Path.Combine(folders[i].GetFolder(), fileName);
                }
            }

            return Path.Combine(SortingConfig.OTHER, fileName);
        }

        private static string UniquePath(string filePath, Random random)
        {
            if (!File.Exists(filePath)) return filePath;

            string name = Path.GetFileNameWithoutExtension(filePath);

            string ext = Path.GetExtension(filePath);

            string dir = Path.GetDirectoryName(filePath);

            return Path.Combine(dir, name + random.Next(9999).ToString() + ext);
        }

        private static string FindNewDirPath(string dirPath)
        {
            string dirName = Path.GetFileName(dirPath);

            string newPath = Path.Combine(SortingConfig.DIRECTORIES, dirName);

            return newPath;
        }
    }
}
