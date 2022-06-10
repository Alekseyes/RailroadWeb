using System;
using System.IO;


namespace RailroadWeb.Validation
{
    public static class ValidationService
    {
        public static bool ValidatePathToFile(string path, string fileExtension, out string validationMessage)
        {
            validationMessage = String.Empty;

            if(String.IsNullOrWhiteSpace(path))
            {
                validationMessage ="Wrong path";
                return false;
            }
            
            if (!File.Exists(path))
            {
                validationMessage = $"There is no file by specified path - {path}";
                return false;
            }

            if (fileExtension != Path.GetExtension(path))
            {
                validationMessage = $"Specified file has wrong extension - {path}. It should be {fileExtension}";
                return false;
            };

            return true;
        }

        public static bool ValidatePathToFile(string[] pathToFiles, string fileExtension, out string validationMessage)
        {
            validationMessage = String.Empty;

            foreach (var path in pathToFiles)
            {
                if (!ValidationService.ValidatePathToFile(path, fileExtension, out validationMessage))
                { 
                    return false;
                }
            }
            return true;
        }

        public static bool ValidateDirectory(string directoryPath, string filesExtension, out string validationMessage)
        {
            validationMessage = String.Empty;

            if (String.IsNullOrWhiteSpace(directoryPath))
            {
                validationMessage = "Wrong directory";
                return false;
            }

            if (!Directory.Exists(directoryPath))
            {
                validationMessage = $"There is no folder with the path {directoryPath}";
                return false;
            }

            var files = Directory.GetFiles(directoryPath, $"*{filesExtension}");

            if(files.Length == 0)
            {
                validationMessage = $"The folder {directoryPath} doesn't contain files with extension {filesExtension} ";
            }

            return ValidatePathToFile(files, filesExtension, out validationMessage);
        }

        public static bool ValidateConfiguration(string directoryPathToRoutes, string pathToWeb, string filesExtension, out string validationMessage)
        {
            validationMessage = String.Empty;

            return ValidatePathToFile(pathToWeb, filesExtension, out validationMessage) && ValidateDirectory(directoryPathToRoutes, filesExtension, out validationMessage);            
        }
    }
}
