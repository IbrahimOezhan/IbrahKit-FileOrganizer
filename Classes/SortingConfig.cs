using System.Text.Json;
using System.Text.Json.Serialization;

namespace FileOrganizer.Classes
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

        private static readonly JsonSerializerOptions options = new JsonSerializerOptions()
        {
            IncludeFields = true,
            WriteIndented = true,
            UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow
        };

        [JsonInclude]
        private List<SortingFolder> folders = new();

        public static SortingConfig Reset(string path)
        {
            SortingConfig config = new();

            string current = GetJsonCurrent(path);

            string combined = Path.Combine(path, configName);

            using (StreamWriter sw = new(combined))
            {
                sw.Write(JsonSerializer.Serialize(config, options));
            }

            Console.WriteLine("Success");

            return config;
        }

        public static SortingConfig Get(string path)
        {
            string[] files = Directory.GetFiles(path);

            string combined = Path.Combine(path, configName);

            SortingConfig deserialized = Deserialize(path);

            if (deserialized != null) return deserialized;

            deserialized = new();

            using (StreamWriter sw = new(combined))
            {
                sw.Write(JsonSerializer.Serialize(deserialized, options));
            }

            return deserialized;
        }

        [JsonConstructor]
        private SortingConfig()
        {
            folders = new(PopulateSortingList());
        }

        public static SortingConfig Deserialize(string path)
        {
            try
            {
                SortingConfig? config = JsonSerializer.Deserialize<SortingConfig>(GetJsonCurrent(path), options);

                return config;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Excpection during deserialize attempt: " + ex.Message);

                return null;
            }
        }

        public static string GetJsonDefault()
        {
            return JsonSerializer.Serialize(new SortingConfig(), options);
        }

        public static string GetJsonCurrent(string processDir)
        {
            string configPath = Path.Combine(processDir, configName);

            string fileText = File.ReadAllText(configPath);

            return fileText;
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

        public List<SortingFolder> GetFolders()
        {
            return folders;
        }
    }
}
