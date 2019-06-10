#include "AgentClient.h"

#pragma once
class AgentInterface
{
public:	
	AgentClient agentClient;	//Object of class AgentClient responsible for sending and recieving
	string guiIp;				//IP of GUI server that is connected to the agent
	int guiPort;				//Port of Agent that GUI listens on
	
	int mode;					//0--Human VS Human \\\\\\ 1--AI VS AI
	int initialTurn;			//0--opponent turn \\\\\\\\ 1--my turn
	int myColor;				//enum color present in AgentClient class (white/black)
	string initialBoardRep;		//initial FEN recieved from the server
	int whoseTurn;				//whose turn me or the opponent
	bool gameTerminated;

	AgentInterface(string ip,int port); //GUI server IP ---- port number default 8090	

	bool setFen(string fen);			//function setting the initial FEN
	void initialize();					//initialize members and setFen
	bool applyMove(string move);		//apply new move to the board
	string requestMove();				//returns best move recommendation to be sent to the GUI
	void play();

	~AgentInterface(void);
};

