using System;
using System.Drawing;
using System.Windows.Input;

namespace SliderPuzzleGameExtension;

public class Map
{
    private readonly int[,] _matrix;

    private int _zeroCol;

    private int _zeroRow;

    public Map(int[,] matrix)
    {
        _matrix = matrix;
        var counter = 0;
        for (var i = 0; i < RowCount; i++)
            for (var j = 0; j < ColCount; j++)
            {
                if (_matrix[i, j] == 0)
                {
                    _zeroRow = i;
                    _zeroCol = j;
                    counter++;
                }
            }
        if (counter != 1) throw new Exception("Invalid map");
    }

    public int ColCount => _matrix.GetLength(1);
    public int RowCount => _matrix.GetLength(0);
    public int this[int i, int j] => _matrix[i, j];

    public int GetRealColIndexFromId(int id)
    {
        return (id - 1) % ColCount;
    }

    public int GetRealRowIndexFromId(int id)
    {
        return (id - 1) / ColCount;
    }

    public bool IsSolved()
    {
        for (var i = 0; i < RowCount; i++)
            for (var j = 0; j < ColCount; j++)
                if ((i != RowCount - 1 || j != ColCount - 1) && _matrix[i, j] != i * ColCount + j + 1)
                    return false;

        return true;
    }
    public int[,] GetCurrentMap()
    {
        // 返回地图状态的一个副本，以防止外部修改
        return (int[,])_matrix.Clone();
    }
    public void Move(Key key)
    {
        switch (key)
        {
            case Key.Up:
                if (_zeroRow == 0) return;
                _matrix[_zeroRow, _zeroCol] = _matrix[_zeroRow - 1, _zeroCol];
                _matrix[_zeroRow - 1, _zeroCol] = 0;
                _zeroRow--;
                break;

            case Key.Down:
                if (_zeroRow == RowCount - 1) return;
                _matrix[_zeroRow, _zeroCol] = _matrix[_zeroRow + 1, _zeroCol];
                _matrix[_zeroRow + 1, _zeroCol] = 0;
                _zeroRow++;
                break;

            case Key.Left:
                if (_zeroCol == 0) return;
                _matrix[_zeroRow, _zeroCol] = _matrix[_zeroRow, _zeroCol - 1];
                _matrix[_zeroRow, _zeroCol - 1] = 0;
                _zeroCol--;
                break;

            case Key.Right:
                if (_zeroCol == ColCount - 1) return;
                _matrix[_zeroRow, _zeroCol] = _matrix[_zeroRow, _zeroCol + 1];
                _matrix[_zeroRow, _zeroCol + 1] = 0;
                _zeroCol++;
                break;
        }
    }
}