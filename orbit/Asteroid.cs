using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orbit
{
    class Asteroid : BaseObject, ICloneable, IComparable<Asteroid>

    {
        public int Power { get; set; } = 3;
        /// <summary>
        /// Конструктор для астероидов
        /// </summary>
        /// <param name="pos">Стартовая позиция</param>
        /// <param name="dir">Смещение</param>
        /// <param name="size">Размер</param>
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = 1;
        }

        public object Clone()
        {
            Asteroid asteroid = new Asteroid(new Point(Pos.X, Pos.Y), new Point(Dir.X, Dir.Y), new Size(Size.Width, Size.Height));
            // Не забываем скопировать новому астероиду Power нашего астероида
            asteroid.Power = Power;
            return asteroid;

        }

        /// <summary>
        /// Рисует астероид заданного цвета
        /// </summary>
        /// <param name="color">Цвет астероида</param>
        public override void Draw(Color color)
        {
            SolidBrush solidBrush = new SolidBrush(color);
            Game.Buffer.Graphics.FillEllipse(solidBrush, Pos.X, Pos.Y, Size.Width, Size.Height);

        }

        int IComparable<Asteroid>.CompareTo(Asteroid obj)
        {
            if (Power > obj.Power) return 1;
            else if (Power < obj.Power) return -1;
            else return 0;
        }

    

    /// <summary>
    /// Изменяет положение астероида
    /// </summary>
    public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            Pos.Y = Pos.Y - Dir.Y;
            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        }
    }
}
