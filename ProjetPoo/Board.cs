using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Media;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ProjetPoo
{

    class Board
    {
        Case[,] cases;
        private List<string> resultsB = new List<string>();
        private List<string> resultsA = new List<string>();
        private int x = 0;
        public Board(int specs , Point start )
        {
            cases = new Case[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    cases[i, j] = new Case(start.X+j*specs, start.Y+i*specs,100);
        }
        public void dessiner(ref Graphics g)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    cases[i, j].draw(ref g);
        }
        public void ResetGame(Form1 f)
        {


            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    cases[i, j].reset();
            x = 0;
            f.Refresh(); // redessiner

        }
        public bool SeachRect(Point p, out int ix, out int jy)
        {
            ix = -1;
            jy = -1;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (cases[i, j].checkPoint(p)) { ix = i; jy = j; return true; };

            return false;
            
        }
        private void validatePlayerEntry()
        {


            string message = "You did not click on the cases. Cancel this operation?";
            string caption = "Error Detected in Input given by " + "Player";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons);


        }
        public bool PlayerB(ref Graphics g, Point p)
        {
            int i = -1, j = -1;
            
            bool r = SeachRect(p, out i, out j); // check where the click occured if is inside the grille
            if (!r) { validatePlayerEntry(); return false; } // if not messagebox outside the box
            if (r) { cases[i, j].round(ref g); return true; } // pla begin
            
            return true;
        }
        public bool PlayerA(ref Graphics g, Point p)
        {
            int i = -1, j = -1;
            bool r = SeachRect(p, out i, out j); // check where the click occured if is inside the grille
            if (!r) { validatePlayerEntry(); return false; } // if not messagebox outside the box
            if (r) { cases[i, j].Croix(ref g); return true; } // pla begin
            return true;
        }
        public int IsWinner()
        {

            // player A
            if (
                (cases[0, 0].PlayerA()) &&
                (cases[1, 0].PlayerA()) &&
                (cases[2, 0].PlayerA())
                )
                return 1;

            if (
                (cases[0, 1].PlayerA()) &&
                (cases[1, 1].PlayerA()) &&
                (cases[2, 1].PlayerA())
                )
                return 1;

            if (
                (cases[0, 2].PlayerA()) &&
                (cases[1, 2].PlayerA()) &&
                (cases[2, 2].PlayerA())
                )
                return 1;

            if (
                            (cases[0, 0].PlayerA()) &&
                            (cases[0, 1].PlayerA()) &&
                            (cases[0, 2].PlayerA())
                            )
                return 1;

            if (
                (cases[1, 0].PlayerA()) &&
                (cases[1, 1].PlayerA()) &&
                (cases[1, 2].PlayerA())
                )
                return 1;

            if (
                (cases[2, 0].PlayerA()) &&
                (cases[2, 1].PlayerA()) &&
                (cases[2, 2].PlayerA())
                )
                return 1;


            // player B

            if (
                (cases[0, 0].PlayerB()) &&
                (cases[1, 0].PlayerB()) &&
                (cases[2, 0].PlayerB())
                )
                return -1;

            if (
                (cases[0, 1].PlayerB()) &&
                (cases[1, 1].PlayerB()) &&
                (cases[2, 1].PlayerB())
                )
                return -1;

            if (
                (cases[0, 2].PlayerB()) &&
                (cases[1, 2].PlayerB()) &&
                (cases[2, 2].PlayerB())
                )
                return -1;

            if (
                            (cases[0, 0].PlayerB()) &&
                            (cases[0, 1].PlayerB()) &&
                            (cases[0, 2].PlayerA())
                            )
                return -1;

            if (
                (cases[1, 0].PlayerB()) &&
                (cases[1, 1].PlayerB()) &&
                (cases[1, 2].PlayerB())
                )
                return -1;

            if (
                (cases[2, 0].PlayerB()) &&
                (cases[2, 1].PlayerB()) &&
                (cases[2, 2].PlayerB())
                )
                return -1;

            // player A
            if (
                (cases[0, 0].PlayerA()) &&
                (cases[1, 1].PlayerA()) &&
                (cases[2, 2].PlayerA())
                )
                return 1;

            // player A
            if (
                (cases[0, 2].PlayerA()) &&
                (cases[1, 1].PlayerA()) &&
                (cases[2, 0].PlayerA())
                )
                return 1;


            // player B
            if (
                (cases[0, 2].PlayerB()) &&
                (cases[1, 1].PlayerB()) &&
                (cases[2, 0].PlayerB())
                )
                return -1;

            // player B
            if (
                (cases[0, 0].PlayerB()) &&
                (cases[1, 1].PlayerB()) &&
                (cases[2, 2].PlayerB())
                )
                return -1;


            return 0;
        }
        public int CheckForGameOver(Form1 f)
        {
            string tiedMessage = "Game tied";
            string caption = "Ending Game bye !";
            MessageBoxButtons buttons = MessageBoxButtons.OK;

            int nWin = this.IsWinner();

            if (nWin == 1)
            {
                SoundPlayer player = new SoundPlayer(ProjetPoo.Properties.Resources.winSound);
                player.Play();
                for (int a = 0; a < 3; a++)
                {
                    for (int b = 0; b < 3; b++)
                    {
                        if (cases[a, b].PlayerA())
                        {
                            string res = "case [" + a + "," + b + "]";
                            resultsA.Add(res);
                        }
                        if (cases[a, b].PlayerB())
                        {
                            string res = "case [" + a + "," + b + "]";
                            resultsB.Add(res);
                        }
                    }
                };

                string mes = string.Join(Environment.NewLine, resultsA) + " Win !";
                string messageB = string.Join(Environment.NewLine, resultsB) + " GameOver !";
                mes = mes.Replace("\r\n", " | ");
                messageB = messageB.Replace("\r\n", " | ");
                var filePath = @Path.GetDirectoryName(Application.ExecutablePath) + "/resource/ResultsD.json";
                var initialJson = File.ReadAllText(filePath);
                if (initialJson == "")
                {
                    initialJson = "[]";
                }

                var array = JArray.Parse(initialJson);

                var itemToAdd = new JObject();
                itemToAdd["Name 1"] = "PlayerA";
                itemToAdd["Results 1"] = mes;
                itemToAdd["Name 2"] = "PlayerB";
                itemToAdd["Results 2"] = messageB;
                itemToAdd["Date"] = DateTime.Now;
                array.Add(itemToAdd);
                var jsonToOutput = JsonConvert.SerializeObject(array, Formatting.Indented);
                File.WriteAllText(filePath, jsonToOutput);
                MessageBox.Show("Player A woon !!", caption, buttons);
                this.ResetGame(f);
                resultsA.Clear(); resultsB.Clear();
                x = 0;
                return 1;
            }
            else


                if (nWin == -1)
            {
                SoundPlayer player = new SoundPlayer(ProjetPoo.Properties.Resources.winSound);
                player.Play();
                for (int a = 0; a < 3; a++)
                {
                    for (int b = 0; b < 3; b++)
                    {
                        if (cases[a, b].PlayerB())
                        {
                            string res = "case [" + a + "," + b + "]";
                            resultsB.Add(res);
                        }
                        if (cases[a, b].PlayerA())
                        {
                            string res = "case [" + a + "," + b + "]";
                            resultsA.Add(res);
                        }
                    }
                }
                string mes = string.Join(Environment.NewLine, resultsA) + " Game Over !";
                string messageB = string.Join(Environment.NewLine, resultsB) + " WIN !";
                mes = mes.Replace("\r\n", " | ");
                messageB = messageB.Replace("\r\n", " | ");
                var filePath = @Path.GetDirectoryName(Application.ExecutablePath) + "/resource/ResultsD.json";
                var initialJson = File.ReadAllText(filePath);
                if (initialJson == "")
                {
                    initialJson = "[]";
                }

                var array = JArray.Parse(initialJson);

                var itemToAdd = new JObject();
                itemToAdd["Name 1"] = "PlayerA";
                itemToAdd["Results 1"] = mes;
                itemToAdd["Name 2"] = "PlayerB";
                itemToAdd["Results 2"] = messageB;
                itemToAdd["Date"] = DateTime.Now;
                array.Add(itemToAdd);
                var jsonToOutput = JsonConvert.SerializeObject(array, Formatting.Indented);
                File.WriteAllText(filePath, jsonToOutput);
                MessageBox.Show("PlayerB wonn !!", caption, buttons); this.ResetGame(f);
                resultsA.Clear(); resultsB.Clear();
                x = 0;
                return -1;
            }
            // en cas d'égalité à ajouter :)
            this.x++;
            if (x == 9)
            {
                MessageBox.Show(tiedMessage, caption, buttons); this.ResetGame(f); return 0;
            }

            return 0;

        }

    }
}
