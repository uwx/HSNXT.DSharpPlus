// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        /// Sets the X-value of the data point.
        /// </summary>
        public static DataPoint SetValueX(this DataPoint point, object xValue)
        {
            point.SetValueXY(xValue, point.YValues.Cast<object>().ToArray());
            return point;
        }
    }
}