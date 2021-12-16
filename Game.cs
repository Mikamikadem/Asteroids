namespace Asteroids
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using Asteroids.Objects;
    using Exception;

    internal static class Game
    {
        #region Fields

        static BufferedGraphicsContext _context;

        public static BufferedGraphics Buffer { get; private set; }

        public static int Width { get; private set; }

        public static int Height { get; private set; }
        
        #region Images

        public static readonly Image Background = Image.FromFile("Pictures\\back2.jpg");

        private static readonly Image HpImage = Image.FromFile("Pictures\\hp.png");

        #endregion

        static readonly Timer Timer = new Timer();

        private static List<Bullet> _bullets;

        private static List<Asteroid> _asteroids;

        private static List<Star> _stars;

        private static MedicineBox _medicineBox;
        
        private static Ship _ship;

        private static int _score;

        private static int _healthPointsCounter;

        private static int _starsAmmount = 25;

        private static int _asteroidsAmmount = 18;


        #endregion

        static Game()
        {
            _score = 0;
            _healthPointsCounter = 3;
            _bullets = new List<Bullet>();
        }

        public static void Init(Form form)
        {
            // Графическое устройство для вывода графики            
            Graphics g;

            // предоставляет доступ к главному буферу графического контекста для текущего приложения
            _context = BufferedGraphicsManager.Current;

            // Создаём объект - поверхность рисования и связываем его с формой
            // Запоминаем размеры формы
            g = form.CreateGraphics();

            if (form.ClientSize.Width > 1000 || form.ClientSize.Width < 0 )
            {
                throw new ArgumentOutOfRangeException(nameof(form.ClientSize.Width));
            }

            if (form.ClientSize.Height > 1000 || form.ClientSize.Height < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(form.ClientSize.Height));
            }

            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;

            form.KeyDown += Form_KeyDown;

            // Связываем буфер в памяти с графическим объектом.
            // для того, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Timer.Interval = 100;
            Timer.Tick += Timer_Tick;
            Timer.Start();
            GenerateObjects();
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ControlKey:
                    GenerateBullet();
                    break;
                case Keys.Up:
                    _ship.Up();
                    break;
                case Keys.Down:
                    _ship.Down();
                    break;
            }
        }
        
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Update();
            Draw();

            if (_healthPointsCounter == 0)
            {
                GameOver();
            }
        }

        public static void GenerateObjects()
        {
            GenerateAsteroids();
            GenerateStars();
            GenerateShip();
            GenerateMedicineBox();
        }

        #region Generation

        private static void GenerateBullet()
        {
            _bullets.Add(new Bullet(new Point(_ship.Rect.X + 10, _ship.Rect.Y + 4), new Point(15, 0), new Size(15, 15)));
        }

        private static void GenerateMedicineBox()
        {
            var rand = new Random();
            _medicineBox = new MedicineBox(new Point(Width, rand.Next(50, Height - 50)), new Point(-10, 0), new Size(60, 40), Width);
        }

        private static void GenerateShip()
        {
            _ship = new Ship(new Point(50, Height / 2), new Point(0, 18), new Size(60, 55));
        }

        private static void GenerateStars()
        {
            var rand = new Random();
            _stars = new List<Star>();

            for (var i = 0; i < _starsAmmount; i++)
            {
                _stars.Add(new Star(new Point(rand.Next(0, Width), Height - (i - 15) * (Height / 35)),
                    new Point(rand.Next(-10, 0), 0), new Size(5, 5), Width));
            }
        }

        private static void GenerateAsteroids()
        {
            var rand = new Random();
            _asteroids = new List<Asteroid>();

            for (var i = 0; i < _asteroidsAmmount; i++)
            {
                _asteroids.Add(new Asteroid(new Point(Width, Height - i * (Height / 15)), new Point(rand.Next(-30, -5), 0),
                    new Size(30, 30), Width));
            }
        }

        #endregion

        public static void Draw()
        {
            Buffer.Graphics.DrawImage(Background, 0, 0);

            foreach (var star in _stars)
            {
                star.Draw();
            }

            foreach (var asteroid in _asteroids)
            {
                asteroid.Draw();
            }

            foreach (var bullet in _bullets)
            {
                bullet.Draw();
            }

            _ship.Draw();
            _medicineBox.Draw();

            for (var i = 0; i < _healthPointsCounter; i++)
            {
                Game.Buffer.Graphics.DrawImage(HpImage, 70 + 25 * i, 1, 20, 20);
            }

            Buffer.Graphics.DrawString("Score:" + _score, new Font(FontFamily.GenericSerif, 13), Brushes.White, 0, 0);

            Buffer.Render();
        }

        public static void Update()
        {
            UpdateAsteroids();

            foreach (var obj in _stars)
            {
                obj.Update();
            }

            _medicineBox.Update();

            if (_medicineBox.Collision(_ship))
            {
                if (_healthPointsCounter < 5)
                {
                    _healthPointsCounter++;
                }
                _medicineBox.Respawn();
            }

            foreach (var bullet in _bullets)
            {
                bullet.Update();
            }
        }

        private static void UpdateAsteroids()
        {
            foreach (var a in _asteroids)
            {
                a.Update();

                for (var i = 0; i < _bullets.Count; i++)
                {
                    if (a.Collision(_bullets[i]))
                    {
                        _score++;
                        a.Respawn();
                        _bullets.Remove(_bullets[i]);
                        i--;
                    }
                }

                if (a.Collision(_ship))
                {
                    _healthPointsCounter--;
                    a.Respawn();
                }
            }
        }

        public static void GameOver()
        {
            Timer.Stop();
            Buffer.Graphics.DrawImage(Background, 0, 0);
            Buffer.Graphics.DrawString("Game Over", new Font(FontFamily.GenericSerif, 25), Brushes.Red, (Width / 2) - 90, (Height / 2) - 70);
            Buffer.Graphics.DrawString($"Your score: {_score}", new Font(FontFamily.GenericSerif, 20), Brushes.AntiqueWhite, (Width / 2) - 90, (Height / 2) - 10);
            Buffer.Render();
        }
    }
}