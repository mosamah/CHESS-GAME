#include "AgentInterface.h"


AgentInterface::AgentInterface(string ip,int port):agentClient(ip,port) 
{
	guiIp=ip;
	guiPort=port;
	initialize();  //sets values of the members (turn / mode / initialBoard /agentColor) and set the initial FEN
}


void AgentInterface::initialize()
{
	mode=agentClient.mode;
	myColor=agentClient.agentColor;
	initialTurn=agentClient.turn;
	initialBoardRep=agentClient.initialBoardRep;
	whoseTurn=initialTurn;
	gameTerminated=false;
	bool isFenValid=setFen(initialBoardRep);

	if(isFenValid==false)
		cout<<"RECIEVED WRONG FEN CAN'T START"<<endl;
}

bool AgentInterface:: setFen(string fen)
{
	//To do
	return true;
}

bool AgentInterface:: applyMove(string move)
{
	//To do
	return true;
}


string AgentInterface:: requestMove()
{
	//To do
	return"";
}

void AgentInterface:: play()
{
	while(gameTerminated==false)
	{
		messageType msgType;
		if(whoseTurn==0) //opponent turn
		{			
			string msg= agentClient.recevMessage(msgType);
			int type=(int)msgType;
			if(type==Move || type==Promotion) //move or promotion
			{
				applyMove(msg);
			}
			else if(type==PlayerDisconnected)	   //player disconnected
			{
				cout<<"OPPONENT DISCONNECTED"<<endl;
				gameTerminated=false;
			}
			else if(type==Winlose)	   //
			{
				gameTerminated=false;
				cout<<"Time out"<<endl;
				if(msg=="L")
				{
					cout<<"WE LOST"<<endl;
				}
				else
				{
					cout<<"WE WON"<<endl;
				}
			}
			
			whoseTurn=1;
		}
		else			//my turn
		{
			string agentMove=requestMove();
			if(agentMove.length()==4)
			{
				agentClient.sendMove(agentMove);
			}
			else if(agentMove.length()==5)
			{
				agentClient.sendPromotion(agentMove);
			}

			if(mode==0)
			{
				string myfinalMove=agentClient.recevMessage(msgType);
				if(msgType==InvalidMove)					  //If agent generated invalid move GUI sends 7 so as to take
				{
					//To do
				}
				else if(msgType==Move || msgType==Promotion) //move or promotion
				{
					applyMove(myfinalMove);
					whoseTurn=0;
				}	
			}
			else
			{
				//hna msh 3amel check 3la elmove valid wla la2a lsa 
				applyMove(agentMove);
			}
		
		}
	}
}

AgentInterface::~AgentInterface(void)
{
}
