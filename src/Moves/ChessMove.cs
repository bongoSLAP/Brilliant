using System;
using System.Text.RegularExpressions;
using Brilliant.Enum;

namespace Brilliant.Moves;

public class ChessMove
{
    private string MoveText { get; set; }
    public Tuple<int, int> MoveTo { get; private set; }
    public Tuple<int, int> MoveFrom { get; private set; }
    public MoveType Type { get; private set; }
    
    public ChessMove(string moveText)
    {
        MoveText = moveText;
        GetCoordinates();
    }

    private void GetCoordinates()
    {
        if (MoveText == "O-O")
        {
            Type = MoveType.KingsideCastle;
            MoveTo = Tuple.Create(6, (MoveText[0] == 'O' ? 0 : 7));
            return;
        }

        if (MoveText == "O-O-O")
        {
            Type = MoveType.QueensideCastle;
            MoveTo = Tuple.Create(2, (MoveText[0] == 'O' ? 0 : 7));
            return;
        }
        
        string cleanMove = Regex.Replace(MoveText, "[+#=]", "");

        if (cleanMove.Contains('x'))
        {
            Type = MoveType.Capture;
            cleanMove = cleanMove.Split('x')[1];
        }
        else
        {
            Type = MoveType.Normal;
        }

        if (cleanMove.Length > 2)
        {
            cleanMove = cleanMove[1..];
        }

        int xIndex = -1;
        int yIndex = -1;
        
        if (cleanMove.Length == 2)
        {
            xIndex = 0;
            yIndex = 1;
        }
        
        if (cleanMove.Length > 2)
        {
            xIndex = 1;
            yIndex = 2;
        }
        
        int x = cleanMove[xIndex] - 'a'; 
        int y = cleanMove[yIndex] - '1';
        MoveTo = Tuple.Create(x, y);
    }
}
