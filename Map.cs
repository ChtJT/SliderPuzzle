using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Documents;
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
    public Map Clone()
    {
        int rows = _matrix.GetLength(0);
        int cols = _matrix.GetLength(1);
        int[,] newBoard = new int[rows, cols];
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                newBoard[row, col] = _matrix[row, col];
            }
        }

        return new Map(newBoard); // 假设 Map 类有一个接受二维数组的构造函数
    }
    public bool IsGoal(Map goal)
    {
        for (int i = 0; i < _matrix.GetLength(0); i++)
        {
            for (int j = 0; j < _matrix.GetLength(1); j++)
            {
                if (_matrix[i, j] != goal._matrix[i, j])
                {
                    return false;
                }
            }
        }
        return true;
    }
    public List<Map> GetNeighbors()
    {
        List<Map> neighbors = new List<Map>();
        // 找到空白格的位置
        (int row, int col) = FindBlankSpace();

        // 尝试移动：上，下，左，右
        int[] dx = { -1, 1, 0, 0 };
        int[] dy = { 0, 0, -1, 1 };

        for (int i = 0; i < 4; i++)
        {
            int newRow = row + dx[i];
            int newCol = col + dy[i];

            if (IsValidMove(newRow, newCol))
            {
                // 生成新的地图状态
                Map newMap = Clone();
                newMap.Swap(row, col, newRow, newCol);
                neighbors.Add(newMap);
            }
        }

        return neighbors;
    }
    private (int, int) FindBlankSpace()
    {
        for (int row = 0; row < _matrix.GetLength(0); row++)
        {
            for (int col = 0; col < _matrix.GetLength(1); col++)
            {
                if (_matrix[row, col] == 0) // 假设空白格用 0 表示
                {
                    return (row, col);
                }
            }
        }
        throw new InvalidOperationException("No blank space found in the map.");
    }
    private bool IsValidMove(int row, int col)
    {
        return row >= 0 && row < _matrix.GetLength(0) && col >= 0 && col < _matrix.GetLength(1);
    }
    private void Swap(int row1, int col1, int row2, int col2)
    {
        int temp = _matrix[row1, col1];
        _matrix[row1, col1] = _matrix[row2, col2];
        _matrix[row2, col2] = temp;
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