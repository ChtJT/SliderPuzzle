using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SliderPuzzleGameExtension;

public static class MapPainter
{
    public static void DrawMap(Map map, Grid grid, bool isSolved)
    {
        grid.RowDefinitions.Clear();
        grid.ColumnDefinitions.Clear();
        grid.Children.Clear();

        var brush = new SolidColorBrush(Color.FromRgb(127, 127, 0));
        if (isSolved) brush = new SolidColorBrush(Color.FromRgb(0, 127, 0));

        var bitmap = new BitmapImage(new Uri("fallbackWallpaper.png", UriKind.Relative));
        var width = bitmap.PixelWidth / map.ColCount;
        var height = bitmap.PixelHeight / map.RowCount;

        for (var row = 0; row < map.RowCount; row++) grid.RowDefinitions.Add(new RowDefinition());
        for (var col = 0; col < map.ColCount; col++) grid.ColumnDefinitions.Add(new ColumnDefinition());
        for (var row = 0; row < map.RowCount; row++)
            for (var col = 0; col < map.ColCount; col++)
            {
                var id = map[row, col];

                if (id == 0)
                {
                    if (!isSolved) continue;
                    id = map.RowCount * map.ColCount;
                }
                var colIndex = map.GetRealColIndexFromId(id);
                var rowIndex = map.GetRealRowIndexFromId(id);
                var rect = new Int32Rect(colIndex * width, rowIndex * height, width,
                    height);
                var label = new Label
                {
                    Content = id,
                    Background = new ImageBrush(new CroppedBitmap(bitmap, rect)),
                    BorderBrush = brush,
                    BorderThickness = new Thickness(1)
                };
                DrawElement(grid, label, row, col);
            }
    }

    private static void DrawElement(Grid mainGrid, Label uiElement, int row, int col)
    {
        mainGrid.Children.Add(uiElement);
        Grid.SetRow(uiElement, row);
        Grid.SetColumn(uiElement, col);
    }
}