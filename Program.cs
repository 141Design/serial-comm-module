using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace SerialComm
{
    class Program
    {
        static void Main(string[] args)
        {
            SerialCommModule scm = new SerialCommModule();

            var portList = scm.GetPortList();
            foreach(string port in portList)
            {
                Console.WriteLine(port);
            }

            scm.OpenPort("COM3", 9600);
            var data = new List<Byte>();
            data.Add((Byte)'@');
            scm.ByteWrite(data);

            data = new List<Byte>();
            while(data.Count == 0)
            {
                data = scm.ByteRead();
            }
            foreach (byte dat in data){
                Console.Write("{0},",dat);
            }

            Console.WriteLine("");
            scm.Clear();
            
            scm.ClosePort();
        }
    }
}
