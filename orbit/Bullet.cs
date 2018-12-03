using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orbit
{
    class Bullet : BaseObject
    {
        /// <summary>
        /// Конструктор снаряда
        /// </summary>
        /// <param name="pos">Стартовая позиция</param>
        /// <param name="dir">Смещение по X и Y</param>
        /// <param name="size">Размер</param>
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {

        }
        /// <summary>
        /// Рисует снаряд заданного цвета
        /// </summary>
        /// <param name="color">Цвет снаряда</param>
        public override void Draw(Color color)
        {
            Pen pen = new Pen(color);
            Game.Buffer.Graphics.DrawRectangle(pen, Pos.X, Pos.Y, Size.Width, Size.Height);

        }
        /// <summary>
        /// Изменяет положение снаряда
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + 3;
        }
    }
}
