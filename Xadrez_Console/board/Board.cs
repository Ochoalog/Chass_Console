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

        public Piece piece(Position pos)
        {
            return Pieces[pos.colum, pos.line];
        }

        public bool existPiece(Position pos)
        {
            validatePosition(pos);
            return piece(pos) != null;

        }

        public void putPiece(Piece p, Position pos)
        {
            if (existPiece(pos))
            {
                throw new BoardException("Já existe uma Peça nessa posição!");
            }           
            Pieces[pos.line, pos.colum] = p;
            p.position = pos;
        }

        public bool positionValid(Position pos)
        {
            if(pos.line < 0|| pos.line >= Lines || pos.colum < 0 || pos.colum >= Colums)
            {
                return false;
            }
            return true;
        }

        public void validatePosition(Position pos)
        {
            if (!positionValid(pos))
            {
                throw new BoardException("Posição Inválida!");
            }
        }
    }
}
