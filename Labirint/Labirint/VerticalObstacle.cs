namespace Labirint
{
    public class VerticalObstacle : Unit
    {
        private bool _obstracleDownDir = true;
        private char[,] _map;

        public VerticalObstacle(int startX, int startY, char symbol, ConsoleRenderer renderer, char[,] map) :
            base(startX, startY, symbol, renderer)
        {
            _map = map;
        }

        public override void Update()
        {
            if (_obstracleDownDir)
            {
                if (!TryMoveDown(_map))
                    _obstracleDownDir = false;
            }
            else
            {
                if (!TryMoveUp(_map))
                    _obstracleDownDir = true;
            }
        }
    }
}
