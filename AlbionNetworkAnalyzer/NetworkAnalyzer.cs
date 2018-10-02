namespace AlbionNetworkAnalyzer
{
    public class NetworkAnalyzer
    {
        private readonly PhotonParser _photonParser;

        public NetworkAnalyzer(IAlbionPackageHandler handler)
        {
            var albionContractParser = new AlbionContractParser(handler);
            _photonParser = new PhotonParser(albionContractParser);
        }

        public void Start(string offlinePcapDumpPath = null)
        {
            _photonParser.Start(offlinePcapDumpPath);
        }
    }
}
