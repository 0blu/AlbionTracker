using System.Diagnostics;
using System.IO;

namespace AlbionTracker
{
    public static class AlbionPathFinder
    {
        private const string AlbionOnlineExe = "Albion-Online.exe";
        private const string DefaultPath = "Program Files (x86)/AlbionOnline/game/";

        public static string TryFindAlbionLocation()
        {
            string path = null;
            var processes = Process.GetProcessesByName(AlbionOnlineExe);

            if (processes.Length > 0)
            {
                path = processes[0].StartInfo.WorkingDirectory;
            }
            // TODO After no running process can be found, try to search the registry
            else if (File.Exists(Path.Combine("C:/", DefaultPath, AlbionOnlineExe)))
            {
                path = Path.Combine("C:/", DefaultPath);
            }
            else if (File.Exists(Path.Combine("D:/", DefaultPath, AlbionOnlineExe)))
            {
                path = Path.Combine("D:/", DefaultPath);
            }

            return path;
        }
    }
}
