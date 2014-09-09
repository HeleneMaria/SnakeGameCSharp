using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;

namespace snake
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ObjectToSerialize
    {
        public int score; //The value of the best score is serialized only of it is bigger than the one already in the xml
        //We replace the array of the snake elements and walls by another that only contains the position x & y of the elements
        public List<Position> myList;
        public List<Position> myWalls;
        public Position myApple;

        /// <summary>
        /// Construtor by default of the class Object to serialize
        /// </summary>
        public ObjectToSerialize()
        {
            score = 0;

            myList = new List<Position>();
            myWalls = new List<Position>();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Score_xml
    {
        public String pathFile;
        public ObjectToSerialize obj;

        /// <summary>
        /// Constructor of the class Score_xml
        /// </summary>
        /// <param name="pathFile">Path of the file where the data will be saved</param>
        public Score_xml(String pathFile)
        {
            obj = new ObjectToSerialize();
            this.pathFile = pathFile;
        }

        /// <summary>
        /// Constructor by default of the class Score_xml
        /// </summary>
        public Score_xml() { }

        /// <summary>
        /// Serializes the data of the user before sending them to adv
        /// </summary>
        /// <param name="score">Score of the user</param>
        /// <param name="myList">List of the snake Elem related to the snake of the user</param>
        /// <param name="myWalls">List of the walls displayed on the user's panel</param>
        /// <param name="myApple">Apple displayed on the user's panel</param>
        /// <returns>String with the data serialized</returns>
        public string Serialize(int score, List<Element> myList, List<Element> myWalls, Apple myApple) //intValue1 = score.ToString() time1 = this.label1.Text = timerD.Elapsed.Hours.ToString() + ":" + timerD.Elapsed.Minutes.ToString() + ":" + timerD.Elapsed.Seconds.ToString();
        {
            obj = new ObjectToSerialize();

            obj.score = score;
            for (int i = 0; i < myList.Count; i++)
            {
                obj.myList.Add(new Position(myList.ElementAt(i).pictureBox1.Left, myList.ElementAt(i).pictureBox1.Top));
            }
            for (int i = 0; i < myWalls.Count; i++)
            {
                obj.myWalls.Add(new Position(myWalls.ElementAt(i).pictureBox1.Left, myWalls.ElementAt(i).pictureBox1.Top));
            }
            obj.myApple = new Position( myApple.pictureBox1.Left, myApple.pictureBox1.Top);

            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(obj.GetType());
            
            StringWriter sw =  new StringWriter();
            x.Serialize(sw, obj);
            return sw.ToString();
        }

        /// <summary>
        /// Deserialize the data received from the adv
        /// </summary>
        /// <param name="str">String with all the data grouped in it</param>
        public void Deserialize(String str)
        {
            var serializer = new XmlSerializer(obj.GetType());
            object result;
            using (TextReader reader = new StringReader(str))
            {
                result = serializer.Deserialize(reader);
            }
            obj = (ObjectToSerialize)result;
        }
    }
}
