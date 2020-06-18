using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ProjetPoo
{
    public partial class Form1 : Form
    {
        private Board board;
        bool verrou = true;
        Graphics win;
        bool ok = true;
        public Form1()
        {
            InitializeComponent();
            Point p = new Point(50, 100);
            board = new Board(100, p);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*label4.Text = this.scoreA.ToString();
            label5.Text = this.scoreB.ToString();*/
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            // if (ok) { win = e.Graphics; ok = false; }

            board.dessiner(ref g);
        }
   

        private void mouseDown(object sender, MouseEventArgs e)
        {
            string eventString = null;
            int i, j;
            Graphics g = this.CreateGraphics();
            Point p = e.Location;
            switch (e.Button)
            {
                case MouseButtons.Left:
                    {
                         ok = false;
                        eventString = "L";
                        // bool r = game.SeachRect(p, out i, out j);
                        if (verrou)
                        {
                            ok = this.board.PlayerA(ref g, p);
                            if (ok) verrou = false;
                        }
                        bool test = board.CheckForGameOver(this);
                        if (test) verrou = true;
                    }

                    break;

                case MouseButtons.Right:
                    {
                        eventString = "R";
                         ok = true;
                        eventString = "L";
                        // bool r = game.SeachRect(p, out i, out j);
                        if (!verrou)
                        {
                            ok = this.board.PlayerB(ref g, p);

                            if (ok) verrou = true;
                        }
                        bool test = board.CheckForGameOver(this);
                        if (test) verrou = true;
                    }

                    break;
                default:
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            board.ResetGame(this);
        }
    }
}
