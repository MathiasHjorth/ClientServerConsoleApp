using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

ExecuteClient();

static void ExecuteClient()
{

    try
    {
        IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddr = ipHost.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

        Socket sender = new Socket(ipAddr.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

        try
        {
            sender.Connect(localEndPoint);

            Console.WriteLine($"Socket connected {sender.RemoteEndPoint.ToString()}");

            byte[] messageSent = Encoding.ASCII.GetBytes("Test Client<EOF>");
            int byteSent = sender.Send(messageSent);

            byte[] messageRecieved = new byte[1024];
            int byteRecieved = sender.Receive(messageRecieved);
            Console.WriteLine($"Message from server {Encoding.ASCII.GetString(messageRecieved,0, byteRecieved)}");

            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }
        catch (ArgumentNullException an)
        {
            Console.WriteLine("Argument Null Exception {0}", an.ToString());
        }
        catch (SocketException se)
        {
            Console.WriteLine("Socket Exception {0}", se.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine("Unexpected exception : {0}", e.ToString());
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.ToString());
    }

}


