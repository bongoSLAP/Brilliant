using System;
using System.Collections.Generic;
using Brilliant.Interfaces;
using Microsoft.Maui.Controls;

namespace Brilliant.Pieces;

public class Pawn : ChessPiece
{
    public Pawn(string color, int x, int y) : base(color, x, y)
    {
        var image = new Image
        {
            Source = $"{color}_pawn.png",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            WidthRequest = 80,
            HeightRequest = 80
        };

        Image = image;
    }
    
    public override List<Tuple<int, int>> GetPossibleMoves(IBoardService boardService)
    {
        var possibleMoves = new List<Tuple<int, int>>();
    
        int direction = Color == "white" ? -1 : 1;
        int startRow = Color == "white" ? 6 : 1;
    
        if (boardService.IsMoveWithinBounds(X, Y + direction) && !boardService.IsOccupied(X, Y + direction))
            possibleMoves.Add(Tuple.Create(X, Y + direction));
    
        if (Y == startRow && !boardService.IsOccupied(X, Y + direction) && !boardService.IsOccupied(X, Y + 2 * direction) && boardService.IsMoveWithinBounds(X, Y + 2 * direction))
            possibleMoves.Add(Tuple.Create(X, Y + 2 * direction));
    
        int[] captureOffsets = {-1, 1};
        foreach (var offset in captureOffsets)
        {
            if (boardService.IsMoveWithinBounds(X + offset, Y + direction) && boardService.IsEnemyPiece(X + offset, Y + direction, Color))
                possibleMoves.Add(Tuple.Create(X + offset, Y + direction));
        }
    
        return possibleMoves;
    }
}
