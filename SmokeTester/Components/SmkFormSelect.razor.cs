using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmokeTester.Components;

public partial class SmkFormSelect<TItem>
{
    [Parameter]
    public Func<TItem, (string, string)> GetValue { get; set; }

    /// <summary>
    /// Data to show within dropdown
    /// </summary>
    [Parameter]
    public IEnumerable<TItem> DataSource { get; set; }

    [Parameter]
    public EventCallback<string> OnElementClick { get; set; }

    protected string Key { get; set; }

    protected string Value { get; set; }

    



    private Task OnValueChanged(ChangeEventArgs e)
    {
        Value = e.Value.ToString();
        return OnElementClick.InvokeAsync(Value);
    }

}
