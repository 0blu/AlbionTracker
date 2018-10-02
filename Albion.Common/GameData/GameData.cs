using Albion.Common.GameData.Characters;
using Albion.Common.GameData.Mobs;
using Albion.Common.GameData.Spells;
using Albion.Common.GameData.Tuning;
using Albion.Common.GameData.WorldInfo;
using System.IO;

namespace Albion.Common.GameData
{
    public class GameData
    {
        public MobData MobData { get; private set; }

        public SpellData SpellData { get; private set; }

        public TuningData TuningData { get; private set; }

        public CharacterData CharacterData { get; private set; }

        public WorldData WorldData { get; private set; }

        public World.World World { get; private set; }
        
        public void LoadAll(string rootPath)
        {
            LoadMobData(rootPath);
            LoadSpellData(rootPath);
            LoadTuningData(rootPath);
            LoadCharacterData(rootPath);
            LoadWorld(rootPath);
            LoadWorldData(rootPath);

            DoPostProcess();
        }

        private void LoadXmlData(string rootPath, string fileName, AlbionXmlData dataContainer)
        {
            var path = Path.GetFullPath(Path.Combine(rootPath, fileName));
            dataContainer.LoadFromXmlFile(path);
        }

        private void LoadMobData(string rootPath)
        {
            MobData = new MobData();
            LoadXmlData(rootPath, "mobs.xml", MobData);
        }

        private void LoadSpellData(string rootPath)
        {
            SpellData = new SpellData();
            LoadXmlData(rootPath, "spells.xml", SpellData);
        }

        private void LoadTuningData(string rootPath)
        {
            TuningData = new TuningData();
            LoadXmlData(rootPath, "gamedata.xml", TuningData);
        }

        private void LoadCharacterData(string rootPath)
        {
            CharacterData = new CharacterData();
            LoadXmlData(rootPath, "characters.xml", CharacterData);
        }

        private void LoadWorld(string rootPath)
        {
            World = new World.World();
            LoadXmlData(rootPath, "cluster/world.xml", World);
        }

        private void LoadWorldData(string rootPath)
        {
            WorldData = new WorldData();
            LoadXmlData(rootPath, "worldsettings.xml", WorldData);
        }

        private void DoPostProcess()
        {
            MobData.PostProcess(this);
            SpellData.PostProcess(this);
            TuningData.PostProcess(this);
            CharacterData.PostProcess(this);
            World.PostProcess(this);
            WorldData.PostProcess(this);
        }
    }
}
