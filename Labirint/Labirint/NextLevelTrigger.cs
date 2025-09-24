namespace Labirint
{
    public class NextLevelTrigger : Unit
    {
        public event Action OnNextLevel = delegate { };
        private bool _shouldTriggerNextLevel = false;

        public NextLevelTrigger(Vector2 position, string view, IRenderer renderer) : base(position, view, renderer)
        {
        }

        public override void Update()
        {
            if (_shouldTriggerNextLevel) return;

            if (LevelModel.Player != null &&
                LevelModel.Player.Position.X == Position.X &&
                LevelModel.Player.Position.Y == Position.Y)
            {
                _shouldTriggerNextLevel = true;
            }
        }

        public void TriggerIfNeeded()
        {
            if (_shouldTriggerNextLevel)
            {
                OnNextLevel();
                _shouldTriggerNextLevel = false;
            }
        }

        public override void Unsubscribe()
        {
            OnNextLevel = delegate { };
            base.Unsubscribe();
        }

        public override void Dispose()
        {
            Unsubscribe();
            base.Dispose();
        }
    }
}