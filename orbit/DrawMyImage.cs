using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orbit
{
    class DrawMyImage : BaseObject
    {
        private string way;

        /// <summary>
        /// Конструктор с заданием стартовой пары X и Y, смещение по X и Y, путь к картинке
        /// </summary>
        /// <param name="pos">Начальная позиция</param>
        /// <param name="dir">Смещение</param>
        /// <param name="way">Путь</param>
        public DrawMyImage(Point pos, Point dir, string way) : base(pos, dir)
        {
            this.way = way;
        }
        
        /// <summary>
        /// Вставляет картинку
        /// </summary>
        public override void Draw()
        {
            Bitmap file = new Bitmap(way);
            Game.Buffer.Graphics.DrawImage(file,Pos.X,Pos.Y);
            
        }

        /// <summary>
        /// Смещает картинку
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        }
    }


}
