using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;

namespace TANK
{
    class GameClient
    {
        private TcpClient client;
        public string ip { set; get; }
        public string data { set; get; }
        private int port_send = 6000; //Data outgoing port
        private int port_recv = 7000; //Data incoming port

        private Boolean recieved = true; //Thread termination condition

        
        private Thread listnerThread;
        
        public static GameClient gameClient { set; get; }
        public GameClient(string ip)
        {
            this.ip = ip;
            listnerThread = new Thread(new ThreadStart(ListenToServer));
        }

        //to send message to the server
        public void SendToServer(string message)
        {
            try
            {
                client = new TcpClient();
                client.Connect(IPAddress.Parse(ip), port_send);
                Stream stream = client.GetStream();

                ASCIIEncoding asciiencode = new ASCIIEncoding();
                byte[] msg = asciiencode.GetBytes(message);

                stream.Write(msg, 0, msg.Length);
                stream.Close();
                client.Close();
                if (message.Equals("JOIN#"))    //starts the game with the command JOIN#
                    listnerThread.Start();
            }
            catch (NullReferenceException e) {
                
            }

           
        }
     
        public void ListenToServer()
        {
            TcpListener listner = new TcpListener(IPAddress.Parse(ip), port_recv);
            TcpClient clientRecieve = null;
            Stream streamRecieve = null;
            while (recieved)
            {
                try
                {
                    listner.Start();
                    clientRecieve = listner.AcceptTcpClient();
                    streamRecieve = clientRecieve.GetStream();
                    Byte[] bytes = new Byte[256];

                    int i;
                    data = null;

                    while ((i = streamRecieve.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    }

                    string[] lines = Regex.Split(data, ":");
                    if (lines[0].StartsWith("S"))
                    {
                        Debug.Log("Game initialized");
                    }
                    else
                    {
                        Debug.Log(data);
                        Debug.Log("\n");
                    }
                }
                catch (Exception e)
                {

                }
                finally
                {
                    streamRecieve.Close();
                    listner.Stop();
                    clientRecieve.Close();
                }  
            }
        }

        public void stopServer()
        {
            recieved = false;
            client.Close();
        }


    }
}
