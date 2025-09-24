namespace Labirint
{
    public abstract class Unit : IDisposable
    {
        public Vector2 Position { get; protected set; }
        public Vector2 StartPosition { get; private set; }
        public string _view;

        protected IRenderer _renderer;

        public Unit(Vector2 startPosition, string view, IRenderer renderer)
        {
            StartPosition = startPosition;
            Position = startPosition;
            _view = view;
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
            char[,] map = LevelModel.GetMap();
            if (map == null || _renderer == null)
                return false;

            if (newPosition.X < 0 || newPosition.X >= map.GetLength(1) ||
                newPosition.Y < 0 || newPosition.Y >= map.GetLength(0))
                return false;

            if (map[newPosition.Y, newPosition.X] == '#')
                return false;

            _renderer.ClearPixel(Position.X, Position.Y, map);

            Position = newPosition;

            _renderer.SetCell(Position.X, Position.Y, _view);
            return true;
        }

        public void Render()
        {
            _renderer.SetCell(Position.X, Position.Y, _view);
        }

        public virtual void ResetPosition()
        {
            char[,] map = LevelModel.GetMap();
            if (map == null)
                return;

            _renderer.ClearPixel(Position.X, Position.Y, map);
            Position = StartPosition;
            _renderer.SetCell(Position.X, Position.Y, _view);
        }

        public virtual void Unsubscribe()
        {
        }
        public abstract void Update();

        public virtual void Dispose()
        {
        }
    }
}