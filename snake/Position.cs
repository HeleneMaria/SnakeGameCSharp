namespace snake
{
    /// <summary>
    /// Used for serialization to save only the position of the elements wall, snake and apple
    /// </summary>
    public class Position
    {
        public int x;
        public int y;

        /// <summary>
        /// Constructor by default of the class Position
        /// </summary>
        public Position()
        {
            x = 0;
            y = 0;
        }

        /// <summary>
        /// constructor of the class Position
        /// </summary>
        /// <param name="x">Position on the horizontal axe</param>
        /// <param name="y">Position on the vertical axe</param>
        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
