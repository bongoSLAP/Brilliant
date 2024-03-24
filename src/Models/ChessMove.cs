using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Brilliant.Enums;

namespace Brilliant.Models;

public class ChessMove
{
    public string MoveText { get; set; }
    public string CleanText { get; set; }
    public Tuple<int, int> MoveTo { get; set; }
    public Tuple<int, int> MoveFrom { get;  set; }
    public MoveType Type { get; private set; }
    public string Color { get; set; }
    
    public ChessMove(string moveText, string color)
    {
        MoveText = moveText;
        Color = color;
        CleanText = CleanMove(moveText);
        SetMoveType();

        if (!MoveText.Contains("O-O"))
            SetMoveTo();
    }

    public void SetMoveTo(Tuple<int, int> coordinates)
    {
        MoveTo = coordinates;
    }
    
    public void SetMoveFrom(Tuple<int, int> coordinates)
    {
        MoveFrom = coordinates;
    }
    
    public Tuple<int, int> ConvertToCoordinates(string algebraicNotation)
    {
        Dictionary<char, int> fileToColumnIndex = new Dictionary<char, int>
        {
            {'a', 0}, {'b', 1}, {'c', 2}, {'d', 3},
            {'e', 4}, {'f', 5}, {'g', 6}, {'h', 7}
        };
    
        char fileLetter = algebraicNotation[^2];
        char rankDigit = algebraicNotation[^1];
    
        int columnIndex = fileToColumnIndex[fileLetter];
        int rowIndex = 8 - (rankDigit - '0');

        return Tuple.Create(columnIndex, rowIndex);
    }

    private void SetMoveType()
    {
        Type = MoveText.Contains('x') ? MoveType.Capture : MoveType.Normal;
    }

    private void SetMoveTo()
    {
        MoveTo = ConvertToCoordinates(CleanText);
    }

    private string CleanMove(string moveText)
    {
        return Regex.Replace(moveText, "[+#=x]", "");
    }
}
