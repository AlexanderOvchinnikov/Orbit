using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orbit
{
    class Log
    {
        /// <summary>
        /// Выводит сообщение в консоль
        /// </summary>
        /// <param name="str">Текст сообщения</param>
        public void Message(string str)
        {
            Console.WriteLine(str);
        }

        /// <summary>
        /// Записывает сообщение в файл
        /// </summary>
        /// <param name="str">Текст сообщения</param>
        internal void MessageToFile(string str)
        {
            using (var r = new System.IO.StreamWriter("log_orbit.txt", true))
            {
                r.WriteLine(str);
            }
        }
    }
}
