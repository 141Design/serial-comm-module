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
            
            if (serialPort.IsOpen)
            {
                var recvData = new Byte[1];
                while (recvData[0] != delimiter[0] && serialPort.ReadBufferSize != 0)
                {
                    serialPort.Read(recvData, 0, 1);
                    retData.Add(recvData[0]);
                }
            }
            return retData;
        }

        //入出力バッファをクリアする
        public int Clear()
        {
            if (serialPort.IsOpen)
            {
                serialPort.DiscardInBuffer();
                serialPort.DiscardOutBuffer();
                return 0;
            }
            return -1;
        }

        //入力バッファをクリアする
        public int ClearInBuffer()
        {
            if (serialPort.IsOpen)
            {
                serialPort.DiscardInBuffer();
            }
                return 0;
        }

        //出力バッファをクリアする
        public int ClearOutBuffer()
        {
            if (serialPort.IsOpen)
            {
                serialPort.DiscardOutBuffer();
            }
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
