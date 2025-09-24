namespace Labirint
{
    public class Player : Unit
    {
        public IMoveInput _input;

        public event Action GameOver;
        private Action _moveUpHandler;
        private Action _moveDownHandler;
        private Action _moveRightHandler;
        private Action _moveLeftHandler;

        public Player(Vector2 startPosition, string view, IRenderer renderer, IMoveInput input) : base(startPosition, view, renderer)
        {
            _input = input;

            _moveUpHandler = () => TryMoveUp();
            _moveDownHandler = () => TryMoveDown();
            _moveRightHandler = () => TryMoveRight();
            _moveLeftHandler = () => TryMoveLeft();

            input.MoveUp += _moveUpHandler;
            input.MoveDown += _moveDownHandler;
            input.MoveRight += _moveRightHandler;
            input.MoveLeft += _moveLeftHandler;
        }

        public override void Dispose()
        {
            _input.MoveUp -= _moveUpHandler;
            _input.MoveDown -= _moveDownHandler;
            _input.MoveRight -= _moveRightHandler;
            _input.MoveLeft -= _moveLeftHandler;

            base.Dispose();
        }

        public override void Update()
        {
            foreach (Unit unit in LevelModel.Units)
            {
                if (unit == this)
                    continue;

                if (Position.Equals(unit.Position))
                    GameOver?.Invoke();
                return;
            }
        }
    }
}