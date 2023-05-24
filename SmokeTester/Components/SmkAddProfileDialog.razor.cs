using SmokeTester.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeTester.Components;

public partial class SmkAddProfileDialog
{
    public bool ShowDialog { get; set; }

    public void Show(EndPointProfile data)
    {
        ShowDialog = true;
    }

    public void Close()
    {
        ShowDialog = false;
    }
}
