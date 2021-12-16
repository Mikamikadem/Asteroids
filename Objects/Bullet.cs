namespace Asteroids.Objects
{
    using System.Drawing;
    using Interfaces;

    public class Bullet : CollisionalObject
    {
        public Bullet(Point position, Point direction, Size size) : base(position, direction, size)
        {
        }

        private static readonly Image BulletImage = Image.FromFile("Pictures\\bullet.png");

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(BulletImage, Position.X, Position.Y, Size.Width, Size.Height);
        }
    }
}