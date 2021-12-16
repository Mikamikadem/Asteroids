namespace Asteroids.Objects 
{
    using System;
    using System.Drawing;

    class MedicineBox : CollisionalObject
    {
        private static readonly Image BulletImage = Image.FromFile("Pictures\\medicineBox.png");
        private static readonly Random Random = new Random();
        private readonly int _respawnX;

        public MedicineBox(Point position, Point direction, Size size, int respawnX) : base(position, direction, size)
        {
            _respawnX = respawnX;
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(BulletImage, Position.X, Position.Y, Size.Width, Size.Height);
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
