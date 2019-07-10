using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WpfControls.PeriodPicker.Dto;
using WpfControls.PeriodPicker.Infrastructure.Enums;
using WpfControls.PeriodPicker.UserControls;

namespace WpfControls.PeriodPicker.View
{
    public partial class PeriodPickerPopupView
    {
        /// <summary>
        /// 列数量
        /// </summary>
        private const int ColumnSize = 4;
        /// <summary>
        /// 总单元格数量
        /// </summary>
        private const int TotalCellSize = 12;

        private int _selectedIndex;
        private int? _selectedId;
        private string _selectedValue;

        private readonly PeriodPickerMode _mode;

        private List<CustomPeriodPickerDto> _dataSources;
        private List<CustomPeriodPickerDisplayRowDto> _displayRows;

        /// <summary>
        /// 确定选择事件
        /// </summary>
        public Action<int?,string> SelectedValueChangedAction;

        public PeriodPickerPopupView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 自然月模式
        /// </summary>
        /// <param name="selectedValue"></param>
        public PeriodPickerPopupView(string selectedValue) : this()
        {
            _mode = PeriodPickerMode.Month;
            if (string.IsNullOrEmpty(selectedValue)||selectedValue.Length<4||!int.TryParse(selectedValue.Substring(0,4),out var year))
            {
                year = DateTime.Now.Year;
            }
            SelectedYear = year;
            ChangeMonthModeSelectedValue(selectedValue);
        }

        /// <summary>
        /// 期间模式
        /// </summary>
        /// <param name="dataSources"></param>
        /// <param name="selectedId"></param>
        public PeriodPickerPopupView(List<CustomPeriodPickerDto> dataSources, int? selectedId) : this()
        {
            _mode = PeriodPickerMode.Period;

            //初始化Index
            var index = 0;
            foreach (var item in dataSources)
            {
                item.Index = ++index;
            }
            _dataSources = dataSources;

            ChangePeriodModeSelectedValue(true, selectedId);
        }

        /// <summary>
        /// 当前页（用于期间模式）
        /// </summary>
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                //if (_selectedIndex == value) return;
                _selectedIndex = value;
                SelectedYearTextBlock.Text = _dataSources.FirstOrDefault(a => a.Index == _selectedIndex)?.DisplayName;
            }
        }

        /// <summary>
        /// 当前页（用于期间模式）
        /// </summary>
        public int? SelectedId
        {
            get => _selectedId;
            set
            {
                //if (_selectedId == value) return;
                _selectedId = value;
                ChangePeriodModeSelectedValue(true, _selectedId);
            }
        }

        /// <summary>
        /// 选中年度（用于月模式）
        /// </summary>
        public int SelectedYear { get; set; }

        /// <summary>
        /// 当前选中值
        /// </summary>
        public string SelectedValue
        {
            private get => _selectedValue;
            set
            {
                _selectedValue = value;
                ChangeMonthModeSelectedValue(_selectedValue);
            }
        }

        /// <summary>
        /// 上一年度按钮点击事件
        /// </summary>
        /// <param name="cSender"></param>
        /// <param name="cE"></param>
        private void PreYearButton_OnClick(object cSender, RoutedEventArgs cE)
        {
            if (_mode != PeriodPickerMode.Month)
            {
                SelectedIndex = SelectedIndex - 1;
            }
            else
            {
                SelectedYear--;
            }
            Bind(SelectedIndex);
        }

        /// <summary>
        /// 下一年度按钮点击事件
        /// </summary>
        /// <param name="cSender"></param>
        /// <param name="cE"></param>
        private void NextYearButton_OnClick(object cSender, RoutedEventArgs cE)
        {
            if (_mode != PeriodPickerMode.Month)
            {
                SelectedIndex = SelectedIndex + 1;
            }
            else
            {
                SelectedYear++;
            }
            Bind(SelectedIndex);
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="selectedIndex"></param>
        private void Bind(int selectedIndex)
        {
            if (_mode == PeriodPickerMode.Month)
            {
                SelectedYearTextBlock.Text = SelectedYear.ToString("D4");
                var cells = new List<CustomPeriodPickerCellDto>();
                for (var i = 1; i <= 12; i++)
                {
                    cells.Add(new CustomPeriodPickerCellDto
                    {
                        DisplayName = $"{i:D2}月",
                        Value = $"{SelectedYear}{i:D2}"
                    });
                }
                _dataSources = new List<CustomPeriodPickerDto>
                {
                    new CustomPeriodPickerDto
                    {
                        Index=1,
                        DisplayName = SelectedYear.ToString(),
                        Cells =cells
                    }
                };
            }
            _displayRows = new List<CustomPeriodPickerDisplayRowDto>();
            var toDisplayCells = _dataSources.FirstOrDefault(a => _mode==PeriodPickerMode.Month || a.Index == selectedIndex)?.Cells.Skip(TotalCellSize * 0).ToList() ?? new List<CustomPeriodPickerCellDto>();
            var displayViewRowCount = Math.Ceiling((double)toDisplayCells.Count / ColumnSize);

            for (var i = 0; i < displayViewRowCount; i++)
            {
                var rowDto = new CustomPeriodPickerDisplayRowDto
                {
                    IsCell1Enable = toDisplayCells.Count >= i * ColumnSize + 1,
                    IsCell2Enable = toDisplayCells.Count >= i * ColumnSize + 2,
                    IsCell3Enable = toDisplayCells.Count >= i * ColumnSize + 3,
                    IsCell4Enable = toDisplayCells.Count >= i * ColumnSize + 4
                };
                if (rowDto.IsCell1Enable)
                {
                    rowDto.Cell1DisplayName = toDisplayCells[i * ColumnSize].DisplayName;
                    rowDto.Cell1Value = toDisplayCells[i * ColumnSize].Value;
                    rowDto.Cell1Id = toDisplayCells[i * ColumnSize].Id;
                    rowDto.IsCell1Selected = _mode==PeriodPickerMode.Month? toDisplayCells[i * ColumnSize].Value == SelectedValue: toDisplayCells[i * ColumnSize].Id == _selectedId;
                }
                if (rowDto.IsCell2Enable)
                {
                    rowDto.Cell2DisplayName = toDisplayCells[i * ColumnSize + 1].DisplayName;
                    rowDto.Cell2Value = toDisplayCells[i * ColumnSize + 1].Value;
                    rowDto.Cell2Id = toDisplayCells[i * ColumnSize+1].Id;
                    rowDto.IsCell2Selected = _mode == PeriodPickerMode.Month ? toDisplayCells[i * ColumnSize+1].Value == SelectedValue : toDisplayCells[i * ColumnSize+1].Id == _selectedId;
                }
                if (rowDto.IsCell3Enable)
                {
                    rowDto.Cell3DisplayName = toDisplayCells[i * ColumnSize + 2].DisplayName;
                    rowDto.Cell3Value = toDisplayCells[i * ColumnSize + 2].Value;
                    rowDto.Cell3Id = toDisplayCells[i * ColumnSize+2].Id;
                    rowDto.IsCell3Selected = _mode == PeriodPickerMode.Month ? toDisplayCells[i * ColumnSize+2].Value == SelectedValue : toDisplayCells[i * ColumnSize+2].Id == _selectedId;
                }
                if (rowDto.IsCell4Enable)
                {
                    rowDto.Cell4DisplayName = toDisplayCells[i * ColumnSize + 3].DisplayName;
                    rowDto.Cell4Value = toDisplayCells[i * ColumnSize + 3].Value;
                    rowDto.Cell4Id = toDisplayCells[i * ColumnSize+3].Id;
                    rowDto.IsCell4Selected = _mode == PeriodPickerMode.Month ? toDisplayCells[i * ColumnSize+3].Value == SelectedValue : toDisplayCells[i * ColumnSize+3].Id == _selectedId;
                }
                _displayRows.Add(rowDto);
            }
            //设置上一年度、下一年度按钮是否可用
            if (_mode == PeriodPickerMode.Month)
            {
                PreViewButton.IsEnabled = true;
                NextViewButton.IsEnabled = true;
            }
            else
            {
                PreViewButton.IsEnabled = _dataSources.Exists(a => a.Index < SelectedIndex);
                NextViewButton.IsEnabled = _dataSources.Exists(a => a.Index > SelectedIndex);
            }
            //设置ListView数据源
            MonthDataSourcesListView.ItemsSource = _displayRows;
        }

        /// <summary>
        /// 日期点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CellButton_OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as CustomCellButton;
            if (button == null) return;
            _selectedValue = button.InputValue;
            _selectedId = button.PeriodId;
            if (_mode == PeriodPickerMode.Month)
            {
                foreach (var rowDto in _displayRows)
                {
                    rowDto.IsCell1Selected = rowDto.Cell1Value == _selectedValue;
                    rowDto.IsCell2Selected = rowDto.Cell2Value == _selectedValue;
                    rowDto.IsCell3Selected = rowDto.Cell3Value == _selectedValue;
                    rowDto.IsCell4Selected = rowDto.Cell4Value == _selectedValue;
                }
            }
            else
            {
                foreach (var rowDto in _displayRows)
                {
                    rowDto.IsCell1Selected = rowDto.Cell1Id == _selectedId;
                    rowDto.IsCell2Selected = rowDto.Cell2Id == _selectedId;
                    rowDto.IsCell3Selected = rowDto.Cell3Id == _selectedId;
                    rowDto.IsCell4Selected = rowDto.Cell4Id == _selectedId;
                }
            }
            SelectedValueChangedAction(_selectedId,_selectedValue);
        }

        /// <summary>
        /// 清除按钮点击事件
        /// </summary>
        /// <param name="cSender"></param>
        /// <param name="cE"></param>
        private void ClearButton_OnClick(object cSender, RoutedEventArgs cE)
        {
            _selectedId = null;
            _selectedValue = string.Empty;
            SelectedValueChangedAction(null,string.Empty);
        }

        /// <summary>
        /// 改变选中值（月度模式）
        /// </summary>
        /// <param name="selectedValue">选中值</param>
        private void ChangeMonthModeSelectedValue(string selectedValue)
        {
            //月度模式
            _selectedValue = selectedValue;
            if (string.IsNullOrEmpty(selectedValue) || selectedValue.Length < 4)
            {
                SelectedYear = DateTime.Now.Year;
            }
            else
            {
                SelectedYear = int.TryParse(selectedValue.Substring(0, 4), out var year) ? year : DateTime.Now.Year;
            }
            Bind(SelectedIndex);
        }

        /// <summary>
        /// 改变选中值（期间模式）
        /// </summary>
        /// <param name="isNeedChangeIndex">是否需要修改页码（初始进入为true）</param>
        /// <param name="selectedId">选中Id</param>
        private void ChangePeriodModeSelectedValue(bool isNeedChangeIndex, int? selectedId)
        {
            //期间模式
            _selectedId = selectedId;
            if (isNeedChangeIndex)
            {
                var defaultYear = _dataSources.FirstOrDefault(a => a.Cells.FirstOrDefault(b => b.Id == selectedId) != null);
                SelectedIndex = defaultYear?.Index ?? 1;
            }
            Bind(SelectedIndex);
        }
    }
}