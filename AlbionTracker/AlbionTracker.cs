using System;
using System.Data;
using System.IO;
using System.Reflection;
using Albion.Common.GameData;
using AlbionNetworkAnalyzer;
using AlbionTracker.Albion;
using AlbionTracker.WebServer;
using AlbionTracker.WebSocket;

namespace AlbionTracker
{
    public class AlbionTracker : IDisposable
    {
        private readonly GameData _gameData = new GameData();
        private readonly string _albionGamePath;

        public AlbionTracker(string albionGamePath = null)
        {
            if (string.IsNullOrEmpty(albionGamePath))
            {
                albionGamePath = AlbionPathFinder.TryFindAlbionLocation();
            }
            
            if (string.IsNullOrEmpty(albionGamePath))
            {
                throw new NoNullAllowedException("No game path was set");
            }

            _albionGamePath = albionGamePath;


            LoadGameData();

            // Hmm interesting, when I just use Assembly.LoadFrom(dllPath); the assembly cannot be found in GlobalAssemblyCache...
            AppDomain.CurrentDomain.AssemblyResolve += LoadAssembly;
        }

        private void LoadGameData()
        {
            _gameData.LoadAll(Path.Combine(_albionGamePath, "Albion-Online_Data/StreamingAssets/GameData"));
        }
        
        public void Start(string offlinePcapDumpPath = null)
        {
            var stateHandler = new StateHandler(_gameData);
            using (new StaticFileServer())
            using (var ws = new SocketServer(stateHandler, _gameData))
            {
                var packageHandler = new AlbionPackageHandler(stateHandler, _gameData);
                var networkAnalyzer = new NetworkAnalyzer(packageHandler);

                ws.Start();
                networkAnalyzer.Start(offlinePcapDumpPath);
            }
        }

        public void Dispose()
        {
        }

        private Assembly LoadAssembly(object sender, ResolveEventArgs args)
        {
            if (!args.Name.StartsWith("Photon3Unity3D,"))
            {
                return args.RequestingAssembly;
            }
            // After this its in GlobalAssemblyCache so I can remove the event listener
            AppDomain.CurrentDomain.AssemblyResolve -= LoadAssembly;

            var dllPath = Path.Combine(_albionGamePath, "Albion-Online_Data/Managed/Photon3Unity3D.dll");
            return Assembly.LoadFrom(dllPath);
        }
    }
}
