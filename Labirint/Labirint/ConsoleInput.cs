namespace Labirint
{
    public class ConsoleInput : IMoveInput
    {
        public event Action MoveUp;
        public event Action MoveDown;
        public event Action MoveLeft;
        public event Action MoveRight;
        public event Action Esc;

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
