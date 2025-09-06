namespace Labirint
{
    public class VerticalObstacle : Unit
    {
        private bool _obstracleDownDir = true;

        public VerticalObstacle(Vector2 startPosition, char symbol, ConsoleRenderer renderer) :
            base(startPosition, symbol, renderer)
        {
        }

        public override void Update()
        {
            if (_obstracleDownDir)
            {
                if (!TryMoveDown())
                    _obstracleDownDir = false;
            }
            else
            {
                if (!TryMoveUp())
                    _obstracleDownDir = true;
            }
        }
        public override void Unsubscribe()
        {
            base.Unsubscribe();
        }
    }
}