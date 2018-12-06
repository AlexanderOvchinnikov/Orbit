using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orbit
{
    class AidKit : BaseObject,  IComparable<AidKit>
    {
        public int Power { get; set; } = 10;
        private Random rnd;
        /// <summary>
        /// Конструктор аптечки
        /// </summary>
        /// <param name="pos">Стартовая позиция</param>
        /// <param name="dir">Смещение</param>
        /// <param name="size">Размер</param>
        public AidKit(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = 10;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        int IComparable<AidKit>.CompareTo(AidKit obj)
        {
            if (Power > obj.Power) return 1;
            else if (Power < obj.Power) return -1;
            else return 0;
        }

        /// <summary>
        /// Рисует аптечку заданного цвета
        /// </summary>
        /// <param name="color">Цвет</param>
        public override void Draw(Color color)
        {
            Pen pen = new Pen(color);
            Game.Buffer.Graphics.DrawLine(pen, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y );
            Game.Buffer.Graphics.DrawLine(pen, Pos.X + Size.Width/2, Pos.Y - Size.Height, Pos.X + Size.Width / 2, Pos.Y + Size.Height);
        }

        /// <summary>
        /// Изменяет координаты аптечки
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            rnd = new Random();
            if (Pos.X < 0)
            {
                Pos.Y = rnd.Next(10, (Game.Height - 10));
                Pos.X = Pos.X + Game.Width;    
            };
        }
    }
}
