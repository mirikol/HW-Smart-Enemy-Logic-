using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            int oldX = X;
            int oldY = Y;
            char oldSymbol = _map[oldY, oldX];

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

            _renderer.SetPixel(oldX, oldY, oldSymbol);
        }
    }
}
