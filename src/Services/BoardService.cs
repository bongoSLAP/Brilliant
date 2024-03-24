using System;
using System.Collections.Generic;
using System.Linq;
using Brilliant.Interfaces;
using Brilliant.Models;
using Brilliant.Pieces;

namespace Brilliant.Services;

public class BoardService : IBoardService
{
    private static BoardService _instance;
    private readonly Board _board;
    private readonly Dictionary<(Type, string), List<Tuple<int, int>>> _positions;

    public BoardService(Board board)
    {
        _board = board;
        _positions = new Dictionary<(Type, string), List<Tuple<int, int>>>();
        UpdatePiecePositions();
    }
    
    public static BoardService GetInstance(Board board)
    {
        return _instance ??= new BoardService(board);
    }
    
    private void UpdatePiecePositions()
    {
        _positions.Clear();
        
        for (int y = 0; y < _board.Grid.Count; y++)
        {
            for (int x = 0; x < _board.Grid[y].Count; x++)
            {
                ChessPiece piece = _board.Grid[y][x];

                if (piece == null)
                    continue;

                (Type, string Color) key = (piece.GetType(), piece.Color);
                if (!_positions.ContainsKey(key))
                {
                    _positions[key] = new List<Tuple<int, int>>();
                }
                _positions[key].Add(Tuple.Create(x, y));
            }
        }
    }

    public List<ChessPiece> FindPieces(Type pieceType, string color)
    {
        var key = (pieceType, color);
        var piecePositions = _positions.TryGetValue(key, out var positions) ? positions : new List<Tuple<int, int>>();
        
        return piecePositions
            .Select(position => _board.Grid[position.Item2][position.Item1])
            .Where(piece => piece != null && piece.GetType() == pieceType && piece.Color == color)
            .ToList(); 
    }
    
    public ChessPiece GetPieceAt(int x, int y) => _board.Grid[y][x];

    public bool IsOccupied(int x, int y) => _board.Grid[y][x] != null;
    
    public bool IsEnemyPiece(int x, int y, string color) => _board.Grid[y][x] != null && _board.Grid[y][x].Color != color;
    
    public bool IsMoveWithinBounds(int x, int y) => x is >= 0 and < 8 && y is >= 0 and < 8;
    
}