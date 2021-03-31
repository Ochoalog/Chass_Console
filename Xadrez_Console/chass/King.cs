using board;

namespace chass
{
    class King : Piece
    {
        public King(Board board, Color color) : base(board, color)
        {

        }

        public override string ToString()
        {
            return "K";
        }

        private bool canMove(Position pos)
        {
            Piece p = board.piece(pos);
            return p == null || p.color != color;
        }

        public override bool[,] possiblesMoviments()
        {
            bool[,] mat = new bool[board.Lines, board.Colums];

            Position pos = new Position(0, 0);

            //acima
            pos.defineValues(position.line - 1, position.colum);
            if(board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.colum] = true;
            }

            
            //nordeste
            pos.defineValues(position.line - 1, position.colum + 1);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.colum] = true;
            }

            //direita
            pos.defineValues(position.line, position.colum + 1);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.colum] = true;
            }

            //sudeste
            pos.defineValues(position.line + 1, position.colum + 1);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.colum] = true;
            }

            //abaixo
            pos.defineValues(position.line + 1, position.colum);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.colum] = true;
            }

            //sudoeste
            pos.defineValues(position.line + 1, position.colum - 1);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.colum] = true;
            }

            //esquerda
            pos.defineValues(position.line, position.colum - 1);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.colum] = true;
            }

            //noroeste
            pos.defineValues(position.line - 1, position.colum - 1);
            if (board.positionValid(pos) && canMove(pos))
            {
                mat[pos.line, pos.colum] = true;
            }

            return mat;
        }
    }
}
