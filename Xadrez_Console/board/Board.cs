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
            return Pieces[pos.line, pos.colum];
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
                throw new BoardException("Já existe uma peça nessa posição!");
            }           
            Pieces[pos.line, pos.colum] = p;
            p.position = pos;
        }

        public Piece removePiece(Position pos)
        {
            if(piece(pos) == null)
            {
                return null;
            }
            Piece aux = piece(pos);
            aux.position = null;
            Pieces[pos.line, pos.colum] = null;
            return aux;
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
            if (positionValid(pos))
            {
                throw new BoardException("Posição Inválida!");
            }
        }
    }
}