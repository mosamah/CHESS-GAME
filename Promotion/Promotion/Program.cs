using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promotion
{
    class Program
    {
        static void Main(string[] args)
        {
            promotion voiceObj = new promotion();
            voiceObj.setup();
            int proc = Console.In.Read();
            while (proc != -1)
            {
                if (proc == '1')
                {
                    voiceObj.recording = true;
                }
                proc = Console.In.Read();
            }
        }
    }
}
