using System.Drawing;
using System.Windows.Forms;

namespace snake
{
    /// <summary>
    /// Takes care of the creation of all the elements: apple, walls and parts of snake
    /// </summary>
    public class Element
    {
        public PictureBox pictureBox1;

        /// <summary>
        /// Constuctor of the class Element
        /// </summary>
        /// <param name="x">Position of the element on the horizontal axe the the panel</param>
        /// <param name="y">Position of the element on the vertical axe the the panel</param>
        /// <param name="object1">Picture of the element</param>
        /// <param name="sizeElem">Width and height of the element</param>
        /// <param name="panel1">Panel where the element will be added and displayed</param>
        public Element(int x, int y, object object1, int sizeElem, Panel panel1)
        {
            pictureBox1 = new System.Windows.Forms.PictureBox();

            panel1.Controls.Add(pictureBox1);
            pictureBox1.Size = new Size(sizeElem, sizeElem);
            pictureBox1.Location = new Point(x, y);
            pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
            pictureBox1.Image = (Image)object1;
        }

    }
}
