﻿using System;
using System.Collections.Generic;
using System.Windows;
using WpfControls.PeriodPicker.Dto;

namespace WpfPeriodPicker
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<CustomPeriodPickerDto> DataSources { get; set; }
        public int? SelectedId { get; set; }
        public string SelectedValue { get; set; }

        public bool IsEnable { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            //SelectedValue = "201806";
            SelectedId = 10;
            DataContext = this;

            IsEnable = false;

            PeriodPicker.SelectedValueChangedAction = (selectedId, selectedValue) =>
            {
                Console.WriteLine($@"{selectedId},{selectedValue}");
            };

            var dataSources = new List<CustomPeriodPickerDto>();
            var id = 1;
            for (var year = 2012; year <= 2025; year++)
            {
                var cells = new List<CustomPeriodPickerCellDto>();
                for (var i = 1; i <= 20; i++)
                {
                    cells.Add(new CustomPeriodPickerCellDto
                    {

                        DisplayName = $"{i:D2}期",
                        Value = $"{year}{i:D2}",
                        Id=id
                    });
                    id++;
                }
                dataSources.Add(new CustomPeriodPickerDto
                {
                    DisplayName = year+"年度",
                    Cells = cells
                });
            }
            DataSources = dataSources;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"{SelectedId},{SelectedValue}");
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var dataSources = new List<CustomPeriodPickerDto>();
            var id = 100;
            for (var year = 2000; year <= 2005; year++)
            {
                var cells = new List<CustomPeriodPickerCellDto>();
                for (var i = 1; i <= 20; i++)
                {
                    cells.Add(new CustomPeriodPickerCellDto
                    {

                        DisplayName = $"{i:D2}期",
                        Value = $"{year}{i:D2}",
                        Id = id
                    });
                    id++;
                }
                dataSources.Add(new CustomPeriodPickerDto
                {
                    DisplayName = year + "年度",
                    Cells = cells
                });
            }
            PeriodPicker.Periods = dataSources;

            //PeriodPicker.SelectedId = null;
        }
    }
}
