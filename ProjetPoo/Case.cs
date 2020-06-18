using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ProjetPoo
{
    class Case
    {
        private Rectangle Rectangle;
        private int state;
        private static readonly int specs = 100;
        // Constructor 
        public Case(int tx , int ty , int specs)
        {
            Rectangle = new Rectangle(tx, ty, specs, specs); // more of a square than rectangle
            state = 0; // no one has played yet 
        }
        public void reset()
        {
            this.state = 0;
        }
        public bool PlayerA()
        { return state == 1; }
        public bool PlayerB()
        { return state == -1; }
        // checking if point is in rectangle 
        public bool checkPoint(Point point)
        {
            bool returner = this.Rectangle.Contains(point);
            return returner;
        }
        public void draw(ref Graphics graphics)
        {
            Pen stylo = new Pen(Color.FromArgb(198, 1, 31) , 15);
            graphics.DrawRectangle(stylo, Rectangle);
        }
        public void round(ref Graphics g)
        {
            if (state == 0 )
            {
                Pen stylo = new Pen(Color.DarkSlateBlue, 15);
                 Brush s = new SolidBrush(Color.Gold);
                 g.FillEllipse(s, Rectangle);
                 g.DrawEllipse(stylo, Rectangle);
                /*Image newimage = Image.FromFile("C:/Users/Noumane agouzil/Desktop/jerry.png");
                Point p = new Point(Rectangle.X + 5, Rectangle.Y + 5);
                g.DrawImage(newimage, p);*/
                

            }
        }
        public void Croix(ref Graphics g)
        {
            // Graphics g = p.Graphics;

            if (state == 0)
            {
                /*Pen s = new Pen(Color.Red, 20);
                g.DrawLine(s, Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom);
                g.DrawLine(s, Rectangle.Left, Rectangle.Bottom, Rectangle.Right, Rectangle.Top);
                state = 1;*/
                Image newimage = Image.FromFile("C:/Users/Noumane agouzil/Desktop/tom.png");
                Point p = new Point(Rectangle.X+5, Rectangle.Y+5);
                g.DrawImage(newimage, p);
                state = 1;
            }
        }

    }
}
