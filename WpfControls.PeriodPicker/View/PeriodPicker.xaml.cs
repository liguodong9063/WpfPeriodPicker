using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using WpfControls.PeriodPicker.Dto;
using WpfControls.PeriodPicker.Infrastructure.Enums;

namespace WpfControls.PeriodPicker.View
{
    public partial class PeriodPicker
    {
        /// <summary>
        /// 选中值（例如：201804）【月度模式使用】
        /// </summary>
        public static readonly DependencyProperty SelectedValueProperty = DependencyProperty.Register("SelectedValue", typeof(string), typeof(PeriodPicker), new PropertyMetadata(string.Empty,OnSelectedValueChangedCallBack));
        /// <summary>
        /// 选中的期间ID（例如：20）【期间模式使用】
        /// </summary>
        public static readonly DependencyProperty SelectedIdProperty = DependencyProperty.Register("SelectedId", typeof(int?), typeof(PeriodPicker), new PropertyMetadata(null, OnSelectedIdChangedCallBack));
        /// <summary>
        /// 期间数据源（仅期间模式使用）
        /// </summary>
        public static readonly DependencyProperty PeriodsProperty = DependencyProperty.Register("Periods", typeof(List<CustomPeriodPickerDto>), typeof(PeriodPicker), new PropertyMetadata(new List<CustomPeriodPickerDto>(), OnPeriodsChangedCallBack));

        /// <summary>
        /// 日期选择Popup视图
        /// </summary>
        private PeriodPickerPopupView _periodPopupView;

        /// <summary>
        /// 是否手工输入改变中
        /// </summary>
        private bool _isTextInputing;
        /// <summary>
        /// 是否弹窗选择改变中
        /// </summary>
        private bool _isSelectionChanging;
        /// <summary>
        /// 上一选择ID（用于判断是否需要触发SelectedValueChangedAction）
        /// </summary>
        private int? _preSelectedId;
        /// <summary>
        /// 上一选择Value（用于判断是否需要触发SelectedValueChangedAction）
        /// </summary>
        private string _preSelectedValue;
        /// <summary>
        /// 期间数据源是否发生变化
        /// </summary>
        private bool _isPeriodsChanged = false;

        public PeriodPicker()
        {
            InitializeComponent();
        }

        public string SelectedValue
        {
            get => (string)GetValue(SelectedValueProperty);
            set
            {
                _preSelectedValue = SelectedValue;
                SetValue(SelectedValueProperty, value);
            }
        }

        public int? SelectedId
        {
            get => (int?)GetValue(SelectedIdProperty);
            set
            {
                _preSelectedId = SelectedId;
                SetValue(SelectedIdProperty, value);
            }
        }

        public List<CustomPeriodPickerDto> Periods
        {
            private get => (List<CustomPeriodPickerDto>)GetValue(PeriodsProperty);
            set => SetValue(PeriodsProperty, value);
        }

        /// <summary>
        /// 日期控件模式（0：自然月模式；1：期间模式；）
        /// </summary>
        public PeriodPickerMode Mode { private get; set; }

        public Action<int?,string> SelectedValueChangedAction;

        /// <summary>
        /// 期间模式ID改变事件
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnSelectedIdChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is PeriodPicker source)) return;
            source.ResetDisplayText(source, false);
        }

        private static void OnPeriodsChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is PeriodPicker source)) return;
            source.ResetDisplayText(source, true);
        }

        /// <summary>
        /// 重置控件显示值
        /// </summary>
        /// <param name="picker"></param>
        /// <param name="isPeriodChanged"></param>
        private void ResetDisplayText(PeriodPicker picker,bool isPeriodChanged)
        {
            if (picker._isTextInputing) return;
            if (isPeriodChanged)
            {
                picker._isPeriodsChanged = true;
            }
            foreach (var period in picker.Periods)
            {
                var selectedCell = period.Cells.FirstOrDefault(a => a.Id == picker.SelectedId);
                if (selectedCell == null) continue;
                picker.DisplayTextBox.Text = selectedCell.Value;
                return;
            }
            picker.DisplayTextBox.Text = string.Empty;
        }

        /// <summary>
        /// 自然月模式值改变回调
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnSelectedValueChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is PeriodPicker source)) return;
            if (source._isTextInputing) return;
            source.DisplayTextBox.Text = source.SelectedValue;
        }

        /// <summary>
        /// 日历图标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenPopupIconButton_Click(object sender, RoutedEventArgs e)
        {    
            if (PeriodPickerPopup.IsOpen)
            {
                PeriodPickerPopup.IsOpen = false;
            }
            if (_periodPopupView == null)
            {
                _periodPopupView = Mode == PeriodPickerMode.Month ? new PeriodPickerPopupView(DisplayTextBox.Text) : new PeriodPickerPopupView(Periods, SelectedId);
                _periodPopupView.SelectedValueChangedAction += (selectedId,selectedValue) =>
                {
                    //备份旧值并赋予新值
                    _isSelectionChanging = true;
                    SetIdValue(selectedId, selectedValue);
                    _isSelectionChanging = false;
                    PeriodPickerPopup.IsOpen = false;

                    TriggerSelectionChangedAction();
                };
            }
            else
            {
                if (Mode == PeriodPickerMode.Month)
                {
                    _periodPopupView.SelectedValue = SelectedValue;
                }
                else
                {
                    if (_isPeriodsChanged)
                    {
                        _periodPopupView = new PeriodPickerPopupView(Periods, SelectedId);
                        _periodPopupView.SelectedValueChangedAction += (selectedId, selectedValue) =>
                        {
                            //备份旧值并赋予新值
                            _isSelectionChanging = true;
                            SetIdValue(selectedId, selectedValue);
                            _isSelectionChanging = false;
                            PeriodPickerPopup.IsOpen = false;

                            TriggerSelectionChangedAction();
                        };
                    }
                    else
                    {
                        _periodPopupView.SelectedId = SelectedId;
                    }
                }
            }
            PeriodPickerPopup.Child = _periodPopupView;
            PeriodPickerPopup.IsOpen = true;
        }

        /// <summary>
        /// 文本框内容改变事件（SelectedValue、SelectedId赋值）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisplayTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //通过日历图片在弹出框选择时返回
            if (_isSelectionChanging) return;


            var textBox = sender as TextBox;
            if (textBox == null) return;
            var regex = new Regex("[^0-9]");
            textBox.Text = regex.Replace(textBox.Text, string.Empty);
            textBox.SelectionStart = textBox.Text.Length;
            
            _isTextInputing = true;
            //设置新值
            if (Mode == PeriodPickerMode.Month)
            {
                SelectedValue = DisplayTextBox.Text;
            }
            else
            {
                int? selectedId = null;
                var selectedValue = DisplayTextBox.Text;
                foreach (var period in Periods)
                {
                    var selectedCell = period.Cells.FirstOrDefault(a => a.Value == DisplayTextBox.Text);
                    if (selectedCell == null) continue;
                    selectedId = selectedCell.Id;
                    break;
                }
                SetIdValue(selectedId, selectedValue);
            }
            _isTextInputing = false;
            TriggerSelectionChangedAction();
        }

        /// <summary>
        /// 设置控件ID及Value
        /// </summary>
        /// <param name="selectedId"></param>
        /// <param name="selectedValue"></param>
        private void SetIdValue(int? selectedId, string selectedValue)
        {
            _preSelectedId = SelectedId;
            _preSelectedValue = SelectedValue;
            SelectedId = selectedId;
            SelectedValue = selectedValue;
        }

        /// <summary>
        /// 触发选中项改变Action
        /// </summary>
        private void TriggerSelectionChangedAction()
        {
            if (Mode == PeriodPickerMode.Month)
            {
                if (_preSelectedValue == SelectedValue) return;
            }
            else
            {
                if (_preSelectedId == SelectedId) return;
            }
            SelectedValueChangedAction?.Invoke(SelectedId, SelectedValue);
        }
    }
}