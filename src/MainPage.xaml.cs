using System;
using System.Collections.Generic;
using System.Linq;
using Brilliant.Interfaces;
using Brilliant.Models;
using Brilliant.Parsers;
using Brilliant.Pieces;
using Brilliant.Services;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace Brilliant;

public partial class MainPage : ContentPage
{
    private readonly IPieceFactory _pieceFactory;
    private IBoardService _boardService;
    
    public MainPage()
    {
        _pieceFactory = new PieceFactory();
        InitializeComponent();
        InitializeChessBoard();
    }

    private void InitializeChessBoard()
    {
        const int rows = 8;
        const int columns = 8;
        
        var emptySquares = new List<ChessPiece> { null, null, null, null, null, null, null, null };
        var board = new Board();
        
        board.Grid.Add(CreateBackRank("black"));
        board.Grid.Add(CreatePawnRow("black"));
        board.Grid.Add(emptySquares);
        board.Grid.Add(emptySquares);
        board.Grid.Add(emptySquares);
        board.Grid.Add(emptySquares);
        board.Grid.Add(CreatePawnRow("white"));
        board.Grid.Add(CreateBackRank("white"));
        _boardService = new BoardService(board);

        for (int i = 0; i < rows; i++)
        {
            ChessBoardGrid.RowDefinitions.Add(new RowDefinition
            {
                Height = new GridLength(1, GridUnitType.Star)
            });
        }

        for (int j = 0; j < columns; j++)
        {
            ChessBoardGrid.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            });
        }

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                var square = new BoxView
                {
                    Color = (i + j) % 2 == 0 ? Colors.SandyBrown : Colors.SaddleBrown
                };
                ChessBoardGrid.Add(square, j, i);

                var chessPiece = board.Grid[i][j];
                if (chessPiece != null)
                {
                    ChessBoardGrid.Add(chessPiece.Image, j, i);
                }
                
                var label = new Label
                {
                    Text = $"x:{j}, y:{i}",
                    TextColor = Colors.Black
                };
                    
                ChessBoardGrid.Add(label, j, i);
            }
        }

        PgnParser parser = new PgnParser();

        string pgn = """
                     [Event "Live Chess"]
                     [Site "Chess.com"]
                     [Date "2024.03.02"]
                     [Round "?"]
                     [White "boolean0101"]
                     [Black "MBogdan05"]
                     [Result "0-1"]
                     [ECO "D04"]
                     [WhiteElo "352"]
                     [BlackElo "398"]
                     [TimeControl "60"]
                     [EndTime "9:33:08 PST"]
                     [Termination "MBogdan05 won on time"]

                     1. d4 d5 2. Nf3 Nf6 3. e3 Nc6 4. c4 dxc4 5. Bxc4 Ne5 6. Nxe5 Nh5 7. Qxh5 g6 8.
                     Qe2 e6 9. O-O c6 10. Nc3 Bd7 11. e4 Qc7 12. Bg5 Be7 13. Qg4 Bxg5 14. Qxg5 f6 15.
                     Qxf6 Rg8 16. Bxe6 Bxe6 17. Qxe6+ Qe7 18. Qxe7+ Kxe7 19. Rfe1 c5 20. dxc5 b6 21.
                     cxb6 axb6 22. Rad1 Rac8 23. Rd7+ Ke8 24. Nb5 Kf8 25. Nc6 Rxc6 0-1
                     """;

        var moves = parser.GetMoves(pgn);
        GetMoves(moves);
    }

    public void GetMoves(List<Tuple<ChessMove, ChessMove>> moves)
    {
        foreach (Tuple<ChessMove, ChessMove> movePair in moves)
        {
            ChessMove whiteMove = movePair.Item1;
            ChessMove blackMove = movePair.Item2;
            
            var color = "white";
            whiteMove.MoveFrom = whiteMove.CleanText[0] switch
            {
                'K' => GetMoveFrom(whiteMove, typeof(King), color),
                'Q' => GetMoveFrom(whiteMove, typeof(Queen), color),
                'R' => GetMoveFrom(whiteMove, typeof(Rook), color),
                'B' => GetMoveFrom(whiteMove, typeof(Bishop), color),
                'N' => GetMoveFrom(whiteMove, typeof(Knight), color),
                _ => GetMoveFrom(whiteMove, typeof(Pawn), color),
            };

            if (blackMove is null)
                break;
            
            color = "black";
            blackMove.MoveFrom = blackMove.CleanText[0] switch
            {
                'K' => GetMoveFrom(blackMove, typeof(King), color),
                'Q' => GetMoveFrom(blackMove, typeof(Queen), color),
                'R' => GetMoveFrom(blackMove, typeof(Rook), color),
                'B' => GetMoveFrom(blackMove, typeof(Bishop), color),
                'N' => GetMoveFrom(blackMove, typeof(Knight), color),
                _ => GetMoveFrom(blackMove, typeof(Pawn), color),
            };
        }
    }
    
    private Tuple<int, int> GetMoveFrom(ChessMove chessMove, Type pieceType, string color)
    {
        List<ChessPiece> pieces = _boardService.FindPieces(pieceType, color);

        if (pieceType != typeof(King))
        {
            var foo = pieces
                .SelectMany(piece => piece.GetPossibleMoves(_boardService).ToList());

            var foo2 = "";
            
            return pieces
                .SelectMany(piece => piece.GetPossibleMoves(_boardService)
                .Select(move => new { Piece = piece, Move = move }))
                .First(pair => Equals(pair.Move, chessMove.MoveTo))?.Piece.CurrentPosition;
        }
        
        return pieces
            .First()
            .GetPossibleMoves(_boardService)
            .First(m => Equals(m, chessMove.MoveTo));
    }

    private List<ChessPiece> CreateBackRank(string color)
    {
        List<string> pieceNames = new()
        {
            "rook",
            "knight",
            "bishop",
            "queen",
            "king",
            "bishop",
            "knight",
            "rook"
        };

        return pieceNames.Select((t, i) =>
            _pieceFactory.CreateChessPiece(t, color, i, color == "white" ? 7 : 0)).ToList();
    }

    private List<ChessPiece> CreatePawnRow(string color)
    {
        List<ChessPiece> pieces = new();

        for (int i = 0; i < 8; i++)
        {
            pieces.Add(_pieceFactory.CreateChessPiece("pawn", color, i, color == "white" ? 6 : 1));
        }

        return pieces;
    }
}