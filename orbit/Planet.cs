using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orbit
{
    class Planet : BaseObject
    {
        /// <summary>
        /// Конструктор Планеты
        /// </summary>
        /// <param name="pos">Стартовая позиция</param>
        /// <param name="dir">Смещение по X и Y</param>
        /// <param name="size">Размер</param>
        public Planet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {

        }

        /// <summary>
        /// Рисует эллипс заданного цвета
        /// </summary>
        /// <param name="color">Цвет</param>
        public override void Draw(Color color)
        {
            SolidBrush myBrush = new SolidBrush(color);
            Game.Buffer.Graphics.FillEllipse(myBrush, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Изменяет позицию эллипса. Если происходит смещение за экран то смещает в стартовую точку
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X + Size.Width < 0) Pos.X = Game.Width + Size.Width;
        }
    }


}
