using System;
using System.Text;


namespace Project0;

public class Board
{
    //public events are like a delegate, but they can be used to call methods in other classes
    public event Action? OnGameOver; // event for when the game is over
    public event Action<Roles>? OnVictory; // event for when a player wins
    public event Action? OnTie; // event for when the game is a tie
    public event Action? OnMove; // event for when a move is made
    public event Action? OnSuccessfulMove; // event for when a move is made successfully
    public event Action? OnUnsuccessfulMove; // event for when a move is made unsuccessfully

    private readonly Roles[][] _board; // 2D array
    private int _turn; // keeps track of whose turn it is

    public Board() 
    {
        _board = new Roles[3][];
        _board[0] = new Roles[3];
        _board[1] = new Roles[3];
        _board[2] = new Roles[3];

        _turn = 0;
    }

    public void MakeMove(int x, int y) 
    {
        if (!CanPlayerMakeAMove(x, y))
        {
            OnUnsuccessfulMove?.Invoke(); // if the move is invalid, call the event
            return;
        }

        var role = GetCurrentExpectedRole(); // get the role that the player is expected to play
        
        _board[y][x] = role; // set the board at the given position to the expected role
        
        HandlePostMove(role); // handle the post move logic
    }

    // returns the current expected role
    public Roles GetCurrentExpectedRole() => _turn % 2 == 0 ? Roles.O : Roles.X; // if the turn is even, O is expected, if odd, X is expected

    private void HandlePostMove(Roles role)
    {
        _turn++;
        OnMove?.Invoke(); 
        
        if (!IsGameOver())
        {
            OnSuccessfulMove?.Invoke(); 
            return;
        }

        OnGameOver?.Invoke();

        if (IsTie())
            OnTie?.Invoke(); // if the game is over and it's a tie, call the event
        else
            OnVictory?.Invoke(role); // if the game is over and it's a victory, call the event
    }

    private bool CanPlayerMakeAMove(int x, int y) => !IsPointOccupied(x, y) && !IsGameOver(); // checks if the point is occupied and if the game is over

    private bool IsPointOccupied(int x, int y) => _board[y][x] != Roles.None; // checks if the point is occupied

    private bool IsGameOver() => IsTie() || IsVictory(); // checks if the game is over

    private bool IsTie() => _turn > 8 && !IsVictory(); // checks if the game is a tie

    private bool IsVictory() => IsHorizontalVictory() || IsVerticalVictory() || IsDiagonalVictory(); // checks if the game is a victory

    private bool IsHorizontalVictory() 
    {
        foreach (var row in _board) 
        {
            if (row.Any(x => x == Roles.None)) // if any of the elements in the row is not occupied, the row is not a victory
                continue;

            var currentRole = row[0]; // get the role of the first element in the row
            if (row.All(x => x == currentRole)) // if all of the elements in the row are the same, the row is a victory
                return true;
        }

        return false;
    }

    private bool IsVerticalVictory()
    {
        for (var i = 0; i < _board.Length; i++) 
        {
            var targetRole = _board[0][i]; // get the role of the first element in the column
            var wasValid = true;

            for (var j = 0; j < _board[i].Length; j++)
            {
                var col = _board[j][i];//

                if (col == Roles.None || col != targetRole)
                {
                    wasValid = false;
                    break;
                }
            }

            if (wasValid)
                return true;
        }

        return false;
    }

    private bool IsDiagonalVictory() =>
         (
            (_board[0][0] == _board[1][1] && _board[1][1] == _board[2][2]) ||
            (_board[0][2] == _board[1][1] && _board[1][1] == _board[2][0])
        );

    public override string ToString()
    {
        var builder = new StringBuilder();

        foreach (var row in _board)
        {
            foreach (var role in row)
            {
                switch (role)
                {
                    case Roles.None:
                        builder.Append('.');
                        break;
                    case Roles.X:
                        builder.Append('X');
                        break;
                    case Roles.O:
                        builder.Append('O');
                        break;
                    default:
                        throw new InvalidOperationException("Unknown Role");
                }

                builder.Append(' ');
            }

            builder.Append('\n');
        }

        return builder.ToString();
    }
}