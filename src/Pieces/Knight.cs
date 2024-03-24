using System;
using System.Collections.Generic;
using Brilliant.Interfaces;
using Microsoft.Maui.Controls;

namespace Brilliant.Pieces;

public class Knight : ChessPiece
{
    public Knight(string color, int x, int y) : base(color, x, y)
    {
        var image = new Image
        {
            Source = $"{color}_knight.png",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            WidthRequest = 80,
            HeightRequest = 80
        };

        Image = image;
    }
    
    public override List<Tuple<int, int>> GetPossibleMoves(IBoardService boardService)
    {
        List<Tuple<int, int>> possibleMoves = new List<Tuple<int, int>>();
        List<Tuple<int, int>> movements = new List<Tuple<int, int>>
        {
            Tuple.Create(2, 1),
            Tuple.Create(2, -1),
            Tuple.Create(-2, 1),
            Tuple.Create(-2, -1),
            Tuple.Create(1, 2),
            Tuple.Create(1, -2),
            Tuple.Create(-1, 2),
            Tuple.Create(-1, -2)
        };

        foreach (var move in movements)
        {
            int nextX = X + move.Item1;
            int nextY = Y + move.Item2;

            if (boardService.IsMoveWithinBounds(nextX, nextY))
            {
                if (!boardService.IsOccupied(nextX, nextY) || boardService.IsEnemyPiece(nextX, nextY, Color))
                    possibleMoves.Add(Tuple.Create(nextX, nextY));
            }
        }

        return possibleMoves;
    }
}
