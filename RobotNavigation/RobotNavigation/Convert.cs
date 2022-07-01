using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RobotNavigation
{
    public class Convert
    {
        private string _string;

        public Convert(string str)
        {
            _string = str;
        }

        public List<int> ToConvert()
        {
            string[] num = Regex.Split(_string, @"\D+");
            List<int> list = new List<int>();
            foreach (string str in num)
            {
                if (!string.IsNullOrEmpty(str)) 
                { 
                    list.Add(int.Parse(str));
                }
            }
            return list;
        }
    }
}
