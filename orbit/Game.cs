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
        /// <summary>
        /// Корабль
        /// </summary>
        private static Ship _ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(20, 10));
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        /// <summary>
        /// Счетчик очков
        /// </summary>
        public static int score = 0;
        /// <summary>
        /// Ширина экрана
        /// </summary>
        public static int Width = 800;//Screen.PrimaryScreen.Bounds.Width;
        /// <summary>
        /// Высота экрана
        /// </summary>
        public static int Height = 800;//Screen.PrimaryScreen.Bounds.Height;

        /// <summary>
        /// Таймер
        /// </summary>
        private static Timer timer = new Timer { Interval = 100 };

        /// <summary>
        /// Делегат для сообщений
        /// </summary>
        /// <param name="str">Текст сообщения</param>
        public delegate void Message(string str);
        /// <summary>
        /// Событие для Message
        /// </summary>
        public static event Message onMes;

        static Game()
        {

        }


        private static BaseObject[] _objs;
        
        //public static DrawMyImage dr = new DrawMyImage(new Point(500, 300), new Point(10, 1), @"moon.png");
        //public static DrawMyImage gal = new DrawMyImage(new Point(700, 50), new Point(4, 1), @"galaxy.png");
        private static Bullet _bullet;
        private static Asteroid[] _asteroids;
        public static Planet sun;
        public static AidKit kit;


        /// <summary>
        /// Инициализирует игровые объекты
        /// </summary>
        /// <param name="count"></param>
        public static void Load(int count)
        {
            //if ((Width > 1000) || (Width < 0)) throw new ArgumentOutOfRangeException();
            //if ((Height > 1000) || (Height < 0)) throw new ArgumentOutOfRangeException();
            if (count > 50) throw new OrbitException("Слишком много звезд");

            
            sun = new Planet(new Point(Width, 200), new Point(5, 3), new Size(10, 10));
            _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(3, 1));
            kit = new AidKit(new Point(100, 100), new Point(5, 5), new Size(6, 6));

            var rnd = new Random();

            _objs = new BaseObject[count];
            for (var i = 0; i < _objs.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _objs[i] = new Star(new Point(Game.Width, rnd.Next(0, Game.Height)), new Point(r, r), new Size(3, 3));
            }

            _asteroids = new Asteroid[10];
            for (var i = 0; i < _asteroids.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _asteroids[i] = new Asteroid(new Point(Game.Width, rnd.Next(0, Game.Height)), new Point(r / 5, r), new Size(r, r));
            }
        }
        
        /// <summary>
        /// Изменяет позиции игровых объектов. Проверяет на столкновения
        /// </summary>
        public static void Update()
        {
            foreach (BaseObject obj in _objs) obj.Update();
            _bullet?.Update();
            for (var i = 0; i < _asteroids.Length; i++)
            {
                if (_asteroids[i] == null) continue;
                _asteroids[i].Update();
                if (_bullet != null && _bullet.Collision(_asteroids[i]))
                {
                    System.Media.SystemSounds.Hand.Play();
                    score++;
                    _asteroids[i] = null;
                    _bullet = null;
                    continue;
                }
                if (!_ship.Collision(_asteroids[i])) continue;
                var rnd = new Random();
                _ship?.EnergyLow(rnd.Next(1, 10));
                System.Media.SystemSounds.Asterisk.Play();
                if (_ship.Energy <= 0) _ship?.Die();
            }

            if (_ship.Collision(kit))
                {
                _ship?.EnergyHigh(10);
                }

            kit.Update();
            sun.Update();
            //_bullet.Update();
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
        /// Описывает графику, запускает таймер и остлеживает нажатия клавиш
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
            
            timer.Start();
            timer.Tick += Timer_Tick;

            form.KeyDown += Form_KeyDown;

            Ship.MessageDie += Finish;
        }

        /// <summary>
        /// Реакция на нажатие клавиш
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            Log log = new Log();
            Game.onMes = log.Message;
            Game.onMes += log.MessageToFile;
            if (e.KeyCode == Keys.ControlKey)
            {
                _bullet = new Bullet(new Point(_ship.Rect.X + 10, _ship.Rect.Y + 4), new Point(4, 0), new Size(4, 1));
                onMes.Invoke("Нажата кнопка выстрела");
            }
            if (e.KeyCode == Keys.Up)
            {
                _ship.Up();
                onMes.Invoke("Нажата кнопка вверх");
            }
            if (e.KeyCode == Keys.Down)
            {
                _ship.Down();
                onMes.Invoke("Нажата кнопка вниз");
            }
        }

        /// <summary>
        /// Выводит объекты на экран
        /// </summary>
        public static void Draw()
        {

            Buffer.Graphics.Clear(Color.Black);
            sun.Draw(Color.Yellow);
            kit.Draw(Color.Green);
            foreach (BaseObject obj in _objs)
                obj.Draw(Color.White);
            foreach (Asteroid a in _asteroids)
            {
                a?.Draw(Color.Brown);
            }
            _bullet?.Draw(Color.Red);
            _ship?.Draw(Color.Blue);
            if (_ship != null)
                Buffer.Graphics.DrawString("Energy:" + _ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);
            Buffer.Graphics.DrawString("Score:" + score, SystemFonts.DefaultFont, Brushes.White, 0, 15);
            Buffer.Render();

        }

        /// <summary>
        /// Останавливает таймер, выводит на экран сообщение о конце игры
        /// </summary>
        public static void Finish()
        {
            timer.Stop();
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
        }

    }
}
