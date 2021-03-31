namespace board
{
    class Position
    {
        public int line { get; set; }
        public int colum { get; set; }

        public Position(int line, int colum)
        {
            this.line = line;
            this.colum = colum;
        }

        public override string ToString()
        {
            return line +
                ", " +
                colum;
        }
    }
}
