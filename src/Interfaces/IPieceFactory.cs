using Brilliant.Pieces;

namespace Brilliant.Interfaces;

public interface IPieceFactory
{
    ChessPiece CreateChessPiece(string identifier, string color, int x, int y);
}
