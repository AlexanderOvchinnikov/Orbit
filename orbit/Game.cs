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
        public static int Width { get; set; }
        public static int Height { get; set; }

        static Game()
        {

        }

        private static BaseObject[] _objs;
        public static Planet sun = new Planet(new Point(Width, 200), new Point(5, 3), new Size(10, 10));
        public static DrawMyImage dr = new DrawMyImage(new Point(500, 300), new Point(10, 1), @"moon.png");
        public static DrawMyImage gal = new DrawMyImage(new Point(700, 50), new Point(4, 1), @"galaxy.png");


        /// <summary>
        /// Создает массив звезд
        /// </summary>
        /// <param name="count">количество звезд</param>
        public static void LoadStar(int count)
        {
            _objs = new BaseObject[count];
            for (int i = 0; i < _objs.Length; i++)
                _objs[i] = new Star(new Point(600, i * 20), new Point(i+1, 2), new Size(3, 3));
        }

        /// <summary>
        /// Изменяет позиции объектов Star, sun, dr
        /// </summary>
        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
            sun.Update();
            dr.Update();
            gal.Update();
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

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

            LoadStar(40);
        }
        public static void Draw()
        {

            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)  obj.Draw();
            sun.Draw(Color.Yellow);
            dr.Draw();
            gal.Draw();
            Buffer.Render();

        }

        
    }
}
