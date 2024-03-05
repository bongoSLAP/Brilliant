using System;
using Brilliant.Parsers;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace Brilliant;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        InitializeChessBoard();
    }

    private void InitializeChessBoard()
    {
        const int rows = 8;
        const int columns = 8;
        string[,] setup = 
        {
            { "black_rook.png", "black_knight.png", "black_bishop.png", "black_queen.png", "black_king.png", "black_bishop.png", "black_knight.png", "black_rook.png" },
            { "black_pawn.png", "black_pawn.png", "black_pawn.png", "black_pawn.png", "black_pawn.png", "black_pawn.png", "black_pawn.png", "black_pawn.png" },
            { "", "", "", "", "", "", "", "" },
            { "", "", "", "", "", "", "", "" },
            { "", "", "", "", "", "", "", "" },
            { "", "", "", "", "", "", "", "" },
            { "white_pawn.png", "white_pawn.png", "white_pawn.png", "white_pawn.png", "white_pawn.png", "white_pawn.png", "white_pawn.png", "white_pawn.png" },
            { "white_rook.png", "white_knight.png", "white_bishop.png", "white_queen.png", "white_king.png", "white_bishop.png", "white_knight.png", "white_rook.png" }
        };

        for (int i = 0; i < rows; i++)
        {
            ChessBoardGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        }

        for (int j = 0; j < columns; j++)
        {
            ChessBoardGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
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

                if (!string.IsNullOrEmpty(setup[i, j]))
                {
                    var pieceImage = new Image
                    {
                        Source = $"{setup[i, j]}",
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        WidthRequest = 80,
                        HeightRequest = 80
                    };
                    ChessBoardGrid.Add(pieceImage, j, i);
                }
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
        
        var moves = parser.ParseMoves(pgn);

        foreach (var move in moves)
        {

        }
    }
}