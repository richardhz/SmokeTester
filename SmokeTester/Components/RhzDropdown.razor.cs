using Microsoft.AspNetCore.Components;

namespace SmokeTester.Components;
public partial class RhzDropdown<TItem>
{
    [Parameter]
    public Func<TItem, (string,string)> GetValue { get; set; }

    /// <summary>
    /// Data to show within dropdown
    /// </summary>
    [Parameter]
    public IEnumerable<TItem> DataSource { get; set; }

    [Parameter]
    public EventCallback<string> OnElementClick { get; set; }

    protected string Key { get; set; }

    protected string Value { get; set; }

    //protected override void OnInitialized()
    //{
    //    LblText = "Select";
    //    //LblText = GetValue(SelectedItem) ?? GetValue(DataSource.FirstOrDefault() != null
    //    //                           ? DataSource.FirstOrDefault()
    //    //                           : default);

    //    ListGroupHeight = (DataSource.Count() < 2) ? string.Empty : ListGroupHeight;
    //}

    

    private Task OnValueChanged(ChangeEventArgs e)
    {
        Value = e.Value.ToString();
        return OnElementClick.InvokeAsync(Value);
    }

}
