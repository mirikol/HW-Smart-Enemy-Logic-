namespace Labirint
{
    public interface IRenderer
    {
        public void SetCell(int x, int y, string val);

        public void Render();

        void ClearPixel(int x, int y, char[,] map);

        public void Clear();
    }
}