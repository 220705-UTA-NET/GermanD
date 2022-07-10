using System;
// Let user choose between player 2 or computer player.
// Add win conditions
// Not able to overwrite previous moves.
// after every match ask player if they would like to continue if so run new instance. Otherwise end.


namespace Project0
{
    class TicTacToe
     {
        static void Main(string[] args)
        {
            // Declare gameBoard and print out to console.
            char[][] gameBoard = {{'.','.','.'},{'.','.','.'},{'.','.','.'}};
            for (int row = 0; row < 3; row++)
            {
                 for (int col = 0; col < 3 ; col++) 
                 {
                    Console.WriteLine(gameBoard[row][col] + " ");
                 }
            }
         

            //int[] gameBoard = new int[9];


            // For loop to run the length of the INT array gameBoard
          //  for (int i = 0; i < 9 ; i++)
           // {
          //      gameBoard[i] = 0;
           // }


            // Declare counter variable
           // int counter = 0;


         //   while (counter <= 8)
           // {

          //  }
            //for (int i = 0; i <= gameBoard.Length; i++);
        }

    }
}