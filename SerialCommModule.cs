using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

//シリアルポートクラスを呼び出す→設定する→開始


namespace SerialComm
{
    public class SerialCommModule
    {

        SerialPort serialPort;
        public string delimiter;

        public SerialCommModule()
        {
            delimiter = "\r";
        }

        public int OpenPort(string portName, int baudRate)
        {
            serialPort = new SerialPort();
            serialPort.PortName = portName;
            serialPort.BaudRate = baudRate;
            serialPort.DataBits = 8;
            serialPort.Parity = Parity.None;
            serialPort.StopBits = StopBits.One;
            serialPort.Encoding = Encoding.ASCII;
            serialPort.WriteTimeout = 100000;
            try
            {
                serialPort.Open();
            }
            catch
            {
                Console.WriteLine("Failed to Open SerialPort.");
                return -1;
            }
            return 0;
        }

        public int ClosePort()
        {
            serialPort.Close();
            serialPort = null;
            return 0;
        }

        public int ByteWrite(List<byte> data)
        {
            Byte[] dataArray = data.ToArray();
            if (serialPort.IsOpen)
            {
                serialPort.Write(dataArray, 0, data.Count());
                serialPort.Write(delimiter);
                return 0;
            }
            return -1;
        }

        public List<byte> ByteRead()
        {
            List<byte> retData = new List<byte>();
            Byte[] recvData = new Byte[serialPort.BytesToRead];
            serialPort.Read(recvData, 0, recvData.GetLength(0));
            retData = recvData.ToList();
            return retData;
        }
        public int Clear()
        {
            
            return 0;
        }

        //ポートリストを返す
        public List<string> GetPortList()
        {
            string[] ports = SerialPort.GetPortNames();
            List<string> retPorts = ports.ToList();
            return retPorts;
        }

    }
}
