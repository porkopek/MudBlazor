using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MudBlazor
{
    public class ScrollEventArgs:EventArgs
    {
        public int Y { get; set; }

        public int X { get; set; }

    }
}
