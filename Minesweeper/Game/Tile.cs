namespace Minesweeper
{
    public class Tile
    {
        public int Row { get; }
        public int Col { get; }
        public int Value { get; set; }
        public bool IsRevealed { get; set; }
        public bool IsFlagged { get; set; }
        public bool CanBePressed => !IsRevealed && !IsFlagged;
        public Tile(int row, int col)
        {
            Row = row;
            Col = col;
            Value = 0;
            IsRevealed = false;
            IsFlagged = false;
        }
    }
}
