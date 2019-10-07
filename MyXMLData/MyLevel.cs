using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyXMLData
{
    public class MyLevel
    {
        

        //The name of the Level
        public string LevelName;
        //Number of enemies at the start of the level
        public int MaxEnemies;
        //Time Limit to complete the level
        public float TimeLimit;

        public string WorldName;

        public struct MyData
        {
            public struct MyName
            {
                public string lastName;
            }
        }

        public MyData Data;

    }
}
