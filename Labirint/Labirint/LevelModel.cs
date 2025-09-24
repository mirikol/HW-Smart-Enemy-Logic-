namespace Labirint
{
    public class LevelModel
    {
        private static char[,] _map;
        private static Player _player;
        private static List<Unit> _units = new List<Unit>();

        public static Player Player => _player;
        public static List<Unit> Units => _units;
        public static char[,] Map => _map;

        public static void SetMap(char[,] map)
        {
            _map = map;
        }

        public static char[,] GetMap()
        {
            if (_map == null)
            {
                Console.WriteLine("Карта не установлена.");
                return null;
            }
            return _map;
        }

        public static void SetPlayer(Player player)
        {
            _player?.Dispose();
            _player = player;
        }

        public static void AddUnit(Unit unit)
        {
            _units.Add(unit);
        }

        public static void ClearUnits()
        {
            foreach (var unit in _units)
            {
                unit.Dispose();
            }
            _units.Clear();

            _player?.Dispose();
            _player = null;
        }
    }
}