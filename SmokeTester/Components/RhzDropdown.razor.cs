using Microsoft.AspNetCore.Components;

namespace SmokeTester.Components;
public partial class RhzDropdown<TItem>
{
    private string Label = string.Empty;

    [Parameter]
    public TItem? SelectedItem { get; set; }

    [Parameter]
    public Func<TItem, string> GetValue { get; set; }

    /// <summary>
    /// Data to show within dropdown
    /// </summary>
    [Parameter]
    public IEnumerable<TItem> DataSource { get; set; }

    [Parameter]
    public EventCallback<TItem> OnElementClick { get; set; }

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

    

    private async Task OnElementSelection(TItem selected)
    {
        await OnElementClick.InvokeAsync(selected);
        SelectedItem = selected; 
        Label = GetValue(SelectedItem);
    }

}
