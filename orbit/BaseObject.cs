using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orbit
{
    class BaseObject
    {
        protected Point Pos; // Пара x,y для позиции
        protected Point Dir; // Пара x,y для смещения 
        protected Size Size; // Пара высота, ширина

        /// <summary>
        /// Конструктор с указанием стартовой позии и смещения
        /// </summary>
        /// <param name="pos">Пара X и Y</param>
        /// <param name="dir">Смещение по X и Y</param>
        public BaseObject(Point pos, Point dir)
        {
            Pos = pos;
            Dir = dir;
        }

        public BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }

        /// <summary>
        /// Отрисовывает эллипс (цвет, позиция X, позиция Y, ширина, высота)
        /// </summary>
        public virtual void Draw()
        {
            Game.Buffer.Graphics.DrawEllipse(Pens.Red, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Смещает стартовые точки в пределах экрана
        /// </summary>
        public virtual void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        }
    }
}
