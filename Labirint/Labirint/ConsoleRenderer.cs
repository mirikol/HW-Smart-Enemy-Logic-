namespace Labirint
{
    public class ConsoleRenderer
    {
        private char[,] _pixels;
        private char[,] _priviousPixels;
        private int _width;
        private int _height;

        public ConsoleRenderer()
        {
            _width = Console.WindowWidth;
            _height = Console.WindowHeight;
            _pixels = new char[_width, _height];
            _priviousPixels = new char[_width, _height];
            Console.CursorVisible = false;
        }

        public void SetPixel(int x, int y, char val)
        {
            if (x >= 0 && x < _width && y >= 0 && y < _height)
            {
                if (val == '#' || _pixels[x, y] != '#')
                {
                    _pixels[x, y] = val;
                }
            }
        }

        public void Render()
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    if (_priviousPixels[x, y] == _pixels[x, y])
                        continue;

                    Console.SetCursorPosition(x, y);
                    Console.Write(_pixels[x, y]);
                    _priviousPixels[x, y] = _pixels[x, y];
                }
            }
        }

        public void ClearPixel(int x, int y, char[,] map)
        {
            if (x >= 0 && x < _width && y >= 0 && y < _height)
            {
                _pixels[x, y] = map[y, x];
            }
        }
    }
}
