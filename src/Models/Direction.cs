using System;

namespace Brilliant.Models;

public static class Direction
{
    public static readonly Tuple<int, int> Left = Tuple.Create(-1, 0);
    public static readonly Tuple<int, int> Right = Tuple.Create(1, 0);
    public static readonly Tuple<int, int> Up = Tuple.Create(0, 1);
    public static readonly Tuple<int, int> Down = Tuple.Create(0, -1);
    public static readonly Tuple<int, int> DiagonalUpRight = Tuple.Create(1, 1);
    public static readonly Tuple<int, int> DiagonalDownRight = Tuple.Create(1, -1);
    public static readonly Tuple<int, int> DiagonalUpLeft = Tuple.Create(-1, 1);
    public static readonly Tuple<int, int> DiagonalDownLeft = Tuple.Create(-1, -1);
}
