namespace Asteroids.Objects
{
    using System.Drawing;
    using Interfaces;

    public abstract class CollisionalObject : BaseObject, ICollision
    {
        public Rectangle Rect => new Rectangle(Position, Size);
        
        protected CollisionalObject(Point position, Point direction, Size size) : base(position, direction, size)
        {
        }

        public bool Collision(ICollision obj)
        {
            return obj.Rect.IntersectsWith(this.Rect);
        }
    }
}
