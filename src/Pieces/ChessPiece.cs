using System;
using System.Collections.Generic;
using Brilliant.Interfaces;
using Microsoft.Maui.Controls;

namespace Brilliant.Pieces;

public abstract class ChessPiece
{
    protected int X { get; set; }
    protected int Y { get; set; }
    public Image Image { get; set; }
    public string Color { get; set; }
    public Tuple<int, int> CurrentPosition => Tuple.Create(X, Y);

    protected ChessPiece(string color, int x, int y)
    {
        Color = color;
        X = x;
        Y = y;
    }

    public abstract List<Tuple<int, int>> GetPossibleMoves(IBoardService boardService);
}

