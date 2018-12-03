using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace orbit
{
    static class Program
    {
     
        static void Main()
        {
            Form form = new Form
            {
                Width = Game.Width,
                Height = Game.Height
            }; 
            Game.Init(form);
            form.Show();
            Game.Load(30);
            Game.Draw();
            Application.Run(form);
        }
    }
}
