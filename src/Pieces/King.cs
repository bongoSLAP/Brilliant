using System;
using System.Collections.Generic;
using Brilliant.Interfaces;
using Brilliant.Models;
using Microsoft.Maui.Controls;

namespace Brilliant.Pieces;

public class King : ChessPiece
{
    public King(string color, int x, int y) : base(color, x, y)
    {
        var image = new Image
        {
            Source = $"{color}_king.png",
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
            Direction.Down,
            Direction.DiagonalUpRight,
            Direction.DiagonalDownRight,
            Direction.DiagonalUpLeft,
            Direction.DiagonalDownLeft
        };

        foreach (var direction in directions)
        {
            int nextX = X + direction.Item1;
            int nextY = Y + direction.Item2;

            if (boardService.IsMoveWithinBounds(nextX, nextY) && 
                (!boardService.IsOccupied(nextX, nextY) || boardService.IsEnemyPiece(nextX, nextY, Color)))
            {
                possibleMoves.Add(new Tuple<int, int>(nextX, nextY));
            }
        }
        
        return possibleMoves;
    }
}
