using System;
using System.Collections.Generic;
using Brilliant.Models;
using Brilliant.Pieces;

namespace Brilliant.Interfaces;

public interface IBoardService
{
    List<ChessPiece> FindPieces(Type pieceType, string color);
    bool IsOccupied(int x, int y);
    bool IsEnemyPiece(int x, int y, string color);
    bool IsMoveWithinBounds(int x, int y);
    ChessPiece GetPieceAt(int x, int y);
}