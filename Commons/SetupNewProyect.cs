using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FLKEngine.Commons
{
    public class SetupNewProyect
    {
        public void CreateNewProyect (string DirectoryWork)
        {
            string startDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

            Console.WriteLine (startDirectory);

            string[] AllShaders = Directory.GetFiles (startDirectory + "/data/EngineLibrarys/");

            CopyFilesRecursively (startDirectory, DirectoryWork);
        }

        private static void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
            }
        }
    }
}