using System;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace snake
{
    //[DllImport("kernel32.dll")]
    //static extern void GetLocalTime(out SYSTEMTIME lpSystemTime);

        


    /// <summary>
    /// Class that makes the snake move, eat an apple, receive and display data of the adv etc.
    /// </summary>
    public partial class Game : Form
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEMTIME
        {
            public short Year;
            public short Month;
            public short DayOfWeek;
            public short Day;
            public short Hour;
            public short Minute;
            public short Second;
            public short Milliseconds;
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetSystemTime(out SYSTEMTIME systemTime);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetSystemTime(ref SYSTEMTIME systemTime);

        private int posx = 0;
        private int posy = 0;
        int direction;
        Snake snake1;
        Apple apple1;
        int sizeElem;
        Stopwatch timerD = new Stopwatch();
        int test;
        Random random = new Random();
        int score;
        int moved;
        public AllWall walls; //list of walls that will appear when a player eats an apple
        NetworkConnection f;
        SetParameters SP;
        Score_xml donneesJeuNous; //RAJOUTE XML
        Score_xml donneesJeuAdv; //RAJOUTE XML
        int scoreAdv;
        Snake snakeAdv;
        Apple appleAdv;
        AllWall wallsAdv;
        bool onePlayer; //=true si one player
        bool alreadyReset;
        public SYSTEMTIME st;

        /// <summary>
        /// default constructor if the class Game
        /// </summary>
        public Game()
        {
            InitializeComponent();
            alreadyReset = false;
            st = new SYSTEMTIME();
        }

        void initializeGame(int nbJoueur)
        {
            int nbElements = 10;
            sizeElem = 20;

            test = 0;
            score = 0;
            direction = 2;
            moved = 0;
            this.label3.Text = score.ToString();

            snake1 = new Snake(nbElements, GameResource.img1, sizeElem, panel1);
        
            walls = new AllWall();//creation of an empty list of walls
           
            apple1 = new Apple(40, 40, GameResource.apple1, sizeElem, panel1);
            apple1.changePosition(snake1, walls, this.panel1.Width, this.panel1.Height);



            if (onePlayer == false)
            {
                snakeAdv = new Snake(nbElements, GameResource.img2, sizeElem / 2, panel3);
                wallsAdv = new AllWall();
                appleAdv = new Apple(40, 40, GameResource.apple2, sizeElem / 2, panel3);
                //RAJOUTE XML
                donneesJeuNous = new Score_xml("SerializationTest.xml");
                donneesJeuAdv = new Score_xml("SerializationAdv.xml");
                scoreAdv = 0;

                SP = new SetParameters();

                if (alreadyReset == false)
                {
                    if (nbJoueur == 1)
                    {
                        f = new NetworkConnection(SP.AddressIPSender, SP.AddressIPReceiver);
                    }
                    else
                    {
                        f = new NetworkConnection(SP.AddressIPReceiver, SP.AddressIPSender);
                    }
                }

                do
                {
                    f.sendInfo("I'm here !");
                } while (f.xml != "I'm here !");

                this.panel3.Visible = true;
                this.label4.Visible = true;
                this.panel2.Visible = true;
                timer2.Start();
            }
            else
            {
                this.panel3.Visible = false;
                this.label4.Visible = false;
                this.panel2.Visible = false;
            }
            

            timerD.Start();
        }

        /// <summary>
        /// Resets the game if the user lost
        /// </summary>
        public void ResetGame()
        {
            //we delete all the walls and create a new list
            for (int count = 0; count < walls.myWalls.Count; count++)
            {
                walls.myWalls.ElementAt<Element>(count).pictureBox1.Visible = false;
            }
            walls = new AllWall();

            int nbElements = 10;
            sizeElem = 20;

            test = 0;
            score = 0;
            direction = 2;
            moved = 0;
            int i;

            this.label3.Text = score.ToString();
            //We hide the old apple and snake
            apple1.pictureBox1.Visible = false;
            for (i = 0; i < snake1.myList.Count; i++)
            {
                snake1.myList.ElementAt(i).pictureBox1.Visible = false;
            }
            snake1 = new Snake(nbElements, GameResource.img1, sizeElem, panel1);
            for (i = 0; i < snake1.myList.Count; i++)
            {
                snake1.myList.ElementAt(i).pictureBox1.Visible = false;
            }
            
            //Adversaire
            if (onePlayer == false)
            {
                for (int count = 0; count < wallsAdv.myWalls.Count; count++)
                {
                    wallsAdv.myWalls.ElementAt<Element>(count).pictureBox1.Visible = false;
                }
                appleAdv.pictureBox1.Visible = false;
                for (i = 0; i < snakeAdv.myList.Count; i++)
                {
                    snakeAdv.myList.ElementAt(i).pictureBox1.Visible = false;
                }
            }

            //We put the timer back to 0
            timerD.Reset();
        }

        /// <summary>
        /// Decides what action will be done when the user presses a specific key
        /// </summary>
        /// <param name="sender">The key that the user pressed</param>
        /// <param name="e">Data to know which key was pressed</param>
        private void Snake_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    if ((direction == 1 || direction == 2) && moved == 0)
                    {
                        posx = -20;
                        posy = 0;
                        test = 1;
                        direction = 0;
                        moved = 1;
                    }
                    break;

                case Keys.Right:
                    if ((direction == 1) && (moved == 0))
                    {
                        posx = 20;
                        posy = 0;
                        test = 1;
                        moved = 1;
                        direction = 0;
                    }
                    break;

                case Keys.Up:
                    if ((direction == 0 || direction == 2) && (moved == 0))
                    {
                        posx = 0;
                        posy = -20;
                        test = 1;
                        direction = 1;
                        moved = 1;
                    }
                    break;

                case Keys.Down:
                    if ((direction == 0 || direction == 2) && (moved == 0))
                    {
                        posx = 0;
                        posy = 20;
                        test = 1;
                        moved = 1;
                        direction = 1;
                    }
                    break;

                //Cheat codes
                case Keys.W: //We add a wall
                    walls.addWall(snake1, GameResource.wall, sizeElem, panel1);
                    break;

                case Keys.A: //Adds an element to the snake
                    int x = snake1.myList.ElementAt<Element>(snake1.myList.Count - 1).pictureBox1.Left;
                    int y = snake1.myList.ElementAt<Element>(snake1.myList.Count - 1).pictureBox1.Top;
                    snake1.myList.Insert(snake1.myList.Count - 1, new Element(x, y, GameResource.img1, sizeElem, panel1));
                    break;
            }
            
            
        }

        /// <summary>
        /// Called at each tick of the timer, makes the snake move, eat apples etc.
        /// </summary>
        /// <param name="sender">The timer</param>
        /// <param name="e">Event of the timer</param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Interoperability native function to get the time
            if (GetSystemTime(out st))
            {
                label2.Text = ((st.Hour)+2).ToString() + ":" + (st.Minute).ToString() + ":" + (st.Second).ToString();
            }

            if (test == 1)
            {
                //the snake moves
                for (int i = snake1.myList.Count - 1; i > 0; i--)
                {
                    snake1.myList.ElementAt<Element>(i).pictureBox1.Left = snake1.myList.ElementAt<Element>(i - 1).pictureBox1.Left;
                    snake1.myList.ElementAt<Element>(i).pictureBox1.Top = snake1.myList.ElementAt<Element>(i - 1).pictureBox1.Top;
                }

                snake1.myList.ElementAt<Element>(0).pictureBox1.Left += posx;
                snake1.myList.ElementAt<Element>(0).pictureBox1.Top += posy;

                //All the elements of the snake have been moved so the user can press a new key
                moved = 0;

                //We check that the snake is in the panel
                if (snake1.myList.ElementAt<Element>(0).pictureBox1.Left < 0) //Left
                {
                    snake1.myList.ElementAt<Element>(0).pictureBox1.Left = panel1.Width - 20;
                }

                if (snake1.myList.ElementAt<Element>(0).pictureBox1.Left > panel1.Width - 20) //Right
                {
                    snake1.myList.ElementAt<Element>(0).pictureBox1.Left = 0;
                }

                if (snake1.myList.ElementAt<Element>(0).pictureBox1.Top < 0) //Top
                {
                    snake1.myList.ElementAt<Element>(0).pictureBox1.Top = panel1.Height - 20;
                }

                if (snake1.myList.ElementAt<Element>(0).pictureBox1.Top > panel1.Height - 20) //Down
                {
                    snake1.myList.ElementAt<Element>(0).pictureBox1.Top = 0;
                }

                //We check if the snake eats his self or touches a wall. 
                // If it is the case we loose
                if (snake1.detecteCorps() == true || walls.detecteWall(snake1) == true)
                {
                    bool best_score = false;
                    test = 0;
                    Program.reset = 0;
                    Program.wait = 1;
                    Form1 formPerdu = new Form1(best_score);
                    formPerdu.Show();
                    this.label1.Text = "0:0:0";

                    if (onePlayer == false)
                    {
                        f.sendInfo("I'm dead");
                        alreadyReset = true;
                    }
                }

                //We check if the snake has eaten the apple
                if (snake1.myList.ElementAt<Element>(0).pictureBox1.Left == apple1.pictureBox1.Left && snake1.myList.ElementAt<Element>(0).pictureBox1.Top == apple1.pictureBox1.Top)
                {
                    //change the position of the apple
                    apple1.changePosition(snake1, walls, this.panel1.Width, this.panel1.Height);
                    
                    score++;
                    this.label3.Text = score.ToString();
                    //Gives the new location of the new element that is added to the snake
                    int x = snake1.myList.ElementAt<Element>(snake1.myList.Count - 1).pictureBox1.Left;
                    int y = snake1.myList.ElementAt<Element>(snake1.myList.Count - 1).pictureBox1.Top;
                    snake1.myList.Insert(snake1.myList.Count - 1, new Element(x, y, GameResource.img1, sizeElem, panel1));
                }

            }
            if (Program.wait == 0)
                this.label1.Text = timerD.Elapsed.Hours.ToString() + ":" + timerD.Elapsed.Minutes.ToString() + ":" + timerD.Elapsed.Seconds.ToString();
            else
                this.label1.Text = "0:0:0";
            
            if (Program.reset == 1)
            {
                this.ResetGame();
                Program.reset = 0;
                Program.wait = 0;
            }
        }

        

        /// <summary>
        /// Updates the data of the adv when he has finished to deserialize the packet received
        /// </summary>
        /// <param name="donneesJeuAdv">All the data about the game of the adv received</param>
        private void updateAdv(Score_xml donneesJeuAdv)
        {
            if (donneesJeuAdv.obj.myList != null)
            {
                for (int i = 0; i < donneesJeuAdv.obj.myList.Count; i++)
                {
                    if(i<snakeAdv.myList.Count){
                        snakeAdv.myList.ElementAt(i).pictureBox1.Left = donneesJeuAdv.obj.myList.ElementAt(i).x/2;
                        snakeAdv.myList.ElementAt(i).pictureBox1.Top = donneesJeuAdv.obj.myList.ElementAt(i).y/2;
                    }
                    else
                         snakeAdv.myList.Add(new Element(donneesJeuAdv.obj.myList.ElementAt(i).x/2, donneesJeuAdv.obj.myList.ElementAt(i).y/2, GameResource.img2, sizeElem/2, panel3));
                }
            }
            if (donneesJeuAdv.obj.myWalls != null)
            {
                for (int i = 0; i < donneesJeuAdv.obj.myWalls.Count; i++)
                {
                    if (wallsAdv.myWalls.Count < i+1)
                        wallsAdv.myWalls.Add(new Element(donneesJeuAdv.obj.myWalls.ElementAt(i).x/2, donneesJeuAdv.obj.myWalls.ElementAt(i).y/2, GameResource.wall2, sizeElem/2, panel3));
                }
            }
            if (donneesJeuAdv.obj.myApple != null)
            {
                appleAdv.pictureBox1.Left = donneesJeuAdv.obj.myApple.x / 2;
                appleAdv.pictureBox1.Top=donneesJeuAdv.obj.myApple.y / 2;
                Console.WriteLine("APPLE x "+donneesJeuAdv.obj.myApple.x / 2+" y "+donneesJeuAdv.obj.myApple.y / 2);
            }
            if(donneesJeuAdv.obj.score>scoreAdv){
                for(int i=0;i<donneesJeuAdv.obj.score-scoreAdv;i++){
                     walls.addWall(snake1, GameResource.wall, sizeElem, panel1);
                }
            }
                scoreAdv = donneesJeuAdv.obj.score;
                label4.Text = scoreAdv.ToString();
        }

        private void player1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            onePlayer = true;

            initializeGame(0);

            timer1.Interval = 100;
        }

        

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool best_score = false;
            test = 0;
            Program.reset = 0;
            Program.wait = 1;
            Form1 formPerdu = new Form1(best_score);
            formPerdu.Show();
            this.label1.Text = "0:0:0";

            if (onePlayer == false)
            {
                alreadyReset = true;
                f.sendInfo("I'm dead");
            }           
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (onePlayer == false)
            {
                //RAJOUTE XML
                string str = donneesJeuNous.Serialize(score, snake1.myList, walls.myWalls, apple1);
                f.sendInfo(str);
                if (f.xml.Contains("<?xml version=\"1.0\" encoding=\"utf-16\"?>"))
                {
                    donneesJeuAdv.Deserialize(f.xml);
                }
                else if (f.xml.Contains("I'm dead"))
                {
                    bool best_score = true;
                    test = 0;
                    Program.reset = 0;
                    Program.wait = 1;
                    Form1 formPerdu = new Form1(best_score);
                    formPerdu.Show();
                    this.label1.Text = "0:0:0";
                    alreadyReset = true;
                }

                if (donneesJeuAdv != null)
                    this.updateAdv(donneesJeuAdv);
            }
        }

        private void player1ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
            onePlayer = false;          

            initializeGame(1);

            timer1.Interval = 100;
            timer2.Interval = 100;
        
        }

        private void player2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            onePlayer = false;

            initializeGame(2);

            timer1.Interval = 100;
            timer2.Interval = 100;
        }
    }
}
