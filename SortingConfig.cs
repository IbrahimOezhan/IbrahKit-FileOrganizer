using System.Text.Json;
using System.Text.Json.Serialization;

namespace FileOrganizer
{
    internal class SortingConfig
    {
        public const string configName = "sort-config.json";

        public const string OTHER = "Other";
        public const string DIRECTORIES = "Directories";
        private const string IMAGES = "Images";
        private const string VIDEOS = "Videos";
        private const string AUDIO = "Audio";
        private const string DOCUMENTS = "Documents";
        private const string ARCHIVES = "Archives";
        private const string EXECUTABLES = "Executables";
        private const string CODE = "Code";

        [JsonInclude]
        private List<SortingFolder> folders = new();

        public static SortingConfig Get(string path)
        {
            if (!Path.Exists(path))
            {
                return new();
            }

            string[] files = Directory.GetFiles(path);

            string combined = Path.Combine(path, configName);

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.IncludeFields = true;
            options.WriteIndented = true;
            options.UnmappedMemberHandling = System.Text.Json.Serialization.JsonUnmappedMemberHandling.Disallow;

            if (File.Exists(combined))
            {
                try
                {
                    return JsonSerializer.Deserialize<SortingConfig>(File.ReadAllText(combined), options);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            SortingConfig config = new();

            using (StreamWriter sw = new(combined))
            {
                sw.Write(JsonSerializer.Serialize(config, options));
            }

            return config;
        }

        [JsonConstructor]
        private SortingConfig()
        {
            folders = new(PopulateSortingList());
        }

        public List<SortingFolder> GetFolders()
        {
            return folders;
        }

        private static List<SortingFolder> PopulateSortingList()
        {
            List<SortingFolder> folders = new();

            folders.Add(new(OTHER, new()));
            folders.Add(new(DIRECTORIES, new()));

            folders.Add(new(IMAGES, new()
    {
        "png", "jpg", "jpeg", "gif", "bmp", "tiff", "webp", "svg", "ico", "heic", "raw"
    }));

            folders.Add(new(VIDEOS, new()
    {
        "mp4", "mov", "avi", "mkv", "webm", "flv", "wmv", "m4v"
    }));

            folders.Add(new(AUDIO, new()
    {
        "mp3", "wav", "ogg", "flac", "aac", "wma", "m4a"
    }));

            folders.Add(new(DOCUMENTS, new()
    {
        "pdf", "doc", "docx", "xls", "xlsx", "ppt", "pptx",
        "txt", "rtf", "odt", "ods", "odp", "md", "csv"
    }));

            folders.Add(new(ARCHIVES, new()
    {
        "zip", "rar", "7z", "tar", "gz", "bz2", "xz", "iso"
    }));

            folders.Add(new(EXECUTABLES, new()
    {
        "exe", "msi", "bat", "cmd", "sh", "app", "apk"
    }));

            folders.Add(new(CODE, new()
    {
        "cs", "js", "ts", "html", "css", "cpp", "h", "c", "java", "py", "rb", "php", "go", "json", "xml", "yaml", "yml", "sql"
    }));

            return folders;
        }
    }
}
