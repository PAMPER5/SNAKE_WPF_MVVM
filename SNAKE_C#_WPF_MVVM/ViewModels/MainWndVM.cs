using Prism.Commands;
using Prism.Mvvm;
using SNAKE_WPF_MVVM.Models;
using System.Windows;
using System.Windows.Input;

namespace SNAKE_WPF_MVVM.ViewModels
{
    class MainWndVM : BindableBase
    {
		private bool _continueGame;

		public bool ContinueGame
		{
			get => _continueGame;
			private set
			{ 
				_continueGame = value; 
				RaisePropertyChanged(nameof(ContinueGame));

				if (_continueGame) SnakeGo();
			}
		}

		public List<List<CellVM>> AllCells { get; } = new List<List<CellVM>>();

		public DelegateCommand StartStopCommand { get; }
		private MoveDirection _currqntMoveDirection = MoveDirection.Right;

		private int _rowCount = 13;
		private int _columnCount = 13;
		private const int SPEED = 300;
		private int _speed = SPEED;

		private Snake _snake;
		private MainWindow _mainWnd;
		private CellVM _lastFood;

		public MainWndVM(MainWindow mainWnd) 
		{
			_mainWnd = mainWnd;
			StartStopCommand = new DelegateCommand(() => ContinueGame = !ContinueGame);

			for (int row = 0; row < _rowCount; row++) 
			{
				var rowList = new List<CellVM>();
				for (int column = 0; column < _columnCount; column++)
				{
					var cell = new CellVM(row, column);
					rowList.Add(cell);
				}
				AllCells.Add(rowList);
			}

            _snake = new Snake(AllCells, AllCells[_rowCount / 2][_columnCount / 2], CreateFood);
			CreateFood();


            _mainWnd.KeyDown += UserKeyDown;
        }

		private async Task SnakeGo()
		{
			while (ContinueGame) 
			{
				await Task.Delay(_speed);

				try
				{
					_snake.Move(_currqntMoveDirection);
				}
				catch(Exception)
				{
					ContinueGame = false;
					MessageBox.Show("Game over");
					_speed = SPEED;
					_snake.Restart();
					_lastFood.CellType = CellType.None;
					CreateFood();
				}
			}
		}

		private void UserKeyDown(object sender, KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.A:
					if (_currqntMoveDirection != MoveDirection.Right)
						_currqntMoveDirection = MoveDirection.Left;
					break;
                case Key.D:
                    if (_currqntMoveDirection != MoveDirection.Left)
                        _currqntMoveDirection = MoveDirection.Right;
                    break;
                case Key.W:
                    if (_currqntMoveDirection != MoveDirection.Down)
                        _currqntMoveDirection = MoveDirection.Up;
                    break;
                case Key.S:
                    if (_currqntMoveDirection != MoveDirection.Up)
                        _currqntMoveDirection = MoveDirection.Down;
                    break;

                case Key.Left:
                    if (_currqntMoveDirection != MoveDirection.Right)
                        _currqntMoveDirection = MoveDirection.Left;
                    break;
                case Key.Right:
                    if (_currqntMoveDirection != MoveDirection.Left)
                        _currqntMoveDirection = MoveDirection.Right;
                    break;
                case Key.Up:
                    if (_currqntMoveDirection != MoveDirection.Down)
                        _currqntMoveDirection = MoveDirection.Up;
                    break;
                case Key.Down:
                    if (_currqntMoveDirection != MoveDirection.Up)
                        _currqntMoveDirection = MoveDirection.Down;
                    break;
				default:
					break;
            }
		}

		private void CreateFood()
		{
			var random = new Random();

			int row = random.Next(0, _rowCount);
			int column = random.Next(0, _columnCount);

			_lastFood = AllCells[row][column];

			if (_snake.SnakeCells.Contains(_lastFood))
			{
				CreateFood();
			}

			_lastFood.CellType = CellType.Food;
			_speed = (int)(_speed * 0.95);
		}
    }
}
