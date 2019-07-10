using System.ComponentModel;

namespace WpfControls.PeriodPicker.Infrastructure.Enums
{
    /// <summary>
    /// 期间选择模式
    /// </summary>
    public enum PeriodPickerMode
    {
        [Description("自然月份")]
        Month,
        [Description("会计期间")]
        Period
    }
}
