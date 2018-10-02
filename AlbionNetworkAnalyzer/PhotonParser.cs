using PcapDotNet.Core;
using PcapDotNet.Packets;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Transport;
using PhotonPackageParser;
using System;

namespace AlbionNetworkAnalyzer
{
    internal class PhotonParser
    {
        private const int GameServerClientPort = 5056;
        private readonly PhotonPackageParser.PhotonPackageParser _photonPackageParser;

        public PhotonParser(IPhotonPackageHandler handler)
        {
            _photonPackageParser = new PhotonPackageParser.PhotonPackageParser(handler);
        }

        public void Start(string offlinePcapDumpPath = null)
        {
            // Ask user or use OfflinePacketDevice 
            var device = string.IsNullOrEmpty(offlinePcapDumpPath) ? PacketDeviceSelector.AskForPacketDevice() : new OfflinePacketDevice(offlinePcapDumpPath);

            using (PacketCommunicator communicator = device.Open(65536, PacketDeviceOpenAttributes.Promiscuous, 1000))
            {
                communicator.ReceivePackets(0, PacketHandler);
            }

            Console.WriteLine("END OF PacketDevice");
        }

        private void PacketHandler(Packet packet)
        {
            IpV4Datagram ip = packet.Ethernet.IpV4;
            UdpDatagram udp = ip.Udp;

            if (udp == null || (udp.SourcePort != GameServerClientPort && udp.DestinationPort != GameServerClientPort))
            {
                return;
            }

            System.Threading.Thread.Sleep(10);
            _photonPackageParser.DeserializeMessageAndCallback(udp);
        }
    }
}
