﻿namespace Labirint
{
    public interface IMoveInput
    {
        public event Action MoveUp;
        public event Action MoveDown;
        public event Action MoveLeft;
        public event Action MoveRight;
    }
}