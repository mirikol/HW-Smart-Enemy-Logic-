namespace Labirint
{
    public abstract class Unit

    {
        public int X { get; private set; }
        public int Y { get; private set; }
        private char _symbol;
        protected ConsoleRenderer _renderer;

        public Unit(int startX, int startY, char symbol, ConsoleRenderer renderer)
        {
            X = startX;
            Y = startY;
            _symbol = symbol;
            _renderer = renderer;

            _renderer.SetPixel(X, Y, _symbol);
        }

        public virtual bool TryMoveLeft(char[,] map)
        {
            return TryChangePosition(X - 1, Y, map);
        }

        public virtual bool TryMoveRight(char[,] map)
        {
            return TryChangePosition(X + 1, Y, map);
        }

        public virtual bool TryMoveUp(char[,] map)
        {
            return TryChangePosition(X, Y - 1, map);
        }

        public virtual bool TryMoveDown(char[,] map)
        {
            return TryChangePosition(X, Y + 1, map);
        }
        protected virtual bool TryChangePosition(int newX, int newY, char[,] map)
        {
            if (map == null || _renderer == null)
            {
                return false;
            }
            if (newX < 0 || newX >= map.GetLength(1) || newY < 0 || newY >= map.GetLength(0))
                return false;

            if (map[newY, newX] == '#')
                return false;

            _renderer.SetPixel(X, Y, map[Y, X]);

            X = newX;
            Y = newY;

            _renderer.SetPixel(X, Y, _symbol);
            return true;
        }

        public abstract void Update();
    }
}
