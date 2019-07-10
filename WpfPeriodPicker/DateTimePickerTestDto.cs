using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfControls.DateTimePicker.Dto;
using WpfControls.DateTimePicker.Infrastructure;

namespace WpfDateTimePicker
{
    public class DateTimePickerTestDto:ModelBase
    {
        private List<CustomDateTimePickerDto> _dataSources;
        private int? _selectedId;
        private string _selectedValue;
        private bool _isEnable;

        public List<CustomDateTimePickerDto> DataSources
        {
            get => _dataSources;
            set
            {
                if (_dataSources == value) return;
                _dataSources = value;
                RaisePropertyChanged(nameof(DataSources));
            }
        }
        public int? SelectedId
        {
            get => _selectedId;
            set
            {
                if (_selectedId == value) return;
                _selectedId = value;
                RaisePropertyChanged(nameof(SelectedId));
            }
        }
        public string SelectedValue
        {
            get => _selectedValue;
            set
            {
                if (_selectedValue == value) return;
                _selectedValue = value;
                RaisePropertyChanged(nameof(SelectedValue));
            }
        }
        public bool IsEnable
        {
            get => _isEnable;
            set
            {
                if (_isEnable == value) return;
                _isEnable = value;
                RaisePropertyChanged(nameof(IsEnable));
            }
        }
    }
}
