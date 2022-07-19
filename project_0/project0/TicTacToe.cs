using System;
namespace Project0;

public class TicTacToe
{
    private readonly Board _board;
    private int _turn;

    public TicTacToe()
    {
        _board = new Board();

        _board.OnMove += () => Console.WriteLine(_board);

        _board.OnSuccessfulMove += AskForAndMakeMove;

        _board.OnUnsuccessfulMove += () =>
        {
            Console.WriteLine("Invalid move, please try again");
            AskForAndMakeMove();
        };

        _board.OnGameOver += () => Console.WriteLine("Game is over!");
        _board.OnTie += () => Console.WriteLine("Game is a tie!");
        _board.OnVictory += role => Console.WriteLine($"{role} wins!");
    }

    public void StartGame() => AskForAndMakeMove();

    private void AskForAndMakeMove()
    {
        var (xPos, yPos) = AskForMove();
        _board.MakeMove(xPos, yPos);
    }

    private (int, int) AskForMove()
    {
        for(var i = 0;; i++)
            try
            {
                Console.WriteLine("Enter Column then Row to select your move.");
                var input = Console.ReadLine().Split(" ");
                var x = int.Parse(input[0]);
                var y = int.Parse(input[1]);
                return (x, y);
            }

            
            catch (Exception)
            {
                if(i > 3)
                    Console.WriteLine("Why must you make me suffer?");
                else
                    Console.WriteLine("Nope");
            }
    }
}

/*     private (int, int) AskForMove()
    {
        for (var i = 0; ; i++)
        {




            //try
            //{

            //   Console.WriteLine("Enter Column then Row to select your move.");
            //   var input = Console.ReadLine().Split(" ");
            //   var x = int.Parse(input[0]);
            //   var y = int.Parse(input[1]);
            var x = -1;
            var y = -1;
            int c = 0;
            do
            {

                Console.WriteLine("Enter Column then Row to select your move.");
                var input = Console.ReadLine().Split(" ");
                if (!int.TryParse(input[0], out int temp) || !int.TryParse(input[0], out int temp1))
                {
                    c++;
                    continue;
                }
                x = int.Parse(input[0]);
                y = int.Parse(input[1]);
                if (c > 3)
                    Console.WriteLine("Why must you make me suffer?");
                else
                    Console.WriteLine("Nope");
                c++;
            } while ((x < 0 || x > 2) || (y < 0 || y > 2));
            //do { }
            //while ((x < 0 || x > 2) && ((y < 0) || (y > 2))

            return (x, y);
        }
    } */