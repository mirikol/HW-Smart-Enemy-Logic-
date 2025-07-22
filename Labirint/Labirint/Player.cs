namespace Labirint
{
    public class Player : Unit
    {
        private char[,] _map;

        public Player(int startX, int startY, ConsoleRenderer renderer, char[,] map) : base(startX, startY, '@', renderer)
        {
            _map = map;
        }

        public override void Update()
        {
            if (!Console.KeyAvailable)
                return;

            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    TryMoveUp(_map);
                    break;
                case ConsoleKey.DownArrow:
                    TryMoveDown(_map);
                    break;
                case ConsoleKey.RightArrow:
                    TryMoveRight(_map);
                    break;
                case ConsoleKey.LeftArrow:
                    TryMoveLeft(_map);
                    break;
            }

            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
        }
    }
}
