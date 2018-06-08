using System;
using System.Collections.Generic;
using System.IO;

namespace EnadeProject.Commons.Helpers
{
    public static class FileLogger
    {
        /// <summary>
        /// Default DefaultDirectory
        /// </summary>
        private const string DefaultDirectory = @"\scripts_projeto_enade";

        /// <summary>
        /// File Name
        /// </summary>
        private const string File = "log.txt";
        
        /// <summary>
        /// Faz log no arquivo utilizando o diretório padrão <see cref="DefaultDirectory"/> (\scripts_projeto_enade) e o nome de arquivo padrão <see cref="File"/> como nome do arquivo (log.txt)
        /// </summary>
        /// <param name="content"></param>
        public static void Log(params string[] content)
        {
            LogContent(GetFullPath(GenerateFullDirectoryPath(DefaultDirectory), File), content);
        }

        public static void LogWithDefaultDirectory(string filename, params string[] content)
        {
            LogContent(GetFullPath(GenerateFullDirectoryPath(DefaultDirectory), filename), content);
        }

        public static void LogWithDefaultFilename(string directoryName, params string[] content)
        {
            LogContent(GetFullPath(GenerateFullDirectoryPath(directoryName), File), content);
        }


        private static string GenerateFullDirectoryPath(string directory)
        {
            var defaultDriveUnit = string.Empty;
            var listaDrives = new List<string>() {"C", "D", "E", "F"};
            foreach (var drive in listaDrives)
            {
                if (Directory.Exists($"{drive}:"))
                {
                    defaultDriveUnit = $"{drive}:";
                    break;
                }
            }
            return $"{defaultDriveUnit}{directory}";
        }

        private static DirectoryInfo GetDirectoryInfo(string directoryPath)
        {
            return Directory.Exists(directoryPath)
                ? new DirectoryInfo(directoryPath)
                : Directory.CreateDirectory(directoryPath);
        }

        private static string GetFullPath(string directory, string filename)
        {
            var directoryInfo = GetDirectoryInfo(directory);
            var path = $@"{directoryInfo.FullName}\{filename}";
            if (!System.IO.File.Exists(path))
            {
                System.IO.File.Create(path);
            }

            return path;
        }

        private static void LogContent(string path, params string[] content)
        {
            using (StreamWriter stream = new StreamWriter(path))
            {
                stream.WriteLine($"-----------------------------------------------------------------------");
                stream.WriteLine($"{DateTime.Now:dd-MM-yyyy HH:mm:ss}");
                foreach (var s in content)
                {
                    stream.WriteLine($"{s}");
                }
                stream.WriteLine($"-----------------------------------------------------------------------");
            }	
        }
    }
}

