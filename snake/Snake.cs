using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace snake
{
    /// <summary>
    /// Groups all the data related to the snake
    /// </summary>
    public class Snake
    {
        public List<Element> myList = new List<Element>();

        /// <summary>
        /// Constructor of the class Snake
        /// </summary>
        /// <param name="nbElements">Number of elements that compose the snake</param>
        /// <param name="object1">Picture of an element of the snake</param>
        /// <param name="sizeElem">Width and height of an element of the snake</param>
        /// <param name="panel1">Panel where the snake will be added & displayed</param>
        public Snake(int nbElements, object object1, int sizeElem, Panel panel1)
        {
            //proto : public SnakeElem(int x, int y, String pathImage, int sizeElem)
            for (int i = 0; i < nbElements; i++)
            {
                myList.Add(new Element(100 + sizeElem * i, 20, object1, sizeElem, panel1));
            }
            
        }



        /// <summary>
        /// Detects if the snake touches his self
        /// </summary>
        /// <returns>Boolean (true if he touches a part of his body with his head)</returns>
        public bool detecteCorps()
        {
            bool touch = false;

            for (int i = 1; i < myList.Count; i++)
            {
                if (myList.ElementAt<Element>(i).pictureBox1.Left == myList.ElementAt<Element>(0).pictureBox1.Left && myList.ElementAt<Element>(i).pictureBox1.Top == myList.ElementAt<Element>(0).pictureBox1.Top)
                {
                    touch = true;
                }
            }
            return touch;
        }

    }

}
