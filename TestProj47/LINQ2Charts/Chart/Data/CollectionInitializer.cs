// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Collections;
using System.Collections.Generic;

namespace HSNXT.Linq.Charting
{
    partial class Chart
    {
        partial class Data : IEnumerable<System.Windows.Forms.DataVisualization.Charting.ChartArea>
        {

            public IEnumerator<System.Windows.Forms.DataVisualization.Charting.ChartArea> GetEnumerator()
            {
                return _chart.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}
