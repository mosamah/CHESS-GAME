using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Net;

namespace ClassLibrary5
{
    public class guiServer
    {
        int requestCount = 0;
        TcpClient clientSocket;
        int port;
        string ipAddress;
        NetworkStream networkStream;
        byte[] data = new byte[1024];


        bool connectToClient(TcpClient clientSocket, int port, string ipAddress)
        {
            try
            {
                TcpListener serverSocket = new TcpListener(IPAddress.Parse(ipAddress), port);
                clientSocket = default(TcpClient);
                serverSocket.Start();
                Console.WriteLine(" >> Server Started");
                clientSocket = serverSocket.AcceptTcpClient();
                networkStream = clientSocket.GetStream();
                Console.WriteLine(" >> Accept connection from client");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            return true;
        }

        public guiServer(string ipAdd, int por)
        {
            port = por;
            ipAddress = ipAdd;
            clientSocket = new TcpClient();

            bool isConnected = connectToClient(clientSocket, port, ipAddress);

            while (isConnected == false) //insure connection
            {
                isConnected = connectToClient(clientSocket, port, ipAddress);
            }
        }

        public void sendOriginal(string msg)
        {
            Byte[] sendBytes = Encoding.ASCII.GetBytes(msg);
            networkStream.Write(sendBytes, 0, sendBytes.Length);
            networkStream.Flush();
        }



        public bool sendMove(String move)
        {

            byte[] opcode = { 0x02, 0x00, 0x00, 0x00 };

            if (move.Length == 4)
            {
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
                networkStream.Write(final, 0, final.Length);
                networkStream.Flush();
                return true;
            }
            return false;
        }


        public bool sendRemainingTime(int time)
        {

            byte[] opcode = { 0x05, 0x00, 0x00, 0x00 };

            int index = 4;
            byte[] final = new byte[8];
            final[0] = opcode[0];
            final[1] = opcode[1];
            final[2] = opcode[2];
            final[3] = opcode[3];
            byte[] bytes = BitConverter.GetBytes(time);
            for (int i = 0; i < 4; i++)
            {
                final[index] = bytes[i];
                index++;
            }
            networkStream.Write(final, 0, final.Length);
            networkStream.Flush();
            return true;
        }


        public bool sendInitial(string init)
        {

            byte[] opcode = { 0x0C, 0x00, 0x00, 0x00 };
            int index = 4;
            byte[] final = new byte[4 + init.Length];
            final[0] = opcode[0];
            final[1] = opcode[1];
            final[2] = opcode[2];
            final[3] = opcode[3];
            byte[] bytes = Encoding.ASCII.GetBytes(init);
            for (int i = 0; i < init.Length; i++)
            {
                final[index] = bytes[i];
                index++;
            }
            networkStream.Write(final, 0, final.Length);

            //networkStream.Write(bytes, 0, bytes.Length);
            networkStream.Flush();
            return true;
        }
        public bool sendPromotion(String pro)
        {
            byte[] opcode = { 0x06, 0x00, 0x00, 0x00 };

            

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
                networkStream.Write(final, 0, final.Length);
                networkStream.Flush();
                return true;
            }
            return false;
        }

        //public messageType MessageType()
        //{

        //    ns.Read(data, 0, data.Length);
        //    int opcode = BitConverter.ToInt32(data,0);
        //    return (messageType)opcode;   
        //}

        public String recevMessage(ref messageType type)
        {

            while (networkStream.DataAvailable == false)
            {
                //waiting for data to be ready by the server
            }
            int recev = networkStream.Read(data, 0, data.Length);
            int opcode = BitConverter.ToInt32(data, 0);
            string temp = Encoding.ASCII.GetString(data, 4, recev - 4);
            type = (messageType)opcode;
            return temp;
        }

    }
}
