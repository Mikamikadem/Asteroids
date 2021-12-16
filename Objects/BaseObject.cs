namespace Asteroids.Objects
{
    using Exception;
    using System;
    using System.Drawing;

    public abstract class BaseObject
    {
        private Point _position, _direction;
        Size _size;

        public Point Position
        {
            get => _position;
            set => _position = value;
        }

        protected Point Direction
        {
            get => _direction;
            set
            {
                if (value.X > 50 || value.Y > 50)
                {
                    throw new GameObjectException();
                }
                _direction = value;
            }
        }

        protected Size Size
        {
            get => _size;
            set
            {
                if (value.Width < 0 || value.Height < 0)
                {
                    throw new GameObjectException();
                }
                _size = value;
            }
        }
        protected BaseObject(Point position, Point direction, Size size)
        {
            Position = position;
            Direction = direction;
            Size = size;
        }
        public abstract void Draw();

        public virtual void Update()
        {
            _position.X += Direction.X;
            _position.Y += Direction.Y;
        }
    }
}
