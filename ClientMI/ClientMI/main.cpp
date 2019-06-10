#include <iostream>
#include <string>
//#include"AgentInterface.h"
#include"AgentClient.h"
#include <WS2tcpip.h>
#pragma comment(lib, "ws2_32.lib")
using namespace std;



void main()//test
{




	string ipAddress = "127.0.0.1";			// IP Address of the server
	int port = 8090;
	AgentClient agentClient(ipAddress,port); //recieves initial message 
	
	while(true)
	{
		string move;
		cout<<"enter move/promotion: ";
		cin>>move;
		agentClient.sendMove(move);
		agentClient.sendPromotion(move);

		messageType msgType;

		string msg;

		msg=agentClient.recevMessage(msgType);

		cout<<"msgType: "<<msgType<<endl;
		cout<<"msg: "<<msg<<endl;
	}

}
