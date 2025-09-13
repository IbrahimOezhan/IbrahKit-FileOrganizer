using System.Text.Json.Serialization;

namespace FileOrganizer
{
    internal class SortingFolder
    { 
        [JsonInclude]
        private string folder;
        [JsonInclude]
        private List<string> extensions;

        public SortingFolder(string folder, List<string> extensions)
        {
            this.folder = folder;
            this.extensions = extensions;
        }

        public string GetFolder()
        {
            return folder;
        }

        public bool CompareExtension(string extension)
        {
            bool matched = extensions.Contains(extension.Replace(".", ""));

            return matched;
        }
    }
}
