using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Brilliant.Moves;

namespace Brilliant.Parsers;

public class PgnParser
{
    public List<ChessMove> ParseMoves(string pgnText)
    {
        List<ChessMove> moves = new List<ChessMove>();
        int firstMoveIndex = pgnText.IndexOf("1.", StringComparison.Ordinal);
        string matchedPgn = pgnText.Substring(firstMoveIndex, pgnText.Length - firstMoveIndex);
        
        var movesRegex = new Regex(@"\d+\.\s*(\S+)[\s|\n](\S+)");
        var matches = movesRegex.Matches(matchedPgn);

        foreach (Match match in matches)
        {
            if (match.Groups.Count < 3)
                continue;

            var whiteMoveText = match.Groups[1].Value;
            var blackMoveText = match.Groups[2].Value;

            moves.Add(new ChessMove(whiteMoveText));
            if (!string.IsNullOrEmpty(blackMoveText) && blackMoveText != "0-1" && blackMoveText != "1-0" && blackMoveText != "1/2-1/2")
            {
                moves.Add(new ChessMove(blackMoveText));
            }
        }

        return moves;
    }
}
