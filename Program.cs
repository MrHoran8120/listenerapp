using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace listenerapp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var ipEndPoint = new IPEndPoint(IPAddress.Any, 9950);
            TcpListener listener = new TcpListener  (ipEndPoint);

            

            try
            {
                listener.Start();

                TcpClient handler = listener.AcceptTcpClient ();
                NetworkStream stream = handler.GetStream();
              
                byte[] myReadBuffer = new byte[1024];
                StringBuilder myCompleteMessage = new StringBuilder();
                int numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);

                while (numberOfBytesRead > 0)
                {
                    myCompleteMessage.Append(Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));
                    numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);
                }

                // Print out the received message to the console.
                Console.WriteLine("You received the following message : " + myCompleteMessage);
            }
            finally
            {
                listener.Stop();
            }

            Console.ReadLine();
        }
    }
}
