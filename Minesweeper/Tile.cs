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
                    if(pressed.BackgroundImage == null)
                    {
                        pressed.BackgroundImage = Engine.form.flag;
                        canBePressed = false;
                    }
                    else
                    {
                        pressed.BackgroundImage = null;
                        canBePressed = true;
                    }
                    
                }
            }
        }

        private void Button_Click(object? sender, EventArgs e)
        {
            Button pressed = sender as Button;
            if (pressed != null)
            {
                if (canBePressed)
                {
                    pressed.Enabled = false;
                    if(this.Value == 0)
                    {
                        FindAllNeighbors();
                        return;
                    }
                    else if(this.Value == 9)
                    {
                        pressed.BackgroundImage = Engine.form.images[this.Value];
                        this.canBePressed = false;

                        MessageBox.Show("You died", "Game Over");
                        //ShowAllMines;
                        return;
                    }
                    pressed.BackgroundImage = Engine.form.images[this.Value];
                    this.canBePressed = false;
                }
            }
                
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
                                Engine.bts[i, j].button.Enabled = false;
                                Engine.bts[i, j].canBePressed = false;
                                neighbors.Enqueue(Engine.bts[i, j]);
                            }
                            else
                            {
                                Engine.bts[i, j].button.Enabled = false;
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
