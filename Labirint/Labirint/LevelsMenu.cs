namespace Labirint
{
    public class LevelsMenu : IDisposable
    {
        private ConsoleInput _input;
        private ConsoleRenderer _renderer;
        private GameData _gameData;

        public LevelsMenu(ConsoleInput input, ConsoleRenderer renderer, GameData gameData)
        {
            _input = input;
            _input.Esc += ShowMenu;
            _renderer = renderer;
            _gameData = gameData;
        }

        public void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Выберите уровень");
            Console.WriteLine("Level 1");
            Console.WriteLine("Level 2");

            string input = Console.ReadLine();
            SetLevel(input);
        }

        public void SetLevel(string level)
        {
            Program.ClearAllUnits();
            _gameData.ClearUnits();
            Console.Clear();
            _renderer.Clear();
            _gameData.SetMap(level);

            RenderMap();

            Player player = new Player(new Vector2(1, 1), '@', _renderer, _input);
            VerticalObstacle obstacle = new VerticalObstacle(new Vector2(2, 5), '!', _renderer);
            SmartEnemy enemy = new SmartEnemy(new Vector2(16, 8), '$', _renderer, player);

            _gameData.AddUnit(player);
            _gameData.AddUnit(enemy);
            _gameData.AddUnit(obstacle);

            Program.SetPlayer(player);
            Program.SetEnemy(enemy);
            Program.SetObstacle(obstacle);

            foreach (Unit unit in _gameData.GetUnits())
            {
                unit.Render();
            }

            _renderer.Render();
        }

        private void RenderMap()
        {
            char[,] map = _gameData.GetMap();
            if (map == null)
                return;

            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    _renderer.SetPixel(x, y, map[y, x]);
                }
            }
        }

        public void Dispose()
        {
            _input.Esc -= ShowMenu;
        }
    }
}