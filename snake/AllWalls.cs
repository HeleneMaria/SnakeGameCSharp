using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace snake
{
    /// <summary>
    /// Takes care of all things related to the wall: creation and collision
    /// </summary>
    public class AllWall
    {

        public List<Element> myWalls;
        int newX, newY;

        /// <summary>
        /// Constructor of the class AllWall
        /// </summary>
        public AllWall()
        {
            myWalls = new List<Element>();
        }

        /// <summary>
        /// Creates and adds a wall to the list myWalls
        /// </summary>
        /// <param name="s">All the data related to the snake, to know its position and not put a wall on it</param>
        /// <param name="object1">Picture of a Wall</param>
        /// <param name="sizeElem">Width and height of a Wall on the panel</param>
        /// <param name="panel1">Panel where the Wall will be added and displayed</param>
        public void addWall(Snake s, object object1, int sizeElem, Panel panel1)
        {
            choosePosition(s, panel1.Width, panel1.Height);
            myWalls.Insert(myWalls.Count, new Element(newX, newY, object1, sizeElem, panel1));
        }

        /// <summary>
        /// Attribute a position to a new wall
        /// </summary>
        /// <param name="s">All the data related to the snake, to know its position</param>
        /// <param name="width">Width of the panel</param>
        /// <param name="height">Height of the panel</param>
        public void choosePosition(Snake s, int width, int height)
        {

            int posx = 0;
            int posy = 0;
            int okFinal = 0;
            Random random = new Random();

            while (okFinal == 0)
            {
                posx = 20 * random.Next(0, width / 20);
                posy = 20 * random.Next(0, height / 20);

                //if its not the first element we check that we don't place the new wall on another one
                if (myWalls.Count > 0)
                    for (int i = 0; i < myWalls.Count; i++)
                    {
                        if (myWalls.ElementAt<Element>(i).pictureBox1.Left == posx && myWalls.ElementAt<Element>(i).pictureBox1.Top == posy)
                        {
                            i = myWalls.Count;//we leave the for and go back in the while
                        }

                        if (i == myWalls.Count - 1)
                        {
                            okFinal = 1; //we leave the for AND the while
                        }
                    }
                else okFinal = 1;

                //We check that the wall won't be at the same position as a part of the snake
                if (okFinal == 1)
                    for (int i = 0; i < s.myList.Count; i++)
                    {
                        if (s.myList.ElementAt<Element>(i).pictureBox1.Left == posx && s.myList.ElementAt<Element>(i).pictureBox1.Top == posy)
                        {
                            i = s.myList.Count;
                            okFinal = 0;
                        }
                    }
            }
            //positions of the new wall element
            newX = posx;
            newY = posy;
        }

        /// <summary>
        /// Detect if the snake touches a wall
        /// </summary>
        /// <param name="s">All the data related to the snake and help to know its position</param>
        /// <returns>boolean (true if touch)</returns>
        public bool detecteWall(Snake s)
        {
            bool touch = false;

            for (int i = 0; i < myWalls.Count; i++)
            {
                if (myWalls.ElementAt<Element>(i).pictureBox1.Left == s.myList.ElementAt<Element>(0).pictureBox1.Left && myWalls.ElementAt<Element>(i).pictureBox1.Top == s.myList.ElementAt<Element>(0).pictureBox1.Top)
                {
                    touch = true;
                }
            }

            return touch;
        }
    }
}


