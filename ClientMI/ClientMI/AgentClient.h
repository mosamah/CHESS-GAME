#include <iostream>
#include <string>
#include <WS2tcpip.h>
#include <algorithm>
#pragma comment(lib, "ws2_32.lib")
using namespace std;


enum messageType
{
    Move = 2,
    Promotion = 6,
    Winlose = 3,
	RemainingTime=5,
	InvalidMove=7,
    PlayerDisconnected = 10
};

 enum color
{
	black=0,
	white=1
};

class AgentClient
{

public:

	string ipAddress ;			// IP Address of the server
	int port;						// Listening port # on the server
	SOCKET sock;
	int turn;
	int agentColor;
	string initialBoardRep;
	int mode; //0--human vs human ------ 1-- AI vs AI

	AgentClient(string ip,int por )
	{
		port=por;
		ipAddress=ip;
		bool isConnnected=connectToServer(ipAddress,port,sock);
		while (isConnnected==false)
		{
			isConnnected=connectToServer(ipAddress,port,sock);
		}
		//hna harecieve initial message
		/*
		char buf[4096];

		ZeroMemory(buf, 4096);
		int bytesReceived = recv(sock, buf, 4096, 0);

		while(bytesReceived<=0)
		{
			bytesReceived = recv(sock, buf, 4096, 0);
		}

		int opCode = (buf[3] << 24) | (buf[2] << 16) | (buf[1] << 8) | (buf[0]);

		if(opCode==12)
		{
			
                turn = (buf[7] << 24) | (buf[6] << 16) | (buf[5] << 8) | (buf[4]);

				

                int message_size =(buf[11] << 24) | (buf[10] << 16) | (buf[9] << 8) | (buf[8]);
				cout<<"msgsize: "<<message_size<<endl;

				

                for (int i = 0; i < message_size; i++)
                {
                    initialBoardRep[i] = buf[i + 12];
                }

				cout<<"initial board: "<<initialBoardRep;
				int space = initialBoardRep.find(" ");
				if(turn==0)
                {
					if(initialBoardRep[space+1]=='w')
						agentColor=black;
					else
						agentColor=white;
                }
                else if(turn==1)
                {
					if(initialBoardRep[space+1]=='w')
						agentColor=white;
					else
						agentColor=black;
                }
				cout<<"myCOLOR: "<<agentColor<<endl;


				mode= (buf[message_size + 16] << 24) | (buf[message_size + 15] << 16) | (buf[message_size + 14] << 8) | (buf[message_size + 13]);
				cout<<"Mode: "<<mode<<endl;

				

		}
		*/
	}


	string recevMessage( messageType &type) //blocking
        {

			char buf[4096];

			ZeroMemory(buf, 4096);


			int bytesReceived = recv(sock, buf, 4096, 0);

			while(bytesReceived<=0)
			{
				bytesReceived = recv(sock, buf, 4096, 0);
			}

			int opCode = (buf[3] << 24) | (buf[2] << 16) | (buf[1] << 8) | (buf[0]);


			string msg;
			int msgIndex=0;
			for(int i=4;i<bytesReceived;i++)
			{
				msg.push_back(buf[i]);
			}

            type = (messageType)opCode;
			
			return msg;
        }


	bool connectToServer(string ipAddress,int port,SOCKET &sock)
	{
		// Initialize WinSock
		WSAData data;
		WORD ver = MAKEWORD(2, 2);
		int wsResult = WSAStartup(ver, &data);
		if (wsResult != 0)
		{
			cerr << "Can't start Winsock, Err #" << wsResult << endl;
			return false;
		}

		// Create socket
		sock = socket(AF_INET, SOCK_STREAM, 0);
		if (sock == INVALID_SOCKET)
		{
			cerr << "Can't create socket, Err #" << WSAGetLastError() << endl;
			WSACleanup();
			return false;
		}

		// Fill in a hint structure
		sockaddr_in hint;
		hint.sin_family = AF_INET;
		hint.sin_port = htons(port);
		inet_pton(AF_INET, ipAddress.c_str(), &hint.sin_addr);

		// Connect to server
		int connResult = connect(sock, (sockaddr*)&hint, sizeof(hint));
		if (connResult == SOCKET_ERROR)
		{
			cerr << "Can't connect to server, Err #" << WSAGetLastError() << endl;
			closesocket(sock);
			WSACleanup();
			return false;
		}
		return true;
	}


	bool sendMove(string move) //move in the form like"A1B1"
{
	/* //question mhtag acheck wla la2 ??
	if(move.size()==4)//check if valid format or not  
	{
		if(move[0]=='A'|
	}
	else
		return false; //invalid format
	*/
	//byte opcode[8] = { 0x02, 0x00, 0x00, 0x00 ,0x01, 0x00, 0x00, 0x00 };
	if(move.length()!=4)
	{
		cout<<"INVALID MOVE LENGTH CANNOT SSEND!!"<<endl;
		return false;
	}
	char message[8]= {0x02, 0x00, 0x00, 0x00 ,move[0],move[1],move[2],move[3] }; 
	
	int sendResult = send(sock,message, 8, 0);
	while(sendResult == SOCKET_ERROR)
	{
		send(sock,message, 8, 0); //Won't exit the loop until the move is sent
	}
	return true;
}


	bool sendPromotion(string prom) //move in the form like"A1B1Q"/"A1B1q" -> q means black queen / Q means white queen [Case Sensitive]
{
	if(agentColor==white)
		transform(prom.begin(), prom.end(),prom.begin(), ::toupper);
	else
		transform(prom.begin(), prom.end(),prom.begin(), ::tolower);

	if(prom.length()!=5)
	{
		cout<<"INVALID PROMOTION LENGTH CANNOT SSEND!!"<<endl;
		return false;
	}
	char message[9]= {0x06, 0x00, 0x00, 0x00 ,prom[0],prom[1],prom[2],prom[3],prom[4] }; 
	string final="2"+prom;  // add opcode char 2 to the move to be like "2A1B1"
	//int sendResult = send(sock,final.c_str(), final.size()+1, 0);
	int sendResult = send(sock,message, 9, 0);
	while(sendResult == SOCKET_ERROR)
	{
		send(sock,message, 9, 0); //Won't exit the loop until the move is sent
	}
	return true;
}

	

	~AgentClient()
	{
		closesocket(sock);
		WSACleanup();
	}
};

