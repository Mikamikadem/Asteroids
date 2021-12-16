using System.Drawing;

namespace Asteroids.Interfaces
{
    public interface ICollision
    {
        bool Collision(ICollision obj);

        Rectangle Rect { get; }
    }
}
