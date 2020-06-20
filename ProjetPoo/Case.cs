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
        {
            if (state == 1) return true;
            else return false;
        }
        public bool PlayerB()
        {
            
            return state == -1; }
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
                
                Image newimage = ProjetPoo.Properties.Resources.jerry;
                Point p = new Point(Rectangle.X + 5, Rectangle.Y + 5);
                g.DrawImage(newimage, p);
                state = -1;
                
                
            }
        }
        public void Croix(ref Graphics g)
        {
            
            if (state == 0)
            {
                
                Image newimage = ProjetPoo.Properties.Resources.tom;
                Point p = new Point(Rectangle.X+5, Rectangle.Y+5);
                g.DrawImage(newimage, p);
                state = 1;
                
            }
        }

    }
}
