namespace Labirint
{
    public class ConsoleInput : IMoveInput
    {
        public event Action MoveUp = delegate { };
        public event Action MoveDown = delegate { };
        public event Action MoveLeft = delegate { };
        public event Action MoveRight = delegate { };
        public event Action Esc = delegate { };

        public void Update()
        {
            if (!Console.KeyAvailable)
                return;

            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    MoveUp?.Invoke();
                    break;
                case ConsoleKey.DownArrow:
                    MoveDown?.Invoke();
                    break;
                case ConsoleKey.RightArrow:
                    MoveRight?.Invoke();
                    break;
                case ConsoleKey.LeftArrow:
                    MoveLeft?.Invoke();
                    break;
                case ConsoleKey.Escape:
                    Esc?.Invoke();
                    break;
            }

            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
        }
    }
}