namespace Asteroids.Objects
{
    using System;
    using System.Drawing;
    using Interfaces;

    class Ship : CollisionalObject
    {
        public Ship(Point position, Point direction, Size size) : base(position, direction, size)
        {
        }

        private static readonly Image BulletImage = Image.FromFile("Pictures\\ship.png");

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(BulletImage, Position.X, Position.Y, Size.Width, Size.Height);
        }

        public void Up()
        {
            var position = Position;
            if (Position.Y > 0) position.Y = Position.Y - Direction.Y;
            Position = position;
        }

        public void Down()
        {
            var position = Position;
            if (Position.Y < Game.Height - Size.Height) position.Y = Position.Y + Direction.Y;
            Position = position;
        }
    }
}