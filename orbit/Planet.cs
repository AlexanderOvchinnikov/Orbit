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
        public Planet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {

        }

        public void Draw(Color color)
        {
            SolidBrush myBrush = new SolidBrush(color);
            Game.Buffer.Graphics.FillEllipse(myBrush, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X + Size.Width < 0) Pos.X = Game.Width + Size.Width;
        }
    }


}
