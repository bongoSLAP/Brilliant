using System.Text.RegularExpressions;
using Brilliant.Models;

namespace Brilliant.Parsers;

public class PgnParser
{
    public List<Tuple<ChessMove, ChessMove>> GetMoves(string pgnText)
    {
        List<Tuple<ChessMove, ChessMove>> moves = new();
        int firstMoveIndex = pgnText.IndexOf("1.", StringComparison.Ordinal);
        string matchedPgn = pgnText.Substring(firstMoveIndex, pgnText.Length - firstMoveIndex);
        
        var movesRegex = new Regex(@"\d+\.\s*(\S+)[\s|\n](\S+)");
        var matches = movesRegex.Matches(matchedPgn);

        foreach (Match match in matches)
        {
            if (match.Groups.Count < 3)
                continue;

            ChessMove whiteMove = new (match.Groups[1].Value, "white");
            Tuple<ChessMove, ChessMove> movePair = new(whiteMove, null);            
            
            var blackMoveText = match.Groups[2].Value;

            if (!string.IsNullOrEmpty(blackMoveText) && blackMoveText != "0-1" && blackMoveText != "1-0" && blackMoveText != "1/2-1/2")
            {
                ChessMove blackMove = new(blackMoveText, "black");
                movePair = new(whiteMove, blackMove);            
            }
            
            moves.Add(movePair);
        }

        return moves;
    }
/*
    private Tuple<int, int> SetMoves(string moveText)
    {

        if (moveText == "O-O")
        {
            Type = MoveType.KingsideCastle;
            MoveTo = Tuple.Create(6, (moveText[0] == 'O' ? 0 : 7));
            return;
        }

        if (MoveText == "O-O-O")
        {
            Type = MoveType.QueensideCastle;
            MoveTo = Tuple.Create(2, (moveText[0] == 'O' ? 0 : 7));
            return;
        }
        
        
        
    }*/
}
