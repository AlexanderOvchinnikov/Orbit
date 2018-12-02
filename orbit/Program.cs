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
            Form form = new Form();
            form.Width = 1200;
            form.Height =800;
            Game.Init(form);
            form.Show();
            Game.Draw();
            Application.Run(form);
        }
    }
}
