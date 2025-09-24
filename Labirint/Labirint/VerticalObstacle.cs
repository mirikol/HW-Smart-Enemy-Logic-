namespace Labirint
{
    public class VerticalObstacle : Unit
    {
        private bool _obstracleDownDir = true;

        public VerticalObstacle(Vector2 startPosition, string view, IRenderer renderer) :
            base(startPosition, view, renderer)
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