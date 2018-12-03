using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orbit
{
    public abstract class BaseObject : ICollision
    {
   
        protected Point Pos; // Пара x,y для позиции
        protected Point Dir; // Пара x,y для смещения 
        protected Size Size; // Пара высота, ширина

        /// <summary>
        /// Конструктор с указанием пар стартовой позии и смещения
        /// </summary>
        /// <param name="pos">Пара X и Y</param>
        /// <param name="dir">Смещение по X и Y</param>
        protected BaseObject(Point pos, Point dir)
        {
            Pos = pos;
            Dir = dir;
        }

        /// <summary>
        /// Конструктор с указанием пар стартовой позии,смещения и размера
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="dir"></param>
        /// <param name="size"></param>
        protected BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }

        public bool Collision(ICollision o) => o.Rect.IntersectsWith(Rect);

        public Rectangle Rect => new Rectangle(Pos, Size);



        /// <summary>
        /// Отрисовывает объект(цвет)
        /// </summary>
        /// <param name="color"></param>
        public abstract void Draw(Color color);


        /// <summary>
        /// Смещает стартовые точки в пределах экрана
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Изменяет положение точки
        /// </summary>
        /// <param name="posX">позиция X</param>
        /// <param name="posY">позиция Y</param>
        public void UpdatePos(int posX, int posY)
        {
            Pos.X = posX;
            Pos.Y = posY;
        }

    }
}
