using System;
using System.Collections.Generic;
using System.Reflection;
using Albion.Common.Math;
using Albion.Common.Network;
using Albion.Common.Network.Events;
using Albion.Common.Network.Operations.Responses;
using Albion.Common.Photon;
using Albion.Common.Time;
using AlbionNetworkAnalyzer.ContractDeserialization;
using AlbionNetworkAnalyzer.ContractDeserialization.CustomTypeBiserializers;
using AlbionNetworkAnalyzer.ContractDeserialization.CustomTypeBiserializers.System;
using AlbionNetworkAnalyzer.ContractDeserialization.CustomTypeBiserializers.System.Numbes;
using PhotonPackageParser;

namespace AlbionNetworkAnalyzer
{
    internal class AlbionContractParser : IPhotonPackageHandler
    {
        private static readonly Deserializer _deserializer = new Deserializer();
        private static readonly SimpleCustomTypeDeserializerRegistry _defaultCustomTypeRegistry = new SimpleCustomTypeDeserializerRegistry();

        private readonly IAlbionPackageHandler _handler;
        private readonly ICustomTypeDeserializerRegistry _registry = _defaultCustomTypeRegistry;

        static AlbionContractParser()
        {
            _defaultCustomTypeRegistry.RegisterCustomTypeDeserializer<Guid, GuidDeserializer>();
            _defaultCustomTypeRegistry.RegisterCustomTypeDeserializer<string, SimpleCastDeserializer<string>>();
            _defaultCustomTypeRegistry.RegisterCustomTypeDeserializer<string[], SimpleCastDeserializer<string[]>>();

            _defaultCustomTypeRegistry.RegisterCustomTypeDeserializer<bool, BooleanDeserializer>();
            _defaultCustomTypeRegistry.RegisterCustomTypeDeserializer<byte, ByteDeserializer>();
            _defaultCustomTypeRegistry.RegisterCustomTypeDeserializer<short, Int16Deserializer>();
            _defaultCustomTypeRegistry.RegisterCustomTypeDeserializer<int, Int32Deserializer>();
            _defaultCustomTypeRegistry.RegisterCustomTypeDeserializer<long, Int64Deserializer>();
            _defaultCustomTypeRegistry.RegisterCustomTypeDeserializer<float, SingleDeserializer>();
            _defaultCustomTypeRegistry.RegisterCustomTypeDeserializer<double, DoubleDeserializer>();

            //_defaultCustomTypeRegistry.RegisterCustomTypeDeserializer<byte[], NumberArray<long>>();

            _defaultCustomTypeRegistry.RegisterCustomTypeDeserializer<bool[], SimpleCastDeserializer<bool[]>>();
            _defaultCustomTypeRegistry.RegisterCustomTypeDeserializer<byte[], SimpleCastDeserializer<byte[]>>();
            _defaultCustomTypeRegistry.RegisterCustomTypeDeserializer<short[], SimpleCastDeserializer<short[]>>();
            _defaultCustomTypeRegistry.RegisterCustomTypeDeserializer<int[], SimpleCastDeserializer<int[]>>();
            _defaultCustomTypeRegistry.RegisterCustomTypeDeserializer<long[], SimpleCastDeserializer<long[]>>();
            _defaultCustomTypeRegistry.RegisterCustomTypeDeserializer<float[], SimpleCastDeserializer<float[]>>();
            _defaultCustomTypeRegistry.RegisterCustomTypeDeserializer<double[], SimpleCastDeserializer<double[]>>();

            _defaultCustomTypeRegistry.RegisterCustomTypeDeserializer<int[][], SimpleCastDeserializer<int[][]>>();

            _defaultCustomTypeRegistry.RegisterCustomTypeDeserializer<GameTimeStamp, GameTimeStampDeserializer>();
            _defaultCustomTypeRegistry.RegisterCustomTypeDeserializer<GameTimeSpan, GameTimeSpanDeserializer>();
            _defaultCustomTypeRegistry.RegisterCustomTypeDeserializer<FixPoint, FixPointDeserializer>();
            _defaultCustomTypeRegistry.RegisterCustomTypeDeserializer<FixPoint[], FixPointArrayDeserializer>();

            var commonAssembly = Assembly.GetAssembly(typeof(OperationDataContract));
            foreach (var t in commonAssembly.GetTypes())
            {
                if (!t.IsClass || t.IsAbstract || t.IsInterface)
                    continue;

                if (t.GetCustomAttribute(typeof(OperationCodeAttribute)) != null || t.GetCustomAttribute(typeof(EventCodeAttribute)) != null)
                    _deserializer.RegisterContract(t);
            }
        }

        public AlbionContractParser(IAlbionPackageHandler handler)
        {
            _handler = handler;
        }

        public void OnEvent(byte code, Dictionary<byte, object> parameters)
        {
            if (code == 2)
                return;
            if (!parameters.TryGetValue(252, out var value))
                return;

            var eventCode = (EventCodes)value;

            switch (eventCode)
            {
                case EventCodes.Leave:
                    _handler.OnLeave(_deserializer.FromDictionary<Leave>(_registry, parameters));
                    break;
                case EventCodes.HealthUpdate:
                    _handler.OnHealthUpdate(_deserializer.FromDictionary<HealthUpdate>(_registry, parameters));
                    break;
                case EventCodes.NewCharacter:
                    _handler.OnNewCharacter(_deserializer.FromDictionary<NewCharacter>(_registry, parameters));
                    break;
                case EventCodes.TakeSilver:
                    _handler.OnTakeSilver(_deserializer.FromDictionary<TakeSilver>(_registry, parameters));
                    break;
                case EventCodes.UpdateFame:
                    _handler.OnUpdateFame(_deserializer.FromDictionary<UpdateFame>(_registry, parameters));
                    break;
                case EventCodes.NewMob:
                    _handler.OnNewMob(_deserializer.FromDictionary<NewMob>(_registry, parameters));
                    break;
                case EventCodes.PartySilverGained:
                    _handler.OnPartySilverGained(_deserializer.FromDictionary<PartySilverGained>(_registry, parameters));
                    break;
                case EventCodes.InCombatStateUpdate:
                    _handler.OnInCombatStateUpdate(_deserializer.FromDictionary<InCombatStateUpdate>(_registry, parameters));
                    break;
            }
        }

        public void OnResponse(byte code, short returnCode, Dictionary<byte, object> parameters)
        {
            if (!parameters.TryGetValue(253, out var value))
                return;

            var operationCode = (OperationCodes)value;

            switch (operationCode)
            {
                case OperationCodes.Join:
                    _handler.OnJoinResponse(_deserializer.FromDictionary<JoinResponse>(_registry, parameters));
                    break;
                case OperationCodes.ChangeCluster:
                    _handler.OnChangeClusterResponse(_deserializer.FromDictionary<ChangeClusterResponse>(_registry, parameters));
                    break;
            }
        }

        public void OnRequest(byte code, Dictionary<byte, object> parameters)
        {

        }
    }
}
