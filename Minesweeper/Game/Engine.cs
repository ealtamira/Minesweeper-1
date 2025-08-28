using System;
using System.Collections.Generic;
using System.Windows.Threading;

namespace Minesweeper
{
    public static class Engine
    {
        public static Random rng;
        public static Tile[,] bts;
        public static int lines;
        public static int size;
        public static bool hasStarted;
        public static int mines;
        public static int flagsplaced;
        public static int clearedboxes;
        private static DispatcherTimer timer;
        private static int seconds;

        // 🔑 EVENTS instead of directly updating UI
        public static event Action<int> TimerTick;
        public static event Action<int, int, int> TileRevealed;
        public static event Action GameOver;
        public static event Action GameWon;
        public static event Action<int> FlagsChanged;

        public static void Init(int n, int m, int tileSize)
        {
            clearedboxes = 0;
            seconds = 0;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            rng = new Random();
            mines = m;
            flagsplaced = 0;
            lines = n;
            size = tileSize;
            bts = new Tile[lines, lines];
            hasStarted = false;

            for (int i = 0; i < lines; i++)
                for (int j = 0; j < lines; j++)
                    bts[i, j] = new Tile(i, j);
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            seconds++;
            TimerTick?.Invoke(seconds);
        }

        public static void HandleLeftClick(int row, int col)
        {
            if (!hasStarted)
            {
                GenerateMines(row, col);
                timer.Start();
            }

            Tile t = bts[row, col];
            if (t.CanBePressed)
                RevealTile(t);
        }

        public static void HandleRightClick(int row, int col)
        {
            if (!hasStarted)
            {
                GenerateMines(row, col);
                timer.Start();
            }

            Tile t = bts[row, col];
            if (!t.IsRevealed)
            {
                if (!t.IsFlagged && flagsplaced >= mines)
                    return;

                t.IsFlagged = !t.IsFlagged;
                flagsplaced += t.IsFlagged ? 1 : -1;
                FlagsChanged?.Invoke(mines - flagsplaced);
            }
        }

        public static void RevealTile(Tile tile)
        {
            tile.IsRevealed = true;
            clearedboxes++;

            if (tile.Value == 9)
            {
                timer.Stop();
                TileRevealed?.Invoke(tile.Row, tile.Col, 9);
                GameOver?.Invoke();
                return;
            }

            TileRevealed?.Invoke(tile.Row, tile.Col, tile.Value);

            if (tile.Value == 0)
                FloodFill(tile);

            WinCheck();
        }

        private static void FloodFill(Tile start)
        {
            Queue<Tile> queue = new Queue<Tile>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                Tile t = queue.Dequeue();
                for (int i = t.Row - 1; i <= t.Row + 1; i++)
                {
                    for (int j = t.Col - 1; j <= t.Col + 1; j++)
                    {
                        if (i < 0 || j < 0 || i >= lines || j >= lines) continue;

                        Tile neighbor = bts[i, j];
                        if (!neighbor.IsRevealed && !neighbor.IsFlagged)
                        {
                            neighbor.IsRevealed = true;
                            TileRevealed?.Invoke(neighbor.Row, neighbor.Col, neighbor.Value);
                            clearedboxes++;

                            if (neighbor.Value == 0)
                                queue.Enqueue(neighbor);
                        }
                    }
                }
            }
        }

        public static void WinCheck()
        {
            if (clearedboxes == (lines * lines) - mines)
            {
                timer.Stop();
                GameWon?.Invoke();
            }
        }

        public static void GenerateMines(int pressedRow, int pressedCol)
        {
            hasStarted = true;
            int minesPlaced = 0;

            while (minesPlaced < mines)
            {
                int r = rng.Next(0, lines);
                int c = rng.Next(0, lines);

                if (bts[r, c].Value == 9) continue;
                if (Math.Abs(r - pressedRow) <= 1 && Math.Abs(c - pressedCol) <= 1) continue;

                bts[r, c].Value = 9;
                minesPlaced++;

                for (int i = r - 1; i <= r + 1; i++)
                {
                    for (int j = c - 1; j <= c + 1; j++)
                    {
                        if (i >= 0 && j >= 0 && i < lines && j < lines && bts[i, j].Value != 9)
                            bts[i, j].Value++;
                    }
                }
            }
        }

        public static void Reset(int tileSize)
        {
            clearedboxes = 0;
            flagsplaced = 0;
            seconds = 0;
            hasStarted = false;
            timer.Stop();
            size = tileSize;
            bts = new Tile[lines, lines];

            for (int i = 0; i < lines; i++)
                for (int j = 0; j < lines; j++)
                    bts[i, j] = new Tile(i, j);
        }
    }
}
