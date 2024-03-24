using System;
using System.Collections.Generic;
using Brilliant.Interfaces;
using Brilliant.Models;
using Microsoft.Maui.Controls;

namespace Brilliant.Pieces;

public class Rook : ChessPiece
{
    public Rook(string color, int x, int y) : base(color, x, y)
    {
        var image = new Image
        {
            Source = $"{color}_rook.png",
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
        List<Tuple<int, int>> directions = new List<Tuple<int, int>>
        {
            Direction.Left,
            Direction.Right,
            Direction.Up,
            Direction.Down
        };

        foreach (var direction in directions)
        {
            int nextX = X + direction.Item1;
            int nextY = Y + direction.Item2;

            while (boardService.IsMoveWithinBounds(nextX, nextY))
            {
                if (!boardService.IsOccupied(nextX, nextY))
                {
                    possibleMoves.Add(Tuple.Create(nextX, nextY));
                }
                else
                {
                    if (boardService.IsEnemyPiece(nextX, nextY, Color))
                        possibleMoves.Add(Tuple.Create(nextX, nextY));
                    
                    break;
                }

                nextX += direction.Item1;
                nextY += direction.Item2;
            }
        }

        return possibleMoves;
    }
}
