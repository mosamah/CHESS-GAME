/*
 * using ChessDotNet; // the namespace of Chess.NET
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.ComponentModel;




namespace ClassLibrary
{
    public class Interface
    {
        public static bool opponent_played_invalid_move = false;
        static string mycolour;
        static char promotionChar;
        public static ChessGame Start(string fen,string mycolor)
        {
            //assuming white will always start
            ChessGame game = new ChessGame(fen);
            mycolour = mycolor;
            promotionChar = 'z'; //lesa magash promotion
            return game;
        }

        public static void setPromotionChar(char x)
        {
            promotionChar = x;
        }
        public static string CheckMove(string From, string To, ChessGame game)
        {           
            char c=promotionChar;
            bool valid = false;
            Move FromTo = null;
            if (mycolour == "white" && game.WhoseTurn == Player.White )
            {
                if ((promotionChar!='z') &&(From == "a7" && To == "a8" && game.GetPieceAt(new Position("a7")).GetFenCharacter() == 'P')
                       || (From == "b7" && To == "b8" && game.GetPieceAt(new Position("b7")).GetFenCharacter() == 'P')
                       || (From == "c7" && To == "c8" && game.GetPieceAt(new Position("c7")).GetFenCharacter() == 'P')
                       || (From == "d7" && To == "d8" && game.GetPieceAt(new Position("d7")).GetFenCharacter() == 'P')
                       || (From == "e7" && To == "e8" && game.GetPieceAt(new Position("e7")).GetFenCharacter() == 'P')
                       || (From == "f7" && To == "f8" && game.GetPieceAt(new Position("f7")).GetFenCharacter() == 'P')
                       || (From == "g7" && To == "g8" && game.GetPieceAt(new Position("g7")).GetFenCharacter() == 'P')
                       || (From == "h7" && To == "h8" && game.GetPieceAt(new Position("h7")).GetFenCharacter() == 'P'))


                {
                    //c = (char)Console.Read();
                    FromTo = new Move(From, To, game.WhoseTurn, c);
                    valid = game.IsValidMove(FromTo);
                    promotionChar = 'z';
                }
                else
                {
                    FromTo = new Move(From, To, game.WhoseTurn);
                    valid = game.IsValidMove(FromTo);
                }
                if (valid)
                {
                    MoveType mtype;

                    mtype = game.ApplyMove(FromTo, true);
                    game.InsertMove(game.GetFen());
                    game.IsItDrawByTRR(game.GetFen());


                }

            }else if (mycolour == "black" && game.WhoseTurn == Player.Black)
            {
                //so2al mynfa3sh mn b2 le a1 msln
                if ( (promotionChar != 'z') && (From == "a2" && To == "a1" && game.GetPieceAt(new Position("a2")).GetFenCharacter() == 'p')
                       || (From == "b2" && To == "b1" && game.GetPieceAt(new Position("b2")).GetFenCharacter() == 'p')
                       || (From == "c2" && To == "c1" && game.GetPieceAt(new Position("c2")).GetFenCharacter() == 'p')
                       || (From == "d2" && To == "d1" && game.GetPieceAt(new Position("d2")).GetFenCharacter() == 'p')
                       || (From == "e2" && To == "e1" && game.GetPieceAt(new Position("e2")).GetFenCharacter() == 'p')
                       || (From == "f2" && To == "f1" && game.GetPieceAt(new Position("f2")).GetFenCharacter() == 'p')
                       || (From == "g2" && To == "g1" && game.GetPieceAt(new Position("g2")).GetFenCharacter() == 'p')
                       || (From == "h2" && To == "h1" && game.GetPieceAt(new Position("h2")).GetFenCharacter() == 'p'))


                {
                    //c = (char)Console.Read(); //eh di ya broo ??
                    FromTo = new Move(From, To, game.WhoseTurn, c);
                    valid = game.IsValidMove(FromTo);
                    promotionChar = 'z';
                }
                else
                {
                    FromTo = new Move(From, To, game.WhoseTurn);
                    valid = game.IsValidMove(FromTo);
                    
                }
                if (valid)
                {
                    MoveType mtype;
                    mtype = game.ApplyMove(FromTo, true);
                    game.InsertMove(game.GetFen());
                    game.IsItDrawByTRR(game.GetFen());
                }

            }else if ((mycolour == "black" && game.WhoseTurn == Player.White) ||(mycolour == "white" && game.WhoseTurn == Player.Black))
            {
                //fi haga hna msh mazboota
                FromTo = new Move(From, To, game.WhoseTurn);
                valid = game.IsValidMove(FromTo);
                if (valid)
                {
                    MoveType mtype;
                    mtype = game.ApplyMove(FromTo, true);
                    game.InsertMove(game.GetFen());
                    game.IsItDrawByTRR(game.GetFen());
                }
                else
                {
                    opponent_played_invalid_move = true;
                }

            }


            string temp = game.GetFen();
            int space = temp.IndexOf(" ");
            if (space != -1)
                return temp.Substring(0,space);
            else
                return " "; //eh di ya georgee ???
        }
        
        public static string getFenToDraw(string fen)
        {

            int space = fen.IndexOf(" ");
            if (space != -1)
                return fen.Substring(0, space);
            else
                return " "; //eh di ya georgee ???
        }

        public static bool GameTerminated(ChessGame game)
        {
            return game.Terminated;
        }


        public static string End(ChessGame game)
        {
            if (game.DrawClaimed)
                return "DrawClaimed";
            else if (game.IsCheckmated(Player.Black))
                return "white wins";
            else if (game.IsCheckmated(Player.White))
                return "black wins";
            else
                return "";
        }
       
    }
}

using ChessDotNet; // the namespace of Chess.NET
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.ComponentModel;




namespace ClassLibrary
{
    public class Interface
    {
        public static bool opponent_played_invalid_move = false;
        static string mycolour;
        static char promotionChar;
        public static ChessGame Start(string fen, string mycolor)
        {
            //assuming white will always start
            ChessGame game = new ChessGame(fen);
            mycolour = mycolor;
            promotionChar = 'z'; //lesa magash promotion
            return game;
        }

        public static void setPromotionChar(char x)
        {
            promotionChar = x;
        }
        public static string CheckMove(string From, string To, ChessGame game)
        {
            char c = promotionChar;
            bool valid = false;
            Move FromTo = null;
            if (mycolour == "white" && game.WhoseTurn == Player.White)
            {
                if ((promotionChar != 'z') && (From == "a7" && To == "a8" && game.GetPieceAt(new Position("a7")).GetFenCharacter() == 'P')
                       || (From == "b7" && To == "b8" && game.GetPieceAt(new Position("b7")).GetFenCharacter() == 'P')
                       || (From == "c7" && To == "c8" && game.GetPieceAt(new Position("c7")).GetFenCharacter() == 'P')
                       || (From == "d7" && To == "d8" && game.GetPieceAt(new Position("d7")).GetFenCharacter() == 'P')
                       || (From == "e7" && To == "e8" && game.GetPieceAt(new Position("e7")).GetFenCharacter() == 'P')
                       || (From == "f7" && To == "f8" && game.GetPieceAt(new Position("f7")).GetFenCharacter() == 'P')
                       || (From == "g7" && To == "g8" && game.GetPieceAt(new Position("g7")).GetFenCharacter() == 'P')
                       || (From == "h7" && To == "h8" && game.GetPieceAt(new Position("h7")).GetFenCharacter() == 'P'))


                {
                    //c = (char)Console.Read();
                    FromTo = new Move(From, To, game.WhoseTurn, c);
                    valid = game.IsValidMove(FromTo);
                    promotionChar = 'z';
                }
                else
                {
                    FromTo = new Move(From, To, game.WhoseTurn);
                    valid = game.IsValidMove(FromTo);
                }
                if (valid)
                {
                    MoveType mtype;

                    mtype = game.ApplyMove(FromTo, true);
                    game.InsertMove(game.GetFen());
                    game.IsItDrawByTRR(game.GetFen());


                }

            }
            else if (mycolour == "black" && game.WhoseTurn == Player.Black)
            {
                if ((promotionChar != 'z') && (From == "a2" && To == "a1" && game.GetPieceAt(new Position("a2")).GetFenCharacter() == 'p')
                       || (From == "b2" && To == "b1" && game.GetPieceAt(new Position("b2")).GetFenCharacter() == 'p')
                       || (From == "c2" && To == "c1" && game.GetPieceAt(new Position("c2")).GetFenCharacter() == 'p')
                       || (From == "d2" && To == "d1" && game.GetPieceAt(new Position("d2")).GetFenCharacter() == 'p')
                       || (From == "e2" && To == "e1" && game.GetPieceAt(new Position("e2")).GetFenCharacter() == 'p')
                       || (From == "f2" && To == "f1" && game.GetPieceAt(new Position("f2")).GetFenCharacter() == 'p')
                       || (From == "g2" && To == "g1" && game.GetPieceAt(new Position("g2")).GetFenCharacter() == 'p')
                       || (From == "h2" && To == "h1" && game.GetPieceAt(new Position("h2")).GetFenCharacter() == 'p'))


                {
                    //c = (char)Console.Read(); //eh di ya broo ??
                    FromTo = new Move(From, To, game.WhoseTurn, c);
                    valid = game.IsValidMove(FromTo);
                    promotionChar = 'z';
                }
                else
                {
                    FromTo = new Move(From, To, game.WhoseTurn);
                    valid = game.IsValidMove(FromTo);

                }
                if (valid)
                {
                    MoveType mtype;
                    mtype = game.ApplyMove(FromTo, true);
                    game.InsertMove(game.GetFen());
                    game.IsItDrawByTRR(game.GetFen());
                }

            }
            else if ((mycolour == "white" && game.WhoseTurn == Player.Black))
            {
                if ((promotionChar != 'z') && (From == "a2" && To == "a1" && game.GetPieceAt(new Position("a2")).GetFenCharacter() == 'p')
                      || (From == "b2" && To == "b1" && game.GetPieceAt(new Position("b2")).GetFenCharacter() == 'p')
                      || (From == "c2" && To == "c1" && game.GetPieceAt(new Position("c2")).GetFenCharacter() == 'p')
                      || (From == "d2" && To == "d1" && game.GetPieceAt(new Position("d2")).GetFenCharacter() == 'p')
                      || (From == "e2" && To == "e1" && game.GetPieceAt(new Position("e2")).GetFenCharacter() == 'p')
                      || (From == "f2" && To == "f1" && game.GetPieceAt(new Position("f2")).GetFenCharacter() == 'p')
                      || (From == "g2" && To == "g1" && game.GetPieceAt(new Position("g2")).GetFenCharacter() == 'p')
                      || (From == "h2" && To == "h1" && game.GetPieceAt(new Position("h2")).GetFenCharacter() == 'p'))


                {
                    //c = (char)Console.Read(); //eh di ya broo ??
                    FromTo = new Move(From, To, game.WhoseTurn, c);
                    valid = game.IsValidMove(FromTo);
                    promotionChar = 'z';
                }
                else
                {
                    FromTo = new Move(From, To, game.WhoseTurn);
                    valid = game.IsValidMove(FromTo);

                }
                if (valid)
                {
                    MoveType mtype;
                    mtype = game.ApplyMove(FromTo, true);
                    game.InsertMove(game.GetFen());
                    game.IsItDrawByTRR(game.GetFen());
                }
                else
                {
                    opponent_played_invalid_move = true;
                }

            }
            else if ((mycolour == "white" && game.WhoseTurn == Player.Black))
            {
                if ((promotionChar != 'z') && (From == "a7" && To == "a8" && game.GetPieceAt(new Position("a7")).GetFenCharacter() == 'P')
                               || (From == "b7" && To == "b8" && game.GetPieceAt(new Position("b7")).GetFenCharacter() == 'P')
                               || (From == "c7" && To == "c8" && game.GetPieceAt(new Position("c7")).GetFenCharacter() == 'P')
                               || (From == "d7" && To == "d8" && game.GetPieceAt(new Position("d7")).GetFenCharacter() == 'P')
                               || (From == "e7" && To == "e8" && game.GetPieceAt(new Position("e7")).GetFenCharacter() == 'P')
                               || (From == "f7" && To == "f8" && game.GetPieceAt(new Position("f7")).GetFenCharacter() == 'P')
                               || (From == "g7" && To == "g8" && game.GetPieceAt(new Position("g7")).GetFenCharacter() == 'P')
                               || (From == "h7" && To == "h8" && game.GetPieceAt(new Position("h7")).GetFenCharacter() == 'P'))


                {
                    //c = (char)Console.Read();
                    FromTo = new Move(From, To, game.WhoseTurn, c);
                    valid = game.IsValidMove(FromTo);
                    promotionChar = 'z';
                }
                else
                {
                    FromTo = new Move(From, To, game.WhoseTurn);
                    valid = game.IsValidMove(FromTo);
                }
                if (valid)
                {
                    MoveType mtype;

                    mtype = game.ApplyMove(FromTo, true);
                    game.InsertMove(game.GetFen());
                    game.IsItDrawByTRR(game.GetFen());
                }
                else
                {
                    opponent_played_invalid_move = true;
                }

            }


            string temp = game.GetFen();
            int space = temp.IndexOf(" ");
            if (space != -1)
                return temp.Substring(0, space);
            else
                return " "; //eh di ya georgee ???
        }

        public static string getFenToDraw(string fen)
        {

            int space = fen.IndexOf(" ");
            if (space != -1)
                return fen.Substring(0, space);
            else
                return " "; //eh di ya georgee ???
        }

        public static bool GameTerminated(ChessGame game)
        {
            return game.Terminated;
        }


        public static string End(ChessGame game)
        {
            if (game.DrawClaimed)
                return "DrawClaimed";
            else if (game.IsCheckmated(Player.Black))
                return "white wins";
            else if (game.IsCheckmated(Player.White))
                return "black wins";
            else
                return "";
        }

    }
}
*/

using ChessDotNet; // the namespace of Chess.NET
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.ComponentModel;




namespace ClassLibrary
{
    public class Interface
    {
        public static bool opponent_played_invalid_move = false;
        static string mycolour;
        static char promotionChar;
        public static ChessGame Start(string fen, string mycolor)
        {
            //assuming white will always start
            ChessGame game = new ChessGame(fen);
            mycolour = mycolor;
            promotionChar = 'z'; //lesa magash promotion
            return game;
        }

        public static void setPromotionChar(char x)
        {
            promotionChar = x;
        }
        public static string CheckMove(string From, string To, ChessGame game)
        {
            char c = promotionChar;
            bool valid = false;
            Move FromTo = null;
            if (mycolour == "white" && game.WhoseTurn == Player.White)
            {
                if ((promotionChar != 'z') && (From == "a7" && (To == "a8" || To == "b8") && game.GetPieceAt(new Position("a7")).GetFenCharacter() == 'P')
                       || (From == "b7" && (To == "b8" || To == "a8" || To == "c8") && game.GetPieceAt(new Position("b7")).GetFenCharacter() == 'P')
                       || (From == "c7" && (To == "c8" || To == "b8" || To == "d8") && game.GetPieceAt(new Position("c7")).GetFenCharacter() == 'P')
                       || (From == "d7" && (To == "d8" || To == "c8" || To == "e8") && game.GetPieceAt(new Position("d7")).GetFenCharacter() == 'P')
                       || (From == "e7" && (To == "e8" || To == "d8" || To == "f8") && game.GetPieceAt(new Position("e7")).GetFenCharacter() == 'P')
                       || (From == "f7" && (To == "f8" || To == "e8" || To == "g8") && game.GetPieceAt(new Position("f7")).GetFenCharacter() == 'P')
                       || (From == "g7" && (To == "g8" || To == "f8" || To == "h8") && game.GetPieceAt(new Position("g7")).GetFenCharacter() == 'P')
                       || (From == "h7" && (To == "h8" || To == "g8") && game.GetPieceAt(new Position("h7")).GetFenCharacter() == 'P'))


                {
                    //c = (char)Console.Read();
                    FromTo = new Move(From, To, game.WhoseTurn, c);
                    valid = game.IsValidMove(FromTo);
                    promotionChar = 'z';
                }
                else
                {
                    FromTo = new Move(From, To, game.WhoseTurn);
                    valid = game.IsValidMove(FromTo);
                }
                if (valid)
                {
                    MoveType mtype;

                    mtype = game.ApplyMove(FromTo, true);
                    game.InsertMove(game.GetFen());
                    game.IsItDrawByTRR(game.GetFen());


                }

            }
            else if (mycolour == "black" && game.WhoseTurn == Player.Black)
            {
                if ((promotionChar != 'z') && (From == "a2" && (To == "a1" || To == "b1") && game.GetPieceAt(new Position("a2")).GetFenCharacter() == 'p')
                       || (From == "b2" && (To == "b1" || To == "a1" || To == "c1") && game.GetPieceAt(new Position("b2")).GetFenCharacter() == 'p')
                       || (From == "c2" && (To == "c1" || To == "b1" || To == "d1") && game.GetPieceAt(new Position("c2")).GetFenCharacter() == 'p')
                       || (From == "d2" && (To == "d1" || To == "c1" || To == "e1") && game.GetPieceAt(new Position("d2")).GetFenCharacter() == 'p')
                       || (From == "e2" && (To == "e1" || To == "d1" || To == "f1") && game.GetPieceAt(new Position("e2")).GetFenCharacter() == 'p')
                       || (From == "f2" && (To == "f1" || To == "e1" || To == "g1") && game.GetPieceAt(new Position("f2")).GetFenCharacter() == 'p')
                       || (From == "g2" && (To == "g1" || To == "f1" || To == "h1") && game.GetPieceAt(new Position("g2")).GetFenCharacter() == 'p')
                       || (From == "h2" && (To == "h1" || To == "g1") && game.GetPieceAt(new Position("h2")).GetFenCharacter() == 'p'))


                {
                    //c = (char)Console.Read(); //eh di ya broo ??
                    FromTo = new Move(From, To, game.WhoseTurn, c);
                    valid = game.IsValidMove(FromTo);
                    promotionChar = 'z';
                }
                else
                {
                    FromTo = new Move(From, To, game.WhoseTurn);
                    valid = game.IsValidMove(FromTo);

                }
                if (valid)
                {
                    MoveType mtype;
                    mtype = game.ApplyMove(FromTo, true);
                    game.InsertMove(game.GetFen());
                    game.IsItDrawByTRR(game.GetFen());
                }

            }
            else if ((mycolour == "white" && game.WhoseTurn == Player.Black))
            {
                if ((promotionChar != 'z') && (From == "a2" && (To == "a1" || To == "b1") && game.GetPieceAt(new Position("a2")).GetFenCharacter() == 'p')
                      || (From == "b2" && (To == "b1" || To == "a1" || To == "c1") && game.GetPieceAt(new Position("b2")).GetFenCharacter() == 'p')
                      || (From == "c2" && (To == "c1" || To == "b1" || To == "d1") && game.GetPieceAt(new Position("c2")).GetFenCharacter() == 'p')
                      || (From == "d2" && (To == "d1" || To == "c1" || To == "e1") && game.GetPieceAt(new Position("d2")).GetFenCharacter() == 'p')
                      || (From == "e2" && (To == "e1" || To == "d1" || To == "f1") && game.GetPieceAt(new Position("e2")).GetFenCharacter() == 'p')
                      || (From == "f2" && (To == "f1" || To == "e1" || To == "g1") && game.GetPieceAt(new Position("f2")).GetFenCharacter() == 'p')
                      || (From == "g2" && (To == "g1" || To == "f1" || To == "h1") && game.GetPieceAt(new Position("g2")).GetFenCharacter() == 'p')
                      || (From == "h2" && (To == "h1" || To == "g1") && game.GetPieceAt(new Position("h2")).GetFenCharacter() == 'p'))


                {
                    //c = (char)Console.Read(); //eh di ya broo ??
                    FromTo = new Move(From, To, game.WhoseTurn, c);
                    valid = game.IsValidMove(FromTo);
                    promotionChar = 'z';
                }
                else
                {
                    FromTo = new Move(From, To, game.WhoseTurn);
                    valid = game.IsValidMove(FromTo);

                }
                if (valid)
                {
                    MoveType mtype;
                    mtype = game.ApplyMove(FromTo, true);
                    
                    game.InsertMove(game.GetFen());
                    game.IsItDrawByTRR(game.GetFen());
                }
                else
                {
                    opponent_played_invalid_move = true;
                }

            }
            else if ((mycolour == "black" && game.WhoseTurn == Player.White))
            {
                if ((promotionChar != 'z') && (From == "a7" && (To == "a8" || To == "b8") && game.GetPieceAt(new Position("a7")).GetFenCharacter() == 'P')
                               || (From == "b7" && (To == "b8" || To == "a8" || To == "c8") && game.GetPieceAt(new Position("b7")).GetFenCharacter() == 'P')
                               || (From == "c7" && (To == "c8" || To == "b8" || To == "d8") && game.GetPieceAt(new Position("c7")).GetFenCharacter() == 'P')
                               || (From == "d7" && (To == "d8" || To == "c8" || To == "e8") && game.GetPieceAt(new Position("d7")).GetFenCharacter() == 'P')
                               || (From == "e7" && (To == "e8" || To == "d8" || To == "f8") && game.GetPieceAt(new Position("e7")).GetFenCharacter() == 'P')
                               || (From == "f7" && (To == "f8" || To == "e8" || To == "g8") && game.GetPieceAt(new Position("f7")).GetFenCharacter() == 'P')
                               || (From == "g7" && (To == "g8" || To == "f8" || To == "h8") && game.GetPieceAt(new Position("g7")).GetFenCharacter() == 'P')
                               || (From == "h7" && (To == "h8" || To == "g8") && game.GetPieceAt(new Position("h7")).GetFenCharacter() == 'P'))


                {
                    //c = (char)Console.Read();
                    FromTo = new Move(From, To, game.WhoseTurn, c);
                    valid = game.IsValidMove(FromTo);
                    promotionChar = 'z';
                }
                else
                {
                    FromTo = new Move(From, To, game.WhoseTurn);
                    valid = game.IsValidMove(FromTo);
                }
                if (valid)
                {
                    MoveType mtype;

                    mtype = game.ApplyMove(FromTo, true);
                    game.InsertMove(game.GetFen());
                    game.IsItDrawByTRR(game.GetFen());
                }
                else
                {
                    opponent_played_invalid_move = true;
                }

            }


            string temp = game.GetFen();
            int space = temp.IndexOf(" ");
            if (space != -1)
                return temp.Substring(0, space);
            else
                return " "; //eh di ya georgee ???
        }

        public static string getFenToDraw(string fen)
        {

            int space = fen.IndexOf(" ");
            if (space != -1)
                return fen.Substring(0, space);
            else
                return " "; //eh di ya georgee ???
        }

        public static bool GameTerminated(ChessGame game)
        {
            return game.Terminated;
        }


        public static string End(ChessGame game)
        {
            if (game.DrawClaimed)
                return "DrawClaimed";
            else if (game.IsCheckmated(Player.Black))
                return "white wins";
            else if (game.IsCheckmated(Player.White))
                return "black wins";
            else
                return "";
        }

    }
}
