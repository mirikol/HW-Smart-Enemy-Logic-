namespace Labirint
{
    public class LevelsMenu
    {
        private GameData _gameData;
        private ConsoleInput _input;
        private IRenderer _renderer;
        private UnitFactory _unitFactory;
        private string _currentLevel;
        private List<NextLevelTrigger> _currentTriggers = new List<NextLevelTrigger>();

        public LevelsMenu(GameData gameData, ConsoleInput input, IRenderer renderer, UnitFactory unitFactory)
        {
            _gameData = gameData;
            _input = input;
            _renderer = renderer;
            _unitFactory = unitFactory;

            _input.Esc += SetMenu;
        }

        public void SetMenu()
        {
            _renderer.Clear();
            Console.WriteLine("Выберите уровень");
            foreach (var levelMap in _gameData.LevelMaps)
            {
                Console.WriteLine(levelMap.Key);
            }
            string input = Console.ReadLine();

            if (_gameData.LevelMaps.ContainsKey(input))
                SetLevel(input);
            else
                SetMenu();
        }

        public void SetLevel(string level)
        {
            _currentLevel = level;
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            _renderer.Clear();

            LevelModel.ClearUnits();
            LevelModel.SetMap(_gameData.LevelMaps[level]);
            SetMapPixels(_gameData.LevelMaps[level]);

            foreach (var trigger in _currentTriggers)
            {
                trigger.OnNextLevel -= GoToNextLevel;
                trigger.Unsubscribe();
            }

            _currentTriggers.Clear();

            if (_gameData.LevelUnits.TryGetValue(level, out var unitConfigs))
            {
                SetUnits(unitConfigs);
            }
            else
            {
                Console.WriteLine($"Для уровня {level} нет юнитов.");
            }

            if (_gameData.LevelEnemies.TryGetValue(level, out var enemyConfigs))
            {
                SetUnits(enemyConfigs);
            }

            if (_gameData.LevelNextLevelTriggers.TryGetValue(level, out var triggerConfigs))
            {
                foreach (var config in triggerConfigs)
                {
                    var trigger = new NextLevelTrigger(config.Position, config.View, _renderer);
                    trigger.OnNextLevel += GoToNextLevel;
                    LevelModel.AddUnit(trigger);
                    _currentTriggers.Add(trigger);
                }
            }

            foreach (var unit in LevelModel.Units)
            {
                unit.Render();
            }
            LevelModel.Player?.Render();

            _renderer.Render();
            Thread.Sleep(50);
        }

        private void GoToNextLevel()
        {
            string nextLevel = GetNextLevel(_currentLevel);

            if (nextLevel != null)
            {
                SetLevel(nextLevel);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Поздравляем! Вы прошли все уровни!");
                Environment.Exit(0);
            }
        }

        private string GetNextLevel(string currentLevel)
        {
            var levels = _gameData.LevelMaps.Keys.ToList();
            int currrentIndex = levels.IndexOf(currentLevel);

            if (currrentIndex < levels.Count - 1)
            {
                return levels[currrentIndex + 1];
            }
            return null;
        }

        public void SetMapPixels(char[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    _renderer.SetCell(j, i, map[i, j].ToString());
                }
            }
        }

        public void SetUnits(List<UnitConfig> units)
        {
            foreach (var unitConfig in units)
            {
                _unitFactory.CreateUnit(unitConfig);
            }
        }
    }
}