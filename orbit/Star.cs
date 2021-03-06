﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orbit
{
    class Star:BaseObject
    {
        /// <summary>
        /// Конструктор с указанием стартовой позии и смещения
        /// </summary>
        /// <param name="pos">Стартовая позиция(X,Y)</param>
        /// <param name="dir">Смещение(X,Y)</param>
        /// <param name="size">Размер(Ширина,Высота)</param>
        public Star(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        /// <summary>
        /// Рисует звезду заданного цвета
        /// </summary>
        /// <param name="color">Цвет</param>
        public override void Draw(Color color)
        {
            Pen pen = new Pen(color);
            Game.Buffer.Graphics.DrawLine(pen, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            Game.Buffer.Graphics.DrawLine(pen, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
        }

        /// <summary>
        /// Смещает крестик
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X - Dir.X - 1;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        }
    }
}
