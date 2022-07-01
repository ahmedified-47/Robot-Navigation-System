using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    class ResourceInitialiser
    {
        private string line, map, startState, endState;
        private System.IO.StreamReader file;
        private List<string> col = new List<string>();

        public ResourceInitialiser(string afile)
        {
            file = new System.IO.StreamReader(afile);
        }
        public string Map => map;

        public string StartState => startState;

        public string EndState => endState;

        public List<string> Col => col;

        public void closeFile()
        {
            file.Close();
        }

        public void populateData()
        {
            int index = 0;
            
            while((line = file.ReadLine()) != null)
            {
                if (index == 0)
                {
                    map = line;
                }
                else if (index == 1)
                {
                    startState = line;
                }
                else if (index == 2)
                {
                    endState = line;
                }
                else if(index == 3)
                {
                    col.Add(line);
                }
                index++;
               
            }
        }

        public void printData()
        {
            Console.WriteLine("world size: {0}", map);
            Console.WriteLine("Start state: {0}", startState);
            Console.WriteLine("End state: {0}", endState);

            foreach (string c in col)
            {
                Console.WriteLine("Column: {0}" , c);
            }
        }

    }
}
