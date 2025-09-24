namespace Labirint
{
    public class UnitConfig
    {
        public Vector2 Position;
        public string View;
        public UnitType Type;
        public UnitConfig(Vector2 pos, string view, UnitType type)
        {
            Position = pos;
            View = view;
            Type = type;
        }
    }
}