using Prism.Mvvm;
using SNAKE_WPF_MVVM.Models;

namespace SNAKE_WPF_MVVM.ViewModels
{
    class CellVM : BindableBase
    {
        public int Row { get; }
        public int Column { get; }

        private CellType _cellType = CellType.None;

        public CellType CellType
        {
            get => _cellType;
            set 
            { 
                _cellType = value; 
                RaisePropertyChanged(nameof(CellType));
            }
        }

        public CellVM(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
