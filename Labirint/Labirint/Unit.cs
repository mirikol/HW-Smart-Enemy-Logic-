namespace Labirint
{
    public abstract class Unit
    {
        public Vector2 Position { get; protected set; }
        public Vector2 StartPosition { get; private set; }
        public char Symbol { get; private set; }

        protected ConsoleRenderer _renderer;

        public Unit(Vector2 startPosition, char symbol, ConsoleRenderer renderer)
        {
            StartPosition = startPosition;
            Position = startPosition;
            Symbol = symbol;
            _renderer = renderer;
        }

        public virtual bool TryMoveLeft()
        {
            return TryChangePosition(new Vector2(Position.X - 1, Position.Y));
        }

        public virtual bool TryMoveRight()
        {
            return TryChangePosition(new Vector2(Position.X + 1, Position.Y));
        }

        public virtual bool TryMoveUp()
        {
            return TryChangePosition(new Vector2(Position.X, Position.Y - 1));
        }

        public virtual bool TryMoveDown()
        {
            return TryChangePosition(new Vector2(Position.X, Position.Y + 1));
        }
        protected virtual bool TryChangePosition(Vector2 newPosition)
        {
            char[,] map = GameData.GetInstance().GetMap();
            if (map == null || _renderer == null)
                return false;

            if (newPosition.X < 0 || newPosition.X >= map.GetLength(1) ||
                newPosition.Y < 0 || newPosition.Y >= map.GetLength(0))
                return false;

            if (map[newPosition.Y, newPosition.X] == '#')
                return false;

            _renderer.ClearPixel(Position.X, Position.Y, map);

            Position = newPosition;

            _renderer.SetPixel(Position.X, Position.Y, Symbol);
            return true;
        }

        public void Render()
        {
            _renderer.SetPixel(Position.X, Position.Y, Symbol);
        }

        public void ResetPosition()
        {
            char[,] map = GameData.GetInstance().GetMap();
            if (map == null)
                return;

            _renderer.ClearPixel(Position.X, Position.Y, map);
            Position = StartPosition;
            _renderer.SetPixel(Position.X, Position.Y, Symbol);
        }

        public virtual void Unsubscribe()
        {
        }
        public abstract void Update();
    }
}
