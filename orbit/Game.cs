using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace orbit
{
    class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        /// <summary>
        /// Ширина экрана
        /// </summary>
        public static int Width = Screen.PrimaryScreen.Bounds.Width;
        /// <summary>
        /// Высота экрана
        /// </summary>
        public static int Height = Screen.PrimaryScreen.Bounds.Height;

        static Game()
        {

        }


        private static BaseObject[] _objs;
        
        //public static DrawMyImage dr = new DrawMyImage(new Point(500, 300), new Point(10, 1), @"moon.png");
        //public static DrawMyImage gal = new DrawMyImage(new Point(700, 50), new Point(4, 1), @"galaxy.png");
        private static Bullet _bullet;
        private static Asteroid[] _asteroids;
        public static Planet sun;


        /// <summary>
        /// Инициализирует игровые объекты звезды, планеты, астероиды, снаряд
        /// </summary>
        /// <param name="count">Количество звезд</param>
        /// 
        public static void Load(int count)
        {
            if ((Width > 1000) || (Width < 0)) throw new ArgumentOutOfRangeException();
            if ((Height > 1000) || (Height < 0)) throw new ArgumentOutOfRangeException();
            if (count > 50) throw new OrbitException("Слишком много звезд");

            sun = new Planet(new Point(Width, 200), new Point(5, 3), new Size(10, 10));
            _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));

            var rnd = new Random();

            _objs = new BaseObject[count];
            for (var i = 0; i < _objs.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _objs[i] = new Star(new Point(Game.Width, rnd.Next(0, Game.Height)), new Point(r, r), new Size(3, 3));
            }

            _asteroids = new Asteroid[3];
            for (var i = 0; i < _asteroids.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _asteroids[i] = new Asteroid(new Point(Game.Width, rnd.Next(0, Game.Height)), new Point(r / 5, r), new Size(r, r));
            }
        }

        /// <summary>
        /// Изменяет позиции объектов звезды, планеты, астероида, снаряда
        /// </summary>
        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
            foreach (Asteroid a in _asteroids)
            {
                a.Update();
                if (a.Collision(_bullet))
                {
                    System.Media.SystemSounds.Hand.Play();
                    var rnd = new Random();
                    _bullet.UpdatePos(0, rnd.Next(0, Game.Height));
                    a.UpdatePos(Game.Width, rnd.Next(0, Game.Height));
                }
            }

            sun.Update();
            _bullet.Update();
            //dr.Update();
            //gal.Update();
        }

        /// <summary>
        /// Вызывает методы для изменения позиции и отрисовки звезд, планет, астероидов, снаряда
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        /// <summary>
        /// Описывает графику и запускает таймер
        /// </summary>
        /// <param name="form"></param>
        public static void Init(Form form)
        {
            // Графическое устройство для вывода графики            
            Graphics g;
            // Предоставляет доступ к главному буферу графического контекста для текущего приложения
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            // Создаем объект (поверхность рисования) и связываем его с формой
            // Запоминаем размеры формы
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;

        }

        /// <summary>
        /// Выводит объекты на экран
        /// </summary>
        public static void Draw()
        {

            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)  obj.Draw(Color.White);            
            foreach (BaseObject obj in _asteroids) obj.Draw(Color.SaddleBrown);
            sun.Draw(Color.Yellow);
            _bullet.Draw(Color.Red);
            //dr.Draw();
            //gal.Draw();
            Buffer.Render();

        }

        
    }
}
