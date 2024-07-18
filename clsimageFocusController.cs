using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;

namespace WindowsFormsApp5
{
    public class ClsimageFocusController
    {

        public SerialPort _serialPort = null;

        public const char STX = (char)0x02;
        public const char CR = (char)0x0D;
        public const char LF = (char)0x0A;
        public const char V = (char)0x56;
        public const char W = (char)0x57;
        public const char O = (char)0x4F;
        public const char F = (char)0x46;


        public bool bConnect = false;


        public ClsimageFocusController()
        {
            _serialPort = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);
        }

        public ClsimageFocusController(string port, int baudRate)
        {
            _serialPort = new SerialPort(port, baudRate, Parity.None, 8, StopBits.One);
        }

        public bool Connect()
        {
            try
            {
                _serialPort.Open();

                if (_serialPort.IsOpen)
                {
                    bConnect = true;
                    return true;
                }
                else
                {
                    bConnect = false;
                    return false;
                }

               
            }
            catch
            {

                bConnect = false;

                return false;
            }
        }

        

        public void SetValue(int nChannel, int nValue, int count = 1)
        {
            try
            {

                if (_serialPort.IsOpen)
                {
                    //string strSendMsg = string.Format("{0:X2}01", nChannel); // 01 = 시작번지 채널, 01 = 채널 개수
                    string strChannel = nChannel.ToString("X2");
                    string strCount = count.ToString("X2");
                    string strValue = nValue.ToString("X2");

                    string message = "\x02" + "\x56" + "\x57" + strChannel + strCount + strValue + "\x0D" + "\x0A";
                    _serialPort.Write(message);

                    Thread.Sleep(100);
                }
                
            }
            catch
            {

            }
        }

        public void AllOnOff(bool OnOff, int count)
        {
            try
            {
                if (_serialPort.IsOpen)
                {
                    int startch = 1;
                    int chOn = 1;
                    int chOff = 0;

                    string strChannel = startch.ToString("X2");
                    string strCount = count.ToString("X2");
                    string strData = string.Empty;
                    string message = null;

                    // OnOff 값에 따라 데이터 문자열을 생성
                    for (int i = 0; i < count; i++)
                    {
                        strData += (OnOff ? chOn : chOff).ToString("X2");
                    }

                    message = "\x02" + "\x4F" + "\x57" + strChannel + strCount + strData + "\x0D" + "\x0A";
                    _serialPort.Write(message);

                    Thread.Sleep(100);
                }
            }
            catch
            {

            }
        }

        public void ChannelOnOff(bool OnOff, int nChannel)
        {
            try
            {
                if (_serialPort.IsOpen)
                {
                    int startch = 1;
                    int chOn = 1;
                    int chOff = 0;

                    string strChannel = startch.ToString("X2");
                    string strCount = nChannel.ToString("X2");
                    string strData = string.Empty;
                    string message = null;

                    strData += (OnOff ? chOn : chOff).ToString("X2");

                    message = "\x02" + "\x4F" + "\x57" + strChannel + strCount + strData + "\x0D" + "\x0A";
                    _serialPort.Write(message);

                    Thread.Sleep(100);
                }
            }
            catch
            {

            }
        }

    }
}
