using System;
using System.Collections.Generic;
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

        public static void Init(Form1 f, int n)
        {
            rng = new Random();
            form = f;
            lines = n;
            size = form.pictureBox1.Width / lines;
            bts = new Tile[lines,lines];

            for(int i = 0; i < lines; i++)
            {
                for(int j = 0; j < lines; j++)
                {
                    bts[i, j] = new Tile(i,j);
                }
            }
            GenerateMines(15);
        }
        public static void GenerateMines(int mines)
        {
            for(int i = 0; i < mines; i++)
            {
                int l, c;
                do {
                    l = rng.Next(lines);
                    c = rng.Next(lines);
                } while(bts[l, c].Value == 9);
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
    }
}
