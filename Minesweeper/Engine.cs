using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public static class Engine
    {
        public static Random rng;
        public static Form1 form;
        public static Tile[,] bts;
        public static int lines;
        public static int size;
        public static bool hasStarted;
        public static int mines;
        public static int flagsplaced;
        public static int clearedboxes;
        public static System.Windows.Forms.Timer timer;

        public static void Init(Form1 f, int n, int m)
        {
            clearedboxes = 0;
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            rng = new Random();
            mines = m;
            flagsplaced = 0;
            form = f;
            lines = n;
            size = form.pictureBox1.Width / lines;
            bts = new Tile[lines,lines];
            hasStarted = false;

            for(int i = 0; i < lines; i++)
            {
                for(int j = 0; j < lines; j++)
                {
                    bts[i, j] = new Tile(i,j);
                }
            }
        }

        private static void Timer_Tick(object? sender, EventArgs e)
        {
            form.TimerUpdate();
        }
        public static void WinCheck()
        {
            //MessageBox.Show(clearedboxes.ToString());
            if(clearedboxes == (lines * lines) - mines)
            {
                timer.Stop();
                MessageBox.Show("You win!", "Congratulations");               
                ShowAllDefusedMines();
            }
        }

        public static void GenerateMines(int pressedline, int pressedcolumn)
        {
            int mines = Engine.mines;
            Engine.hasStarted = true;
            for(int i = 0; i < mines; i++)
            {
                int l, c;
                do {
                    l = rng.Next(lines);
                    c = rng.Next(lines);
                } while((l >= pressedline - rng.Next(1, 4) && l <= pressedline + rng.Next(1, 4) && c >= pressedcolumn - rng.Next(1, 4) && c <= pressedcolumn + rng.Next(1, 4)) || bts[l, c].Value == 9);
                bts[l, c].Value = 9;
                //bts[l, c].button.BackgroundImage = Engine.form.flag;
                for(int k = l - 1; k <= l + 1; k++)
                {
                    for(int h = c - 1; h <= c + 1; h++)
                    {
                        try
                        {
                            if (bts[k, h].Value != 9)
                            {
                                bts[k, h].Value += 1;
                                //bts[k, h].button.Text = bts[k,h].Value.ToString();
                            }
                            //bts[k, h].button.BackgroundImage = Engine.form.images[bts[k, h].Value - 1];
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }
            }

        }
        public static void ShowAllMines()
        {
            for(int i = 0; i < lines; i++)
            {
                for(int j = 0; j < lines; j++)
                {
                    bts[i, j].canBePressed = false;
                    bts[i, j].button.Enabled = false;
                    if (bts[i, j].Value == 9)
                    {
                        bts[i, j].button.BackgroundImage = form.images[9];
                    }
                }
            }
        }
        public static void ShowAllDefusedMines()
        {
            for (int i = 0; i < lines; i++)
            {
                for (int j = 0; j < lines; j++)
                {
                    bts[i, j].canBePressed = false;
                    bts[i, j].button.Enabled = false;
                    if (bts[i, j].Value == 9)
                    {
                        bts[i, j].button.BackgroundImage = form.images[10];
                    }
                }
            }
        }
        public static void Reset()
        {
            clearedboxes = 0;
            size = form.pictureBox1.Width / lines;
            hasStarted = false;
            timer.Stop();
            flagsplaced = 0;

            for (int i = 0; i < lines; i++)
            {
                for (int j = 0; j < lines; j++)
                {
                    bts[i, j] = new Tile(i, j);
                }
            }
        }
    }
}
