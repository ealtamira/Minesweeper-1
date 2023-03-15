using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Minesweeper
{
    public class Tile
    {
        public Button button;
        public int line;
        public int column;
        public int Value;
        public bool canBePressed;
        public Tile(int i, int j) 
        {
            canBePressed = true;
            line = i;
            column = j;
            Value = 0;
            button = new Button()
            {
                Parent = Engine.form.pictureBox1,
                Size = new Size(Engine.size, Engine.size),
                Location = new Point(i * Engine.size, j * Engine.size),
                BackgroundImageLayout = ImageLayout.Stretch,
                BackgroundImage = Engine.form.unclicked,
                
            };
            button.Click += Button_Click;
            button.MouseDown += Button_MouseDown;
        }

        private void Button_MouseDown(object? sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                Button pressed = sender as Button;
                if(pressed != null)
                {
                    if(pressed.BackgroundImage == Engine.form.unclicked)
                    {
                        pressed.BackgroundImage = Engine.form.flag;
                        Engine.flagsplaced++;
                        canBePressed = false;
                    }
                    else
                    {
                        pressed.BackgroundImage = Engine.form.unclicked;
                        Engine.flagsplaced--;
                        canBePressed = true;
                    }
                    Engine.form.LabelUpdate();
                }
            }
        }

        private void Button_Click(object? sender, EventArgs e)
        {
            Button pressed = sender as Button;
            if (pressed != null)
            {
                if (!Engine.hasStarted)
                {
                    Engine.GenerateMines(line, column);
                    Engine.timer.Start();
                }
                if (canBePressed)
                {
                    RevealButton(pressed);
                }
            }
                
        }

        void RevealButton(Button pressed)
        {

            pressed.Enabled = false;
            Engine.clearedboxes++;
            if (this.Value == 0)
            {
                FindAllNeighbors();
                Engine.WinCheck();
                return;
            }
            else if (this.Value == 9)
            {
                pressed.BackgroundImage = Engine.form.images[this.Value];
                this.canBePressed = false;
                Engine.timer.Stop();
                MessageBox.Show("You died", "Game Over");
                Engine.ShowAllMines();
                return;
            }
            pressed.BackgroundImage = Engine.form.images[this.Value];
            this.canBePressed = false;
            Engine.WinCheck();
        }
        void FindAllNeighbors()
        {
            Queue<Tile> neighbors = new Queue<Tile>();
            neighbors.Enqueue(this);
            while(neighbors.Count > 0)
            {
                Tile toCheck = neighbors.Dequeue();
                for (int i = toCheck.line - 1; i <= toCheck.line + 1; i++)
                {
                    for (int j = toCheck.column - 1; j <= toCheck.column + 1; j++)
                    {
                        try
                        {
                            if (Engine.bts[i,j].canBePressed && Engine.bts[i, j].Value == 0)
                            {
                                if(Engine.bts[i, j].button.Enabled)
                                {
                                    Engine.bts[i, j].button.Enabled = false;
                                    Engine.clearedboxes++;
                                }
                                
                                Engine.bts[i, j].canBePressed = false;
                                neighbors.Enqueue(Engine.bts[i, j]);
                            }
                            else
                            {
                                if (Engine.bts[i,j].button.BackgroundImage == Engine.form.flag)
                                {
                                    Engine.flagsplaced--;
                                    Engine.form.LabelUpdate();
                                    if(Engine.bts[i, j].Value == 0)
                                    {
                                        neighbors.Enqueue(Engine.bts[i, j]);
                                    }
                                }
                                if (Engine.bts[i, j].button.Enabled)
                                {
                                    Engine.bts[i, j].button.Enabled = false;
                                    Engine.clearedboxes++;
                                }
                                Engine.bts[i, j].canBePressed = false;
                                Engine.bts[i, j].button.BackgroundImage = Engine.form.images[Engine.bts[i, j].Value];
                                
                            }
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
