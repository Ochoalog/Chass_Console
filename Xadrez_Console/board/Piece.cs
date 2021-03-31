namespace board
{
    abstract class Piece
    {
        public Position position { get; set; }
        public Color color { get; protected set; }
        public int qtyMoviments { get; protected set; }
        public Board board { get; protected set; }

        public Piece(Board board, Color color)  
        {
            this.position = null;
            this.color = color;
            this.board = board;
            this.qtyMoviments = 0;
        }

        public void incrementQtyMoviments()
        {
            qtyMoviments ++;
        }

        public void decrementQtyMoviment()
        {
            qtyMoviments--;
        }

        public bool existpossiblesMoviments()
        {
            bool[,] mat = possiblesMoviments();
            for (int i = 0; i < board.Lines; i++)
            {
                for (int j = 0; j < board.Colums; j++)
                {
                    if(mat[i, j])
                    {
                        return true;
                    }
                    
                }
            }
            return false;
        }

        public bool canGo(Position pos)
        {
            return possiblesMoviments()[pos.line, pos.colum];
        }

        public abstract bool[,] possiblesMoviments();
    }
}
