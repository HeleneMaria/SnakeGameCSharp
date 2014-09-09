using System;
using System.Linq;
using System.Windows.Forms;

namespace snake
{
    /// <summary>
    /// Takes care of all the data related to the apple: creates and change its position if the apple has been eaten by the snake
    /// </summary>
    public class Apple : Element 
    {
        /// <summary>s
        /// Constructor
        /// </summary>
        /// <param name="x">Location of the Apple on the horizontal axe of the panel</param>
        /// <param name="y">Location of the Apple on the vertical axe of the panel</param>
        /// <param name="object1">Picture of the apple</param>
        /// <param name="sizeElem">Height and width of the apple</param>
        /// <param name="panel1">Panel where the apple will be added and displayed</param>
        public Apple(int x, int y,object object1, int sizeElem, Panel panel1)
            : base(x, y, object1, sizeElem, panel1)
        { }


        /// <summary>
        /// Change the position of the apple when this one is touched by the snake
        /// </summary>
        /// <param name="s">All the data related to the snake, to know its position.</param>
        /// <param name="walls">All the data related to the walls, to know their position</param>
        /// <param name="width">Width of the panel</param>
        /// <param name="height">Height of the panel</param>
        public void changePosition(Snake s, AllWall walls, int width, int height) //width and height of the panel
        {
            Random random = new Random();
            int posx = 0;
            int posy = 0;
            int okFinal = 0;

            while (okFinal == 0)
            {
                posx = 20 * random.Next(0, width / 20);
                posy = 20 * random.Next(0, height / 20);
                for (int i = 0; i < s.myList.Count; i++)
                {
                    if (s.myList.ElementAt<Element>(i).pictureBox1.Left == posx && s.myList.ElementAt<Element>(i).pictureBox1.Top == posy)
                    {
                        i = s.myList.Count;//we leave the for and go back in the while
                    }
                    if (i == s.myList.Count - 1)
                    {
                        okFinal = 1;//we leave the for AND the while
                    }
                }

                if(okFinal == 1)
                    for (int i = 0; i < walls.myWalls.Count; i++)
                    {
                        if (walls.myWalls.ElementAt<Element>(i).pictureBox1.Left == posx && walls.myWalls.ElementAt<Element>(i).pictureBox1.Top == posy)
                        {
                            i = walls.myWalls.Count;//we leave the for and go back in the while
                            okFinal = 0;
                        }
                    }
            }
            pictureBox1.Left = posx;
            pictureBox1.Top = posy;
        }
    }
}
