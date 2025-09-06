namespace Labirint
{
    public class GameData
    {
        private static GameData _instance;
        private static char[,] _map;

        private List<Unit> _units = new List<Unit>();
        private Dictionary<string, char[,]> _levelMaps;

        private GameData() { }

        public static GameData GetInstance()
        {
            if (_instance == null)
                _instance = new GameData();

            return _instance;
        }

        public void SetLevelMaps(Dictionary<string, char[,]> levelMaps)
        {
            _levelMaps = levelMaps;
        }

        public void SetMap(string levelName)
        {
            if (_levelMaps != null && _levelMaps.ContainsKey(levelName))
            {
                _map = _levelMaps[levelName];
            }
        }

        public char[,] GetMap()
        {
            if (_map == null)
            {
                throw new InvalidOperationException("Карта не установленна. Вызовите SetMap() перед использованием.");
            }
            return _map;
        }

        public List<Unit> GetUnits()
        {
            return _units;
        }

        public void AddUnit(Unit unit)
        {
            _units.Add(unit);
        }

        public void ClearUnits()
        {
            _units.Clear();
        }
    }
}