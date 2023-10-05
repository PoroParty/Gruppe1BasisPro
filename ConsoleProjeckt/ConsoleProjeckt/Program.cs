using System;

namespace Jeopardy //JEPPE
{
    internal class Program
    {
        static int boardSize = 7;
        static char[,] visibleBoard = new char[boardSize, boardSize]; // synligt bræt for spilleren
        static char[,] hiddenBoard = new char[boardSize, boardSize]; // skjult bræt for spilleren
        static int numberOfShips = 4;
        static int shipsPlaced = 0;
        //static int[] intArray = new int[7] {12, 33, 532, 145, 1, 4565, 1};
        //static string[] nameArray = new string[5] {"a", "b", "c", "d", "e" };
        //static int inputIndex;

        static int[,] chessArray = new int[8, 8];
        static int[,] tempArraySargon = new int[8, 8];
        static int checkPiece;
        static int countMeDaddy;
        static int aSargon;
        static int bSargon;
        static int cTempSargon;
        static int dTempSargon;
        static bool legalMoveSargon = false;
        static int turnCounterSargon = 1;
        static string whitePlayerSargon = "white";
        static string blackPlayerSargon = "black";

        /*
        empty = 0

        WHITE ONLY:
        pawn = 1
        rook = 2
        knight = 3
        bishop = 4
        queen = 5
        king = 6

        BLACK ONLY:
        pawn = 7
        rook = 8
        knight = 9
        bishop = 10
        queen = 11
        king = 12
        */

        static void EnterLoopSargon()
        {
            for (int x = 0; x < chessArray.GetLength(0); x++)
            {
                Console.WriteLine();
                for (int y = 0; y < chessArray.GetLength(1); y++)
                {
                    Console.Write(chessArray[x, y].ToString("00 "));

                    if (x == 7 && y == 7)
                    {
                        ChangePositionSargon(x, y);
                    }
                    /*if (x == 4 && y == 0) {         //how to get position
                        Console.WriteLine("you have landed on " + chessArray[x, y]); // how to get value
                        //chessArray[x, y] = 1;
                    }*/
                }
            }
        }

        static int[,] ChangePositionSargon(int x, int y)
        {
            Console.WriteLine();
            if (turnCounterSargon == 1)
            {
                Console.WriteLine("White player's turn");
            }
            else
            {
                Console.WriteLine("Black player's turn");
            }



            if (turnCounterSargon == 1)
            {               //moving from
                Console.WriteLine("Choose a position to move from");
                x = Int32.Parse(Console.ReadLine());
                y = Int32.Parse(Console.ReadLine());
                cTempSargon = chessArray[x, y];

                if (cTempSargon <= 6 && cTempSargon != 0)
                {
                    //moving to
                    Console.WriteLine("Choose a position to move to?");
                    aSargon = Int32.Parse(Console.ReadLine());
                    bSargon = Int32.Parse(Console.ReadLine());
                    //moves your piece from current position to new position

                    IsLegalSargon(x, y, aSargon, bSargon); //ifstatement for this

                    if (chessArray[aSargon, bSargon] != 0)
                    {
                        if (chessArray[aSargon, bSargon] <= 6 && chessArray[aSargon, bSargon] != 0) //checks to see if youre about to move a piece onto another one of your own pieces.
                        {
                            Console.WriteLine("You cant place our own pieces on top of each other, try again");
                            ChangePositionSargon(x, y); //reruns the code if an invalid move is made, giving the player another attempt.
                        }
                        WhoMovedSargon(chessArray[aSargon, bSargon]); //checks where you're going to place your piece.
                        chessArray[x, y] = 0;
                        chessArray[aSargon, bSargon] = cTempSargon;
                        Console.WriteLine(" but now a...");
                        WhoMovedSargon(cTempSargon);
                    }
                    else
                    {
                        chessArray[x, y] = 0;
                        chessArray[aSargon, bSargon] = cTempSargon;
                        Console.WriteLine("you moved to a free spot");
                    }

                    turnCounterSargon = 2;

                }
                else
                {
                    Console.WriteLine("You cant move another player's pieces, try again");
                    ChangePositionSargon(x, y);
                }
            }

            else if (turnCounterSargon == 2)
            {
                Console.WriteLine("Choose a position to move from"); //input to declare your piece
                x = Int32.Parse(Console.ReadLine());
                y = Int32.Parse(Console.ReadLine());
                cTempSargon = chessArray[x, y];

                if (cTempSargon <= 12 && cTempSargon > 6) //move turn out to a higher level if priority and the turn counter change here.
                {
                    //moving to
                    Console.WriteLine("Choose a position to move to?");
                    aSargon = Int32.Parse(Console.ReadLine());
                    bSargon = Int32.Parse(Console.ReadLine());
                    //moves your piece from current position to new position
                    if (chessArray[aSargon, bSargon] != 0)
                    {
                        if (chessArray[aSargon, bSargon] <= 12 && chessArray[aSargon, bSargon] > 6)
                        {
                            Console.WriteLine("You cant place our own pieces on top of each other, try again");
                            ChangePositionSargon(x, y);
                        }
                        WhoMovedSargon(chessArray[aSargon, bSargon]); //checks where you're going to place your piece.
                        chessArray[x, y] = 0;
                        chessArray[aSargon, bSargon] = cTempSargon;
                        Console.WriteLine(" but now a...");
                        WhoMovedSargon(cTempSargon);
                        //EnterLoop();
                    }
                    else
                    {
                        chessArray[x, y] = 0;
                        chessArray[aSargon, bSargon] = cTempSargon;
                        Console.WriteLine("you moved to a free spot");
                        //EnterLoop();
                    }
                    turnCounterSargon = 1;

                }
                else
                {
                    Console.WriteLine("You cant move another player's pieces, try again");
                    ChangePositionSargon(x, y);
                }
            }
            //reruns the ChangePosition function to prevent empty fields being moved.


            TurnManagerSargon();
            Console.WriteLine("press ENTER to continue board update");
            Console.ReadLine();
            return tempArraySargon; //i think this is unnecessary and can be removed + change the function to int
        }

        static void TurnManagerSargon()
        {
            if (turnCounterSargon == 1)
            {
                Console.WriteLine();
                Console.WriteLine(whitePlayerSargon + "'s turn:");
                Console.WriteLine("Updating the board");
                //turnCounter = 2;
                EnterLoopSargon();
            }
            else if (turnCounterSargon == 2)
            {
                Console.WriteLine();
                Console.WriteLine(blackPlayerSargon + "'s turn:");
                Console.WriteLine("Updating the board");
                //turnCounter = 1;
                EnterLoopSargon();
            }

        }
        static void WhoMovedSargon(int checkPiece)
        {
            //checkPiece = chessArray[x, y];
            //Console.WriteLine(checkPiece + "you found meeeeeeeeee");
            //Console.WriteLine("a pawn occupies this space");
            switch (checkPiece)
            {
                case 1:
                    Console.Write("a white Pawn occupies this space");
                    break;
                case 2:
                    Console.Write("a white Rook occupies this space");
                    break;
                case 3:
                    Console.Write("a white Knight occupies this space");
                    break;
                case 4:
                    Console.Write("a white Bishop occupies this space");
                    break;
                case 5:
                    Console.Write("a white Queen occupies this space");
                    break;
                case 6:
                    Console.Write("a white King occupies this space");
                    break;

                case 7:
                    Console.Write("a black Pawn occupies this space");
                    break;
                case 8:
                    Console.Write("a black Rook occupies this space");
                    break;
                case 9:
                    Console.Write("a black Knight occupies this space");
                    break;
                case 10:
                    Console.Write("a black Bishop occupies this space");
                    break;
                case 11:
                    Console.Write("a black Queen occupies this space");
                    break;
                case 12:
                    Console.Write("a black King occupies this space");
                    break;
                default:
                    Console.Write("Looking forward to the Weekend.");
                    break;
            }
        }

        static bool IsLegalSargon(int x, int y, int a, int b)
        {
            cTempSargon = chessArray[x, y];
            dTempSargon = chessArray[a, b];

            //dTemp value - 6 to get same result

            switch (cTempSargon) //checks which piece is being selected
            {
                case 1: //&& chessArray[a, b] == chessArray[x, y + 1]
                    //b >= y + 2 ||
                    //(chessArray[x, y + 1] > 0)
                    if (b >= y + 2 || x == a && b == y + 1 && (chessArray[a, b] > 0))
                    {
                        Console.WriteLine(y + 1);
                        Console.WriteLine("movement blocked");
                        //Console.WriteLine(y);
                        // Console.WriteLine(b);
                        //Console.Write("You are moving too far, you cant do that // or youre being blocked by an enemy piece.");
                        legalMoveSargon = false;
                        return legalMoveSargon;
                    }
                    else if ((chessArray[x - 1, y + 1] >= 7) || (chessArray[x + 1, y + 1] >= 7)) //change 0 to 1
                    {
                        Console.WriteLine("ATAAAAAAAAAAAAAAACKKKKKKKKK");
                        legalMoveSargon = true;
                    }


                    break;
                case 2:

                    break;
                default:
                    Console.Write("YOU HAVE DEFAULTED THE LEGAL");
                    break;
            }


            //legalMove = false;
            return legalMoveSargon;
        }

        static void InitializeChess()
        {
            //chessArray[1, 2] = 7; // FAKE PIECE, remove it, ONLY FOR DEBUGGING
            //chessArray[2, 2] = 7; // FAKE PIECE, remove it, ONLY FOR DEBUGGING
            //Establishing positions:

            //White's board
            chessArray[0, 1] = 1; //pawn = 1
            chessArray[1, 1] = 1; //pawn = 1
            chessArray[2, 1] = 1; //pawn = 1
            chessArray[3, 1] = 1; //pawn = 1
            chessArray[4, 1] = 1; //pawn = 1
            chessArray[5, 1] = 1; //pawn = 1
            chessArray[6, 1] = 1; //pawn = 1
            chessArray[7, 1] = 1; //pawn = 1

            chessArray[0, 0] = 2; //rook = 2
            chessArray[1, 0] = 3; //knight = 3
            chessArray[2, 0] = 4; //bishop = 4
            chessArray[3, 0] = 5; //queen = 5
            chessArray[4, 0] = 6; //king = 6
            chessArray[5, 0] = 4; //bishop = 4
            chessArray[6, 0] = 3; //knight = 3
            chessArray[7, 0] = 2; //rook = 2

            //----------------------//

            //black's board//
            chessArray[0, 6] = 7; //pawn = 1
            chessArray[1, 6] = 7; //pawn = 1
            chessArray[2, 6] = 7; //pawn = 1
            chessArray[3, 6] = 7; //pawn = 1
            chessArray[4, 6] = 7; //pawn = 1
            chessArray[5, 6] = 7; //pawn = 1
            chessArray[6, 6] = 7; //pawn = 1
            chessArray[7, 6] = 7; //pawn = 1

            chessArray[0, 7] = 8; //rook = 8
            chessArray[1, 7] = 9; //knight = 9
            chessArray[2, 7] = 10;//bishop = 10
            chessArray[3, 7] = 11;//queen = 11
            chessArray[4, 7] = 12;//king = 12
            chessArray[5, 7] = 10;//bishop = 10
            chessArray[6, 7] = 9; //knight = 9
            chessArray[7, 7] = 8; //rook = 8

            EnterLoopSargon();

        }

        static void Skibe()
        {
            InitializeBoard();
            PlaceShips();
            while (true)
            {
                DisplayVisibleBoard();
                TakeShot();
            }
        }
        static void InitializeBoard()
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    visibleBoard[i, j] = '~';
                    hiddenBoard[i, j] = '~';
                }
            }
        }

        static void DisplayVisibleBoard()
        {
            Console.Clear();
            Console.WriteLine("Slagskibsspil");
            Console.WriteLine();
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    Console.Write(visibleBoard[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void PlaceShips()
        {
            Console.WriteLine("Placer dine skibe:");
            while (shipsPlaced < numberOfShips)
            {
                Console.Write("Indtast række for skib #" + (shipsPlaced + 1) + " (0-" + (boardSize - 1) + "): ");
                int row = int.Parse(Console.ReadLine());

                Console.Write("Indtast kolonne for skib #" + (shipsPlaced + 1) + " (0-" + (boardSize - 1) + "): ");
                int col = int.Parse(Console.ReadLine());

                if (row >= 0 && row < boardSize && col >= 0 && col < boardSize && hiddenBoard[row, col] == '~')
                {
                    hiddenBoard[row, col] = 'S';
                    shipsPlaced++;
                }
                else
                {
                    Console.WriteLine("Ugyldig placering. Prøv igen.");
                }
            }
        }

        static void TakeShot()
        {
            Console.Write("Indtast række for dit skud (0-" + (boardSize - 1) + "): ");
            int row = int.Parse(Console.ReadLine());

            Console.Write("Indtast kolonne for dit skud (0-" + (boardSize - 1) + "): ");
            int col = int.Parse(Console.ReadLine());

            if (row >= 0 && row < boardSize && col >= 0 && col < boardSize)
            {
                if (visibleBoard[row, col] == '~')
                {
                    if (hiddenBoard[row, col] == 'S')
                    {
                        Console.WriteLine("Du ramte et skib!");
                        visibleBoard[row, col] = 'X'; // Marker ramt skib
                    }
                    else
                    {
                        Console.WriteLine("Du missede.");
                        visibleBoard[row, col] = 'O'; // Marker vand
                    }
                }
                else
                {
                    Console.WriteLine("Du har allerede skudt der.");
                }
            }
            else
            {
                Console.WriteLine("Ugyldig skudplacering. Prøv igen.");
            }

            Console.WriteLine();
            Console.ReadKey();
        }
        static void Mastermind()
        {
            string[] masterCode = new string[] { "black", "blue", "yellow", "red" };
            string[] codeGuess = new string[] { "white", "white", "white", "white" };

            Console.WriteLine("Mastermind By Kasper ");

            Console.WriteLine("guess the first color");
            codeGuess[0] = Console.ReadLine();
            Console.WriteLine("guess the second color");
            codeGuess[1] = Console.ReadLine();
            Console.WriteLine("guess the third color");
            codeGuess[2] = Console.ReadLine();
            Console.WriteLine("guess the forth color");
            codeGuess[3] = Console.ReadLine();


            // kigger efter om arrays har de samme farver.  
            if (masterCode[0] == codeGuess[0])
            {
                Console.WriteLine("one right color in right place");
            }
            else
            {
                Console.WriteLine("one color is wrong");
            }
            if (masterCode[1] == codeGuess[1])
            {
                Console.WriteLine("one right color in right place");
            }
            else
            {
                Console.WriteLine("one color is wrong");
            }
            if (masterCode[2] == codeGuess[2])
            {
                Console.WriteLine("one right color in right place");
            }
            else
            {
                Console.WriteLine("one color is wrong");
            }
            if (masterCode[3] == codeGuess[3])
            {
                Console.WriteLine("one right color in right place");
            }
            else
            {
                Console.WriteLine("one color is wrong");
            }



        }
        static int JeopardyPoints()

        {
            Console.WriteLine("Welcome to Jeopardy! \n\nPlease enter the name of your team");
            // Variablerne for holdene bliver lavet
            string jeopardyTeam1 = Console.ReadLine();
            string jeopardyTeam2;
            string jeopardyTeam3;
            Console.WriteLine("\nWelcome " + jeopardyTeam1 + "\nYou can either play alone or add more teams, would you like to add more?");

            //Tilføj hold
            string addMoreTeams = Console.ReadLine();
            string addEvenMoreTeams;

            // Switch case til at tilføje hold
            switch (addMoreTeams)
            {
                case "Yes":
                    Console.WriteLine("Please enter the name of the team");
                    jeopardyTeam2 = Console.ReadLine();
                    Console.WriteLine(jeopardyTeam2 + " has been added to the game");
                    Console.WriteLine("Would you like to add more teams?");
                    addEvenMoreTeams = Console.ReadLine();
                    if (addEvenMoreTeams == "Yes")
                    {
                        Console.WriteLine("Please enter the name of the team");
                        jeopardyTeam3 = Console.ReadLine();
                        Console.WriteLine(jeopardyTeam3 + " has been added to the game. \nLet's begin the game");
                    }
                    else if (addEvenMoreTeams == "No")
                    {
                        Console.WriteLine("\nOkay, let's begin the game then\n");
                    }
                    break;
                case "No":
                    Console.WriteLine("\nOkay, let's begin the game then\n");
                    break;
            }



            // Point for hvert hold
            int jeopardyTeam1Points = 0;
            int jeopardyTeam2Points = 0;
            int jeopardyTeam3Points = 0;

            int teamTurn = 1;
            string incorrect = Console.ReadLine();

            if (true)
            {
                jeopardyTeam1 = incorrect;
                teamTurn = 2;
            }
            if (true)
            {
                jeopardyTeam2 = incorrect;
                teamTurn = 3;
            }
            if (true)
            {
                jeopardyTeam3 = incorrect;
                teamTurn = 1;
            }

            // Et multidimensionelt arrray i dimensionerne 6*6, som viser  kategorier og point
            while (true)
            {
                string[,] jeopardyPointsBoard = new string[6, 6]
                {
                {"Goats  ",   "  C# ",     "  Netlix ",    "   Internet ",      "   Memes   ",       "   Capitals" },
                {" 100  ", "  100  ", "   100   ", "    100    ", "    100    ", "   100  "},
                {" 200  ", "  200  ", "   200   ", "    200    ", "    200    ", "   200  "},
                {" 300  ", "  300  ", "   300   ", "    300    ", "    300    ", "   300  "},
                {" 400  ", "  400  ", "   400   ", "    400    ", "    400    ", "   400  "},
                {" 500  ", "  500  ", "   500   ", "    500    ", "    500    ", "   500  "}
                };


                {
                    // Et for loop til at vise boardet
                    for (int i = 0; i < jeopardyPointsBoard.GetLength(0); i++)
                    {
                        for (int j = 0; j < jeopardyPointsBoard.GetLength(1); j++)
                        {
                            Console.Write(jeopardyPointsBoard[i, j]);
                        }
                        Console.WriteLine();
                        Console.WriteLine();
                    }

                    Console.WriteLine("Please choose a category and the amount of points (fx C# 500)");
                    string playerChoice = Console.ReadLine();
                    string playerAnswer = "";

                    // Switch case der indeholder alle spørgsmål i quizzen
                    switch (playerChoice)
                    {
                        case "Goats 100":
                            Console.WriteLine("\nYou chose " + playerChoice);
                            Console.WriteLine("A horned animal, with a goatee and four legs");
                            playerAnswer = Console.ReadLine();

                            // Hvis korrekt så accepteres svaret og point bliver tilføjet
                            if (playerAnswer == "What is a goat?")
                            {
                                Console.WriteLine("\nCorrect, you get 100 points!");
                                jeopardyTeam1Points += 100;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                                jeopardyPointsBoard[1, 0] = "Chosen";
                            }
                            // Ellers hvis forkert så accepteres svaret ikke og point bliver trukket fra
                            else
                            {
                                Console.WriteLine("\nIncorrect, you get -100 points.");
                                jeopardyTeam1Points -= 100;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                                // Forsøg på at rette i array
                                jeopardyPointsBoard[1, 0] = "Chosen";
                            }
                            break;
                        case "Goats 200":
                            Console.WriteLine("\nYou chose " + playerChoice);
                            Console.WriteLine("The type of goats which live in the mountains");
                            playerAnswer = Console.ReadLine();
                            if (playerAnswer == "What is a mountain goat?")
                            {
                                Console.WriteLine("\nCorrect, you get 200 points!");
                                jeopardyTeam1Points += 200;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                                jeopardyPointsBoard[2, 0] = "";
                            }
                            else
                            {
                                Console.WriteLine("\nIncorrect, you get -200 points.");
                                jeopardyTeam1Points -= 200;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                                jeopardyPointsBoard[2, 0] = "";

                            }
                            break;
                        case "Goats 300":
                            Console.WriteLine("\nYou chose " + playerChoice);
                            Console.WriteLine("This Argentinian footballer with 5 BallonD'ors recently won the world cup");
                            playerAnswer = Console.ReadLine();
                            if (playerAnswer == "Who is Messi?")
                            {
                                Console.WriteLine("\nCorrect, you get 300 points!");
                                jeopardyTeam1Points += 300;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            else
                            {
                                Console.WriteLine("\nIncorrect, you get -300 points.");
                                jeopardyTeam1Points -= 300;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            break;
                        case "Goats 400":
                            Console.WriteLine("\nYou chose " + playerChoice);
                            Console.WriteLine("This rapper has made the song '99 Problems'");
                            playerAnswer = Console.ReadLine();
                            if (playerAnswer == "Who is Jay-Z?")
                            {
                                Console.WriteLine("\nCorrect, you get 400 points!");
                                jeopardyTeam1Points += 400;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            else
                            {
                                Console.WriteLine("\nIncorrect, you get -400 points.");
                                jeopardyTeam1Points -= 400;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                                jeopardyPointsBoard[2, 0] = "";
                            }
                            break;
                        case "Goats 500":
                            Console.WriteLine("\nYou chose " + playerChoice);
                            Console.WriteLine("This boxer has an iconic snapshot of him standing over his opponent lying down in the ring");
                            playerAnswer = Console.ReadLine();
                            if (playerAnswer == "Who is Muhammad Ali?")
                            {
                                Console.WriteLine("\nCorrect, you get 500 points!");
                                jeopardyTeam1Points += 500;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            else
                            {
                                Console.WriteLine("\nIncorrect, you get -500 points.");
                                jeopardyTeam1Points -= 500;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            break;
                        case "C# 100":
                            Console.WriteLine("\nYou chose " + playerChoice);
                            Console.WriteLine("'int' is short for this term");
                            playerAnswer = Console.ReadLine();
                            if (playerAnswer == "What is an integer?")
                            {
                                Console.WriteLine("\nCorrect, you get 100 points!");
                                jeopardyTeam1Points += 100;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            else
                            {
                                Console.WriteLine("\nIncorrect, you get -100 points.");
                                jeopardyTeam1Points -= 100;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            break;
                        case "C# 200":
                            Console.WriteLine("\nYou chose " + playerChoice);
                            Console.WriteLine("The application where this game takes place");
                            playerAnswer = Console.ReadLine();
                            if (playerAnswer == "What is console?")
                            {
                                Console.WriteLine("\nCorrect, you get 200 points!");
                                jeopardyTeam1Points += 200;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            else
                            {
                                Console.WriteLine("\nIncorrect, you get -200 points.");
                                jeopardyTeam1Points -= 200;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            break;
                        case "C# 300":
                            Console.WriteLine("\nYou chose " + playerChoice);
                            Console.WriteLine("The thing that makes you go round and round");
                            playerAnswer = Console.ReadLine();
                            if (playerAnswer == "What is a loop?")
                            {
                                Console.WriteLine("\nCorrect, you get 300 points!");
                                jeopardyTeam1Points += 300;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            else
                            {
                                Console.WriteLine("\nIncorrect, you get -300 points.");
                                jeopardyTeam1Points -= 300;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            break;
                        case "C# 400":
                            Console.WriteLine("\nYou chose " + playerChoice);
                            Console.WriteLine("When you want to switch it up");
                            playerAnswer = Console.ReadLine();
                            if (playerAnswer == "What is a switch case?")
                            {
                                Console.WriteLine("\nCorrect, you get 400 points!");
                                jeopardyTeam1Points += 400;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            else
                            {
                                Console.WriteLine("\nIncorrect, you get -400 points.");
                                jeopardyTeam1Points -= 400;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            break;
                        case "C# 500":
                            Console.WriteLine("\nYou chose " + playerChoice);
                            Console.WriteLine("A list of numbers or words, which can also be 2D");
                            playerAnswer = Console.ReadLine();
                            if (playerAnswer == "What is an array?")
                            {
                                Console.WriteLine("\nCorrect, you get 500 points!");
                                jeopardyTeam1Points += 500;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            else
                            {
                                Console.WriteLine("\nIncorrect, you get -500 points.");
                                jeopardyTeam1Points -= 500;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            break;
                        case "Netflix 100":
                            Console.WriteLine("\nYou chose " + playerChoice);
                            Console.WriteLine("The iconic phrase which starts with 'Netflix and'");
                            playerAnswer = Console.ReadLine();
                            if (playerAnswer == "What is chill?")
                            {
                                Console.WriteLine("\nCorrect, you get 100 points!");
                                jeopardyTeam1Points += 100;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            else
                            {
                                Console.WriteLine("\nIncorrect, you get -100 points.");
                                jeopardyTeam1Points -= 100;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            break;
                        case "Netflix 200":
                            Console.WriteLine("\nYou chose " + playerChoice);
                            Console.WriteLine("A show about a group of children vs. the evil in the upside down");
                            playerAnswer = Console.ReadLine();
                            if (playerAnswer == "What is Stranger Things?")
                            {
                                Console.WriteLine("\nCorrect, you get 200 points!");
                                jeopardyTeam1Points += 200;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            else
                            {
                                Console.WriteLine("\nIncorrect, you get -200 points.");
                                jeopardyTeam1Points -= 200;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            break;
                        case "Netflix 300":
                            Console.WriteLine("\nYou chose " + playerChoice);
                            Console.WriteLine("The show which this quote originates from: 'Jesse we have to cook'");
                            playerAnswer = Console.ReadLine();
                            if (playerAnswer == "What is Breaking Bad?")
                            {
                                Console.WriteLine("\nCorrect, you get 300 points!");
                                jeopardyTeam1Points += 300;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            else
                            {
                                Console.WriteLine("\nIncorrect, you get -300 points.");
                                jeopardyTeam1Points -= 300;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            break;
                        case "Netflix 400":
                            Console.WriteLine("\nYou chose " + playerChoice);
                            Console.WriteLine("A reality dating show, where the contestant have to propose to their partners before seeing them for the first time");
                            playerAnswer = Console.ReadLine();
                            if (playerAnswer == "What is Love is Blind?")
                            {
                                Console.WriteLine("\nCorrect, you get 400 points!");
                                jeopardyTeam1Points += 400;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            else
                            {
                                Console.WriteLine("\nIncorrect, you get -400 points.");
                                jeopardyTeam1Points -= 400;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            break;
                        case "Netflix 500":
                            Console.WriteLine("\nYou chose " + playerChoice);
                            Console.WriteLine("Better Call");
                            playerAnswer = Console.ReadLine();
                            if (playerAnswer == "Who is Saul?")
                            {
                                Console.WriteLine("\nCorrect, you get 500 points!");
                                jeopardyTeam1Points += 500;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            else
                            {
                                Console.WriteLine("\nIncorrect, you get -500 points.");
                                jeopardyTeam1Points -= 500;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            break;
                        case "Internet 100":
                            Console.WriteLine("\nYou chose " + playerChoice);
                            Console.WriteLine("The new name of Twitter");
                            playerAnswer = Console.ReadLine();
                            if (playerAnswer == "What is X?")
                            {
                                Console.WriteLine("\nCorrect, you get 100 points!");
                                jeopardyTeam1Points += 100;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            else
                            {
                                Console.WriteLine("\nIncorrect, you get -100 points.");
                                jeopardyTeam1Points -= 100;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            break;
                        case "Internet 200":
                            Console.WriteLine("\nYou chose " + playerChoice);
                            Console.WriteLine("The most common search engine");
                            playerAnswer = Console.ReadLine();
                            if (playerAnswer == "What is Google?")
                            {
                                Console.WriteLine("\nCorrect, you get 200 points!");
                                jeopardyTeam1Points += 200;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            else
                            {
                                Console.WriteLine("\nIncorrect, you get -200 points.");
                                jeopardyTeam1Points -= 200;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            break;
                        case "Internet 300":
                            Console.WriteLine("\nYou chose " + playerChoice);
                            Console.WriteLine("Broadcast Yourself");
                            playerAnswer = Console.ReadLine();
                            if (playerAnswer == "What is YouTube?")
                            {
                                Console.WriteLine("\nCorrect, you get 300 points!");
                                jeopardyTeam1Points += 300;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            else
                            {
                                Console.WriteLine("\nIncorrect, you get -300 points.");
                                jeopardyTeam1Points -= 300;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            break;
                        case "Internet 400":
                            Console.WriteLine("\nYou chose " + playerChoice);
                            Console.WriteLine("A huge community forum made up of smaller sub-communities");
                            playerAnswer = Console.ReadLine();
                            if (playerAnswer == "What is Reddit?")
                            {
                                Console.WriteLine("\nCorrect, you get 400 points!");
                                jeopardyTeam1Points += 400;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            else
                            {
                                Console.WriteLine("\nIncorrect, you get -400 points.");
                                jeopardyTeam1Points -= 400;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            break;
                        case "Internet 500":
                            Console.WriteLine("\nYou chose " + playerChoice);
                            Console.WriteLine("Short for 'Uniform Resource Locater");
                            playerAnswer = Console.ReadLine();
                            if (playerAnswer == "What is a URL?")
                            {
                                Console.WriteLine("\nCorrect, you get 500 points!");
                                jeopardyTeam1Points += 500;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            else
                            {
                                Console.WriteLine("\nIncorrect, you get -500 points.");
                                jeopardyTeam1Points -= 500;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            break;
                        case "Memes 100":
                            Console.WriteLine("\nYou chose " + playerChoice);
                            Console.WriteLine("The man who claims he is 'Never gonna give you up'");
                            playerAnswer = Console.ReadLine();
                            if (playerAnswer == "Who is Rick Astley?")
                            {
                                Console.WriteLine("\nCorrect, you get 100 points!");
                                jeopardyTeam1Points += 100;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            else
                            {
                                Console.WriteLine("\nIncorrect, you get -100 points.");
                                jeopardyTeam1Points -= 100;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            break;
                        case "Memes 200":
                            Console.WriteLine("\nYou chose " + playerChoice);
                            Console.WriteLine("This redheaded student became famous for his school picture, with many putting him in the context of being unlucky");
                            playerAnswer = Console.ReadLine();
                            if (playerAnswer == "Who is Bad Luck Brian?")
                            {
                                Console.WriteLine("\nCorrect, you get 200 points!");
                                jeopardyTeam1Points += 200;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            else
                            {
                                Console.WriteLine("\nIncorrect, you get -200 points.");
                                jeopardyTeam1Points -= 200;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            break;
                        case "Memes 300":
                            Console.WriteLine("\nYou chose " + playerChoice);
                            Console.WriteLine("This cartoon character is the face of the memes 'Not sure if' and 'Shut up and take my money!'");
                            playerAnswer = Console.ReadLine();
                            if (playerAnswer == "Who is Fry?")
                            {
                                Console.WriteLine("\nCorrect, you get 300 points!");
                                jeopardyTeam1Points += 300;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            else
                            {
                                Console.WriteLine("\nIncorrect, you get -300 points.");
                                jeopardyTeam1Points -= 300;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            break;
                        case "Memes 400":
                            Console.WriteLine("\nYou chose " + playerChoice);
                            Console.WriteLine("This feline has found internet fame through it's notorious face");
                            playerAnswer = Console.ReadLine();
                            if (playerAnswer == "Who is Grumpy Cat?")
                            {
                                Console.WriteLine("\nCorrect, you get 400 points!");
                                jeopardyTeam1Points += 400;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            else
                            {
                                Console.WriteLine("\nIncorrect, you get -400 points.");
                                jeopardyTeam1Points -= 400;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            break;
                        case "Memes 500":
                            Console.WriteLine("\nYou chose " + playerChoice);
                            Console.WriteLine("Considerered by many to be the original meme, \nThese cartoon strips contained phrases like 'FUUUUUUU' and 'Forever alone'");
                            playerAnswer = Console.ReadLine();
                            if (playerAnswer == "What is a rage comic?")
                            {
                                Console.WriteLine("\nCorrect, you get 500 points!");
                                jeopardyTeam1Points += 500;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            else
                            {
                                Console.WriteLine("\nIncorrect, you get -500 points.");
                                jeopardyTeam1Points -= 500;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            break;
                        case "Capitals 100":
                            Console.WriteLine("\nYou chose " + playerChoice);
                            Console.WriteLine("The capital of Denmark");
                            playerAnswer = Console.ReadLine();
                            if (playerAnswer == "What is Copenhagen?")
                            {
                                Console.WriteLine("\nCorrect, you get 100 points!");
                                jeopardyTeam1Points += 100;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            else
                            {
                                Console.WriteLine("\nIncorrect, you get -100 points.");
                                jeopardyTeam1Points -= 100;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            break;
                        case "Capitals 200":
                            Console.WriteLine("\nYou chose " + playerChoice);
                            Console.WriteLine("The capital of Germany");
                            playerAnswer = Console.ReadLine();
                            if (playerAnswer == "What is Berlin?")
                            {
                                Console.WriteLine("\nCorrect, you get 200 points!");
                                jeopardyTeam1Points += 200;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            else
                            {
                                Console.WriteLine("\nIncorrect, you get -200 points.");
                                jeopardyTeam1Points -= 200;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            break;
                        case "Capitals 300":
                            Console.WriteLine("\nYou chose " + playerChoice);
                            Console.WriteLine("The capital of Brazil");
                            playerAnswer = Console.ReadLine();
                            if (playerAnswer == "What is Brasilia?")
                            {
                                Console.WriteLine("\nCorrect, you get 300 points!");
                                jeopardyTeam1Points += 300;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            else
                            {
                                Console.WriteLine("\nIncorrect, you get -300 points.");
                                jeopardyTeam1Points -= 300;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            break;
                        case "Capitals 400":
                            Console.WriteLine("\nYou chose " + playerChoice);
                            Console.WriteLine("The capital of Canada");
                            playerAnswer = Console.ReadLine();
                            if (playerAnswer == "What is Ottawa?")
                            {
                                Console.WriteLine("\nCorrect, you get 400 points!");
                                jeopardyTeam1Points += 400;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            else
                            {
                                Console.WriteLine("\nIncorrect, you get -400 points.");
                                jeopardyTeam1Points -= 400;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            break;
                        case "Capitals 500":
                            Console.WriteLine("\nYou chose " + playerChoice);
                            Console.WriteLine("The capital of Australia");
                            playerAnswer = Console.ReadLine();
                            if (playerAnswer == "What is Canberra?")
                            {
                                Console.WriteLine("\nCorrect, you get 500 points!");
                                jeopardyTeam1Points += 500;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            else
                            {
                                Console.WriteLine("\nIncorrect, you get -500 points.");
                                jeopardyTeam1Points -= 500;
                                Console.WriteLine(jeopardyTeam1 + " now has " + jeopardyTeam1Points + " points");
                            }
                            break;
                        // default hvis spilleren ikke vælger en kategori
                        default:
                            Console.WriteLine(playerChoice + " is not a valid category\n");
                            break;
                    }
                    // Reset gives til loopet, så den ikke fortsætter med at skrive i uendelighed, men kræver i stedet input fra spilleren
                    Console.WriteLine("\nPlease choose another question or type 'quit' to exit to the main menu\n");


                    string quit = Console.ReadLine();
                    switch (quit)
                    {
                        case "quit":
                            Environment.Exit(0);
                            break;
                    }

                }


            }
        }

        static void Main(string[] args)
        {
            //JeopardyPoints();
            //Mastermind();
            //Skibe();
            //InitializeChess();
        }

    }
}
