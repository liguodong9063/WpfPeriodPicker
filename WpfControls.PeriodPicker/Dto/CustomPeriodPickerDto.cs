using System.Collections.Generic;
using WpfControls.PeriodPicker.Infrastructure;

namespace WpfControls.PeriodPicker.Dto
{
    /// <summary>
    /// 控件原始数据源对象
    /// </summary>
    public class CustomPeriodPickerDto
    {
        public int Index { get; set; }
        public string DisplayName { get; set; }
        /// <summary>
        /// 所有的核算期
        /// </summary>
        public List<CustomPeriodPickerCellDto> Cells { get; set; }
    }

    /// <summary>
    /// 控件原始数据源单元格对象
    /// </summary>
    public class CustomPeriodPickerCellDto
    {
        public string DisplayName { get; set; }
        public string Value { get; set; }
        public int? Id { get; set; }
    }

    /// <summary>
    /// 控件弹出框区域行对象
    /// </summary>
    public class CustomPeriodPickerDisplayRowDto:ModelBase
    {
        private bool _isCell1Enable;
        private bool _isCell2Enable;
        private bool _isCell3Enable;
        private bool _isCell4Enable;
        private string _cell1DisplayName;
        private string _cell2DisplayName;
        private string _cell3DisplayName;
        private string _cell4DisplayName;
        private string _cell1Value;
        private string _cell2Value;
        private string _cell3Value;
        private string _cell4Value;
        private bool _isCell1Selected;
        private bool _isCell2Selected;
        private bool _isCell3Selected;
        private bool _isCell4Selected;
        private int? _cell1Id;
        private int? _cell2Id;
        private int? _cell3Id;
        private int? _cell4Id;

        public bool IsCell1Enable
        {
            get => _isCell1Enable;
            set
            {
                if (_isCell1Enable == value) return;
                _isCell1Enable = value;
                RaisePropertyChanged(nameof(IsCell1Enable));
            }
        }

        public bool IsCell2Enable
        {
            get => _isCell2Enable;
            set
            {
                if (_isCell2Enable == value) return;
                _isCell2Enable = value;
                RaisePropertyChanged(nameof(IsCell2Enable));
            }
        }

        public bool IsCell3Enable
        {
            get => _isCell3Enable;
            set
            {
                if (_isCell3Enable == value) return;
                _isCell3Enable = value;
                RaisePropertyChanged(nameof(IsCell3Enable));
            }
        }

        public bool IsCell4Enable
        {
            get => _isCell4Enable;
            set
            {
                if (_isCell4Enable == value) return;
                _isCell4Enable = value;
                RaisePropertyChanged(nameof(IsCell4Enable));
            }
        }

        public string Cell1DisplayName
        {
            get => _cell1DisplayName;
            set
            {
                if (_cell1DisplayName == value) return;
                _cell1DisplayName = value;
                RaisePropertyChanged(nameof(Cell1DisplayName));
            }
        }

        public string Cell2DisplayName
        {
            get => _cell2DisplayName;
            set
            {
                if (_cell2DisplayName == value) return;
                _cell2DisplayName = value;
                RaisePropertyChanged(nameof(Cell2DisplayName));
            }
        }

        public string Cell3DisplayName
        {
            get => _cell3DisplayName;
            set
            {
                if (_cell3DisplayName == value) return;
                _cell3DisplayName = value;
                RaisePropertyChanged(nameof(Cell3DisplayName));
            }
        }

        public string Cell4DisplayName
        {
            get => _cell4DisplayName;
            set
            {
                if (_cell4DisplayName == value) return;
                _cell4DisplayName = value;
                RaisePropertyChanged(nameof(Cell4DisplayName));
            }
        }


        public string Cell1Value
        {
            get => _cell1Value;
            set
            {
                if (_cell1Value == value) return;
                _cell1Value = value;
                RaisePropertyChanged(nameof(Cell1Value));
            }
        }

        public string Cell2Value
        {
            get => _cell2Value;
            set
            {
                if (_cell2Value == value) return;
                _cell2Value = value;
                RaisePropertyChanged(nameof(Cell2Value));
            }
        }

        public string Cell3Value
        {
            get => _cell3Value;
            set
            {
                if (_cell3Value == value) return;
                _cell3Value = value;
                RaisePropertyChanged(nameof(Cell3Value));
            }
        }

        public string Cell4Value
        {
            get => _cell4Value;
            set
            {
                if (_cell4Value == value) return;
                _cell4Value = value;
                RaisePropertyChanged(nameof(Cell4Value));
            }
        }

        public bool IsCell1Selected
        {
            get => _isCell1Selected;
            set
            {
                if (_isCell1Selected == value) return;
                _isCell1Selected = value;
                RaisePropertyChanged(nameof(IsCell1Selected));
            }
        }

        public bool IsCell2Selected
        {
            get => _isCell2Selected;
            set
            {
                if (_isCell2Selected == value) return;
                _isCell2Selected = value;
                RaisePropertyChanged(nameof(IsCell2Selected));
            }
        }

        public bool IsCell3Selected
        {
            get => _isCell3Selected;
            set
            {
                if (_isCell3Selected == value) return;
                _isCell3Selected = value;
                RaisePropertyChanged(nameof(IsCell3Selected));
            }
        }

        public bool IsCell4Selected
        {
            get => _isCell4Selected;
            set
            {
                if (_isCell4Selected == value) return;
                _isCell4Selected = value;
                RaisePropertyChanged(nameof(IsCell4Selected));
            }
        }

        public int? Cell1Id
        {
            get => _cell1Id;
            set
            {
                if (_cell1Id == value) return;
                _cell1Id = value;
                RaisePropertyChanged(nameof(Cell1Id));
            }
        }

        public int? Cell2Id
        {
            get => _cell2Id;
            set
            {
                if (_cell2Id == value) return;
                _cell2Id = value;
                RaisePropertyChanged(nameof(Cell2Id));
            }
        }

        public int? Cell3Id
        {
            get => _cell3Id;
            set
            {
                if (_cell3Id == value) return;
                _cell3Id = value;
                RaisePropertyChanged(nameof(Cell3Id));
            }
        }

        public int? Cell4Id
        {
            get => _cell4Id;
            set
            {
                if (_cell4Id == value) return;
                _cell4Id = value;
                RaisePropertyChanged(nameof(Cell4Id));
            }
        }
    }
}