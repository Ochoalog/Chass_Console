namespace board
{
    class Board
    {
        public int Lines { get; set; }
        public int Colums { get; set; }
        private Piece[,] Pieces;

        public Board(int lines, int colums)
        {
            this.Lines = lines;
            this.Colums = colums;
            Pieces = new Piece[lines, colums];
        }

        public Piece piece(int line, int colum)
        {
            return Pieces[line, colum];
        }
    }
}
