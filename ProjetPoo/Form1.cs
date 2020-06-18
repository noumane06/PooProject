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
        private int scoreA = 0;
        private int scoreB = 0;
        bool verrou = true;
        bool ok = true;
        public Form1()
        {
            InitializeComponent();
            Point p = new Point(50, 100);
            board = new Board(100, p);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label4.Text = this.scoreA.ToString();
            label5.Text = this.scoreB.ToString();
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            board.dessiner(ref g);
        }
         

        private void mouseDown(object sender, MouseEventArgs e)
        {
           
            Graphics g = CreateGraphics();
            Point p = e.Location;
            switch (e.Button)
            {
                case MouseButtons.Left:
                    {
                         ok = false;
                        int test = 0;
                        if (verrou)
                        {
                            ok = this.board.PlayerA(ref g, p);
                            if (ok) verrou = false;
                             test = board.CheckForGameOver(this);
                        }
                         
                        if (test == 1 || test == -1)
                        {
                            verrou = true;
                            if (test == 1)
                            {
                                this.scoreA++;
                                label4.Text = this.scoreA.ToString();
                            }
                            if (test == -1 )
                            {
                                this.scoreB++;
                                label5.Text = this.scoreB.ToString();
                            }
                        }
                    }

                    break;

                case MouseButtons.Right:
                    {

                         ok = true;
                        int test = 0;

                        if (!verrou)
                        {
                            ok = this.board.PlayerB(ref g, p);
                            
                            test = board.CheckForGameOver(this);
                            if (ok) verrou = true;
                        }
                         
                        if (test == 1 || test == -1)
                        {
                            verrou = true;
                            if (test == 1)
                            {
                                this.scoreA++;
                                label4.Text = this.scoreA.ToString();
                            }
                            if (test == -1)
                            {
                                this.scoreB++;
                                label5.Text = this.scoreB.ToString();
                            }
                        }
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.scoreA = 0;
            this.scoreB = 0;
            label4.Text = this.scoreA.ToString();
            label5.Text = this.scoreB.ToString();
        }
    }
}
