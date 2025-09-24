namespace Labirint
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameData data = new GameData();
            IRenderer renderer = new ConsoleRenderer();
            ConsoleInput input = new ConsoleInput();
            UnitFactory unitFactory = new UnitFactory(renderer, input);
            LevelsMenu levelsMenu = new LevelsMenu(data, input, renderer, unitFactory);

            levelsMenu.SetMenu();

            renderer.Render();

            if (LevelModel.Player != null)
            {
                LevelModel.Player.GameOver += GameOver;
            }
            while (true)
            {
                try
                {
                    input.Update();
                }
                catch (NullReferenceException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if (LevelModel.Units != null)
                {
                    var unitsCopy = LevelModel.Units.ToList();

                    foreach (Unit unit in unitsCopy)
                    {
                        unit.Update();
                    }

                    foreach (Unit unit in unitsCopy)
                    {
                        if (unit is NextLevelTrigger trigger)
                        {
                            trigger.TriggerIfNeeded();
                        }
                    }
                }

                renderer.Render();

                Thread.Sleep(400);
            }
        }
        static void GameOver()
        {
            Environment.Exit(0);
        }
    }
}