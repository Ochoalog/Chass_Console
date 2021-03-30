namespace board
{
    class Position
    {
        public int line { get; set; }
        public int colum { get; set; }

        public Position(int linha, int coluna)
        {
            this.line = linha;
            this.colum = coluna;
        }

        public override string ToString()
        {
            return line +
                ", " +
                colum;
        }
    }
}
