using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orbit
{
    class Ship : BaseObject
    {
        private int _energy = 100;
        public int Energy => _energy;

        /// <summary>
        /// Уменьшает энергию корабля на параметр n
        /// </summary>
        /// <param name="n">параметр n</param>
        public void EnergyLow(int n)
        {
            _energy -= n;
        }

        /// <summary>
        /// Увеличивает энергию корабля на парраметр n
        /// </summary>
        /// <param name="n">параметр n</param>
        public void EnergyHigh(int n)
        {
            _energy += n;
            if (_energy >= 100) _energy = 100;
        }


        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Update()
        {
        }

        /// <summary>
        /// Изменяет позицию корабля 'вверх'
        /// </summary>
        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }

        /// <summary>
        /// Изменяет позицию корабля 'вниз'
        /// </summary>
        public void Down()
        {
            if (Pos.Y < Game.Height) Pos.Y = Pos.Y + Dir.Y;
        }

        /// <summary>
        /// Запускает метод(ы) для делегата MessageDie
        /// </summary>
        public void Die()
        {
            MessageDie?.Invoke();
        }

        /// <summary>
        /// Рисует корабль заданного цвета
        /// </summary>
        /// <param name="color">Цвет корабля</param>
        public override void Draw(Color color)
        {
            SolidBrush solidBrush = new SolidBrush(color);
            Game.Buffer.Graphics.FillEllipse(solidBrush, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Событие 
        /// </summary>
        public static event Message MessageDie;

        
    }
}
