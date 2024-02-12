using Prism.Commands;
using Prism.Mvvm;
using SNAKE_WPF_MVVM.Models;
using System.Windows;

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

		private int _rowCount = 10;
		private int _columnCount = 10;

		private Snake _snake;

		public MainWndVM() 
		{
			StartStopCommand = new DelegateCommand(StartStop);

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

            _snake = new Snake(AllCells, AllCells[_rowCount / 2][_columnCount / 2]);

        }
		private void StartStop()
		{
			ContinueGame = !ContinueGame;
		}

		private async Task SnakeGo()
		{
			while (ContinueGame) 
			{
				await Task.Delay(300);

				try
				{
					_snake.Move(_currqntMoveDirection);
				}
				catch(Exception ex)
				{
					ContinueGame = false;
					MessageBox.Show(ex.Message);
				}
			}
		}
    }
}
