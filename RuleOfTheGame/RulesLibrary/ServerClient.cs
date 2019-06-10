using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ClassLibrary5
{
    public enum messageType
    {
        move = 2,
        promotion = 6,
        winlose = 3,
        PlayerDisconnected = 10

    };
    public class ServerClient
    {
        public int Turn;
        public string mycolor; //according to the server initial state
        public int Mode; //0 human vs human -----  1 AI vs AI
        public String BoardRep;
        private static TcpClient server = null;
        private static NetworkStream ns = null;
        byte[] data = new byte[1024];

        //added
        public string initialMsg;
        public string msgWithOp;
        public int initialMsgSize;

        public ServerClient(string ipAddress,int port)
        {
            //byte[] data = new byte[1024];
            byte[] int_data = new byte[4];

            while (server == null)
            {
                try
                {
                    server = new TcpClient(ipAddress, port);
                    Console.WriteLine("Connected");

                }

                catch (SocketException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Unable to connect to server");
                }
            }
            
            ns = server.GetStream();
            try
            {
                int recv = ns.Read(data, 0, data.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }

            int opcode = BitConverter.ToInt32(data, 0);
            Console.WriteLine(opcode.ToString());

            
            if (opcode == 12)
            {

                Turn = BitConverter.ToInt32(data, 4);
                /*
                if (Turn==0)
                {
                    mycolor = "black";
                }
                else
                {
                    mycolor = "white";
                }
                */
                Console.WriteLine(Turn.ToString());

                int message_size = BitConverter.ToInt32(data, 8);
                Console.WriteLine(message_size.ToString());

                byte[] init_byte = new byte[message_size];

                for (int i = 0; i < message_size; i++)
                {
                    init_byte[i] = data[i + 12];
                }

                BoardRep = Encoding.ASCII.GetString(init_byte, 0, message_size);
                Console.WriteLine(BoardRep);


                int space = BoardRep.IndexOf(" ");
                if (Turn == 0)
                {
                    if (BoardRep[space + 1] == 'w')
                        mycolor = "black";
                    else
                        mycolor = "white";
                }
                else if (Turn == 1)
                {
                    if (BoardRep[space + 1] == 'w')
                        mycolor = "white";
                    else
                        mycolor = "black";
                }

                Mode = BitConverter.ToInt32(data, message_size + 12);
                Console.WriteLine(Mode.ToString());

                initialMsgSize = message_size + 12 + 4;
                initialMsg= Encoding.ASCII.GetString(data, 0, initialMsgSize);

            }
        }

        ~ServerClient()
        {
            ns.Close();
            server.Close();
        }

        //gets turn for first time play
        public int getTurn()
        {
            return Turn;
        }
        public NetworkStream getStream()
        {
            return ns;
        }
        public bool sendMove(String move)
        {
            if (move.Length != 4)
                return false;

            byte[] opcode = { 0x02, 0x00, 0x00, 0x00 };

            int index = 4;
            byte[] final = new byte[4 + move.Length];
            final[0] = opcode[0];
            final[1] = opcode[1];
            final[2] = opcode[2];
            final[3] = opcode[3];
            byte[] bytes = Encoding.ASCII.GetBytes(move);
            for (int i = 0; i < move.Length; i++)
            {
                final[index] = bytes[i];
                index++;
            }

            ns.Write(final, 0, final.Length);
            ns.Flush();
            return true;
        }


        public bool sendPromotion(String pro)
        {
            byte[] opcode = { 0x06, 0x00, 0x00, 0x00 };

            if (mycolor == "white")
                pro = pro.ToUpper();
            else
                pro = pro.ToLower();

            if (pro.Length == 5)
            {
                int index = 4;
                byte[] final = new byte[4 + pro.Length];
                final[0] = opcode[0];
                final[1] = opcode[1];
                final[2] = opcode[2];
                final[3] = opcode[3];
                byte[] bytes = Encoding.ASCII.GetBytes(pro);
                for (int i = 0; i < pro.Length; i++)
                {
                    final[index] = bytes[i];
                    index++;
                }
                ns.Write(final, 0, final.Length);
                ns.Flush();
                return true;
            }
            return false;
        }

        public int getRemainingTime()
        {
            byte[] remtime_opcode = { 0x05, 0x00, 0x00, 0x00 };
            ns.Write(remtime_opcode, 0, 4);
            ns.Flush();
            while(ns.DataAvailable==false)
            {

            }
            ns.Read(data, 0, data.Length);
            return BitConverter.ToInt32(data, 0);
        }
        //public messageType MessageType()
        //{

        //    ns.Read(data, 0, data.Length);
        //    int opcode = BitConverter.ToInt32(data,0);
        //    return (messageType)opcode;   
        //}

        public String recevMessage(ref messageType type)
        {
            while(ns.DataAvailable==false)
            {
                //waiting for data to be ready by the server
            }
            int recev = ns.Read(data, 0, data.Length);
            int opcode = BitConverter.ToInt32(data, 0);
            string temp = Encoding.ASCII.GetString(data, 4, recev - 4);
            type = (messageType)opcode;
            return temp;
        }
        /*
        static void Main()
        {
            String message = null;
            Program m1 = new Program();
            messageType type = 0;
            while (true)
            {
                if (m1.getTurn() == 1)
                {
                    message = Console.ReadLine();
                    m1.sendMove(message);
                    message = m1.recevMessage(ref type);
                    Console.WriteLine(message);
                    Console.WriteLine(type);
                    message = Console.ReadLine();
                    m1.sendPromotion(message);
                    message = m1.recevMessage(ref type);
                    Console.WriteLine(message);
                    Console.WriteLine(type);
                    int x = m1.getRemainingTime();
                    Console.WriteLine(x.ToString());

                }
                else
                {
                    message = m1.recevMessage(ref type);
                    Console.WriteLine(message);
                    Console.WriteLine(type);
                    message = Console.ReadLine();
                    m1.sendMove(message);
                    message = m1.recevMessage(ref type);
                    Console.WriteLine(message);
                    Console.WriteLine(type);
                    message = Console.ReadLine();
                    m1.sendPromotion(message);
                    int x = m1.getRemainingTime();
                    Console.WriteLine(x.ToString());

                }

            }

        }

        */
    }
}
