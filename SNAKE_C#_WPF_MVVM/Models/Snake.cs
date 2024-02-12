using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNAKE_WPF_MVVM.ViewModels;

namespace SNAKE_WPF_MVVM.Models
{
    class Snake
    {
        public Queue<CellVM> SnakeCells { get; } = new Queue<CellVM>();

        private List<List<CellVM>> _allCells;

        private CellVM _start;

        public Snake(List<List<CellVM>> allCells, CellVM start)
        {
            _start = start;
            _allCells = allCells;
            _start.CellType = CellType.Snake;
            SnakeCells.Enqueue(start);
        }

        public void Move(MoveDirection direction)
        {
            var leaderCell = SnakeCells.Peek();
            int nextRow = leaderCell.Row;
            int nextColumn = leaderCell.Column;

            switch (direction)
            {
                case MoveDirection.Right:
                    nextColumn++;
                    break;

                case MoveDirection.Left:
                    nextColumn--;
                    break;

                case MoveDirection.Up:
                    nextRow--;
                    break;

                case MoveDirection.Down:
                    nextRow++;
                    break;

                default:
                    break;
            }

            var nextCell = _allCells[nextRow][nextColumn];

            try
            {
                switch (nextCell?.CellType)
                {
                    case CellType.None:
                        nextCell.CellType = CellType.Snake;
                        SnakeCells.Dequeue().CellType = CellType.None;
                        SnakeCells.Enqueue(nextCell);
                        break;

                    case CellType.Food:
                        nextCell.CellType = CellType.Snake;
                        SnakeCells.Enqueue(nextCell);
                        break;

                    default:
                        throw _gameOverEx;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                throw _gameOverEx;
            }
        }
        private Exception _gameOverEx => new Exception("Game over");
    }
}
