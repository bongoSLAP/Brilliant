using Brilliant.Drawables;

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
                    Color = (i + j) % 2 == 0 ? Colors.White : Colors.Black
                };
                ChessBoardGrid.Add(square, j, i);

                if (!string.IsNullOrEmpty(setup[i, j]))
                {
                    var pieceImage = new Image
                    {
                        Source = $"Resources/Images/{setup[i, j]}",
                        Aspect = Aspect.AspectFit,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        WidthRequest = 60, // Set the desired width
                        HeightRequest = 60 // Set the desired height
                    };
                    ChessBoardGrid.Add(pieceImage, j, i);
                }
            }
        }

        this.Layout;
    }
}