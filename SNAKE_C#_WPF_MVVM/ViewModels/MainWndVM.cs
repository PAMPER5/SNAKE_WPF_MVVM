using Prism.Commands;
using Prism.Mvvm;
using SNAKE_C__WPF_MVVM.Models;

namespace SNAKE_C__WPF_MVVM.ViewModels
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
			}
		}

		public List<List<CellVM>> AllCells { get; } = new List<List<CellVM>>();

		public DelegateCommand StartStopCommand { get; }
		private MoveDirection _currqntMoveDirection = MoveDirection.Right;

		private int _rowCount = 10;
		private int _columnCount = 10;

		public MainWndVM() 
		{
			StartStopCommand = new DelegateCommand(StartStop);

			for (int row = 0; row < _rowCount; row++) 
			{
				var rowList = new List<CellVM>();
				for (int column = 0; column < _columnCount; column++)
				{
					var cell = new CellVM(row, column);
				}
				AllCells.Add(rowList);
			}
		}

		private void StartStop()
		{
			ContinueGame = !ContinueGame;
		}
    }
}
