using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wetest.functions
{
    public class Functions
    {
        
        public static string ChangeCounttoId(long count)
        {
            string temp = count.ToString();
            if (temp.Length < 8)
            {
                int n = 8 - temp.Length;
                for(int i = 1; i <= n; i++)
                {
                    temp = '0' + temp;
                }
            }
            return temp;
        }
    }
}
