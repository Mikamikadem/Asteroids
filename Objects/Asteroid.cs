namespace Asteroids.Objects
{
    using System;
    using System.Drawing;

    public class Asteroid : CollisionalObject
    {
        private readonly int _respawnX;
        private static readonly Random Random = new Random();

        public Asteroid(Point position, Point direction, Size size, int respawnX) : base(position, direction, size)
        {
            _respawnX = respawnX;
        }

        private static readonly Image AsteroidImage = Image.FromFile("Pictures\\asteroid2.png");

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(AsteroidImage, Position.X, Position.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            var position = Position;
            position.X += Direction.X;
            position.Y += Direction.Y;
            Position = position;

            if (Position.X < -50)
            {
                Respawn();
            }
        }

        public void Respawn()
        {
            var position = Position;
            position.X = _respawnX;
            position.Y = Random.Next(50, 750);
            Position = position;
        }
    }
}
