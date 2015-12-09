using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TestXNA.Network_Component
{
    public class Network
    {
        public Objects.Item[][] map = new Objects.Item[10][];
        public Network()
        {
            for (int i = 0; i < map.Length; i++)
            {
                map[i] = new Objects.Item[10];
            }
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Objects.Item item = new Objects.Item();
                    item.type = 0;
                    map[i][j] = item;
                }
            }
        }
        public void setTime(String x, String y, int val)
        {
            Thread.Sleep(val);
            map[Int32.Parse(x)][Int32.Parse(y)].settype(0);
            
        }
        public void StartConnection()
        {
            //createNetwork();

            NetworkStream net = null;
            TcpClient conn = null;
            String msg = null;
            try
            {

                Console.WriteLine(" StartCommunication() method is calling.........");

                String command = "JOIN#";
                conn = new TcpClient();
                // Connects the client to a remote TCP host using the specified host name and port number
                conn.Connect("localhost", 6000);
                //Returns the NetworkStream used to send and receive data
                net = conn.GetStream();
                ASCIIEncoding encode = new ASCIIEncoding();
                byte[] bytes = encode.GetBytes(command);
                net.Write(bytes, 0, bytes.Length);
                Console.WriteLine("\n Join Command Sent To The Server");


            }
            catch (Exception error)
            {
                Console.WriteLine(" Error Message: " + error.StackTrace);
            }
            finally
            {
               /* new Thread(() =>
                {
                    while (true)
                    {
                        String massage = fr.GetMsg();
                        if (massage != null)
                        {
                            TcpClient conn1 = new TcpClient();
                            conn1.Connect("localhost", 6000);
                            NetworkStream net1 = conn1.GetStream();
                            ASCIIEncoding encode = new ASCIIEncoding();

                            byte[] Nbytes = encode.GetBytes(massage);
                            net1.Write(Nbytes, 0, Nbytes.Length);
                            fr.SetMsg();
                            if (massage.Equals("SHOOT#"))
                            {
                                fr.setInfo_textbox("StatusTextbox", "Shooting...");
                                Thread backgroundThread = new Thread(() => setStatusbar(1500));
                                backgroundThread.Start();
                            }
                            if (net1 != null && conn1 != null)
                            {
                                net1.Close();
                                conn1.Close();
                            }

                        }
                    }

                }).Start();*/
                new Thread(() =>
                {
                    if (net != null)
                    {
                        net.Close();
                        //handleMessages
                        TcpListener server_listner = new TcpListener(IPAddress.Any, 7000);
                        server_listner.Start();
                        Byte[] bytes = new Byte[1000];


                        while (true)
                        {
                            TcpClient Server = server_listner.AcceptTcpClient();
                            NetworkStream stream = Server.GetStream();
                            msg = System.Text.Encoding.ASCII.GetString(bytes, 0, stream.Read(bytes, 0, bytes.Length));
                            Console.WriteLine(msg + "\n");
                            EncodeMsg(msg);
                            Server.Close();
                            stream.Close();
                        }

                    }
                }).Start();

                if (conn != null) conn.Close();
            }
        }

        public void EncodeMsg(String msg)
        {
        char ltr=msg[msg.Length-1];
            if(ltr.Equals('#')){
           msg = msg.Remove(msg.Length - 1);}
            char index = msg[0];
            if (index.Equals('I') && !(msg.Equals("INVALID_CELL")))
            {
                Console.WriteLine("*******************************************************************\n");
                Console.WriteLine("Game Instance Received.......\n");
                String[] parts = msg.Split(':');
                //Get brick co-ordinates
                String type = "";
                for (int l = 1; l < 4; l++)
                {
                    if (l == 1) { type = "brick"; }
                    else if (l == 2) { type = "stone"; }
                    else if (l == 3) { type = "water"; }
                    Console.WriteLine(type + " Co-ordinates are........." + "\n");
                    parts[l + 1] = parts[l + 1].Replace(',', ';');
                    String[] bricks = parts[l + 1].Split(';');
                    String[] BrickX = new String[bricks.Length / 2];
                    String[] BrickY = new String[bricks.Length / 2];

                    int j = 0, k = 0;

                    for (int i = 0; i < bricks.Length; i = i + 2)
                    {

                        BrickX[j] = bricks[i];
                        j++;
                    }
                    for (int i = 1; i < bricks.Length; i = i + 2)
                    {
                        if (bricks[i] != null)
                        {
                            BrickY[k] = bricks[i];
                            k++;
                        }
                    }
                    for (int p = 0; p < bricks.Length/2; p++)
                    {
                        //Objects.Item item1 = new Objects.Item();
                        //item1.settype(l);
                        //item1.setX_cor(BrickX[p]);
                       // item1.setX_cor(BrickY[p]);
                      
                        map[Int32.Parse(BrickX[p])][Int32.Parse(BrickY[p])].settype(l);

                        
                    }
                    for (int i = 0; i < bricks.Length / 2; i++)
                    {
                        Console.WriteLine(type + " " + (i + 1) + ": " + "X--> " + BrickX[i] + "   Y--> " + BrickY[i]);
                       
                            //fr.settext(BrickX[i],BrickY[i],type);
                          
                    }
                    
                    Console.WriteLine("\n");
                }
                Console.WriteLine("*******************************************************************\n");
            }
            else if (index.Equals('S'))
            {
                Console.WriteLine("*******************************************************************\n");
                Console.WriteLine("Your Details Received.......\n");

                String[] parts = msg.Split(';');
                String[] playerno = parts[0].Split(':');

                Console.WriteLine("Player Number Is: " + parts[0] + "\n");
                //play = playerno[1];
                String[] cor = parts[1].Split(',');
                Console.WriteLine("Your Position Cordinates Are: " + "X--> " + cor[0] + " Y--> " + cor[1] + "\n");
                map[Int32.Parse(cor[0])][Int32.Parse(cor[1])].settype(4);
                String dir = null;
                if (parts[2].Equals("0")) { dir = "North"; }
                else if (parts[2].Equals("1")) { dir = "East"; }
                else if (parts[2].Equals("2")) { dir = "South"; }
                else if (parts[2].Equals("3")) { dir = "West"; }
                Console.WriteLine("Your Direction Is: " + dir + "\n");
                Console.WriteLine("*******************************************************************\n");

            }
            else if (index.Equals('C') && !(msg.Equals("CELL_OCCUPIED")))
            {
                Console.WriteLine("*******************************************************************\n");
                Console.WriteLine("Guys...Coins Appeared.......\n");
                String[] parts = msg.Split(':');
                String[] cor = parts[1].Split(',');
                Console.WriteLine("Coin Cordinates Are: " + "X--> " + cor[0] + " Y--> " + cor[1] + "\n");
                Console.WriteLine("Time For The Coins to Disappear: " + parts[2] + "\n");
                Console.WriteLine("Value Of The Coins: " + parts[3] + "\n");
                String val = "Coins: " + parts[3];
                map[Int32.Parse(cor[0])][Int32.Parse(cor[1])].settype(5);
                int exp = Int32.Parse(parts[2]);
                Thread backgroundThread = new Thread(() => setTime(cor[0], cor[1], exp));
                backgroundThread.Start();
                Console.WriteLine("*******************************************************************\n");
            }
            else if (index.Equals('L'))
            {
                Console.WriteLine("*******************************************************************\n");
                Console.WriteLine("Guys...Life Pack Appeared.........\n");
                String[] parts = msg.Split(':');
                String[] cor = parts[1].Split(',');
                Console.WriteLine("Life Pack Cordinates Are: " + "X--> " + cor[0] + " Y--> " + cor[1] + "\n");
                Console.WriteLine("Time For The Life Pack to Disappear: " + parts[2] + "\n");
                map[Int32.Parse(cor[0])][Int32.Parse(cor[1])].settype(6);
                int exp = Int32.Parse(parts[2]);
                Thread backgroundThread = new Thread(() => setTime(cor[0], cor[1], exp));
                backgroundThread.Start();
                Console.WriteLine("*******************************************************************\n");
            }
}
    }
}
