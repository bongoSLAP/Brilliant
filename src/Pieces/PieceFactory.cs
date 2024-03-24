using System;
using Brilliant.Interfaces;

namespace Brilliant.Pieces;

public class PieceFactory : IPieceFactory
{
    public ChessPiece CreateChessPiece(string identifier, string color, int x, int y)
    {
        switch (identifier)
        {
            case "rook": return new Rook(color, x, y);
            case "knight": return new Knight(color, x, y);
            case "bishop": return new Bishop(color, x, y);
            case "queen": return new Queen(color, x, y);
            case "king": return new King(color, x, y);
            case "pawn": return new Pawn(color, x, y);
            default: throw new ArgumentException("Unknown piece type", nameof(identifier));
        }
    }
}
