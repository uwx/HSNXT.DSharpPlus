// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

namespace HSNXT.Linq.Charting
{
    public partial class PieOr_<S>
    {
        public new abstract partial class DataPoint : Series<S>.DataPoint
        {
            protected DataPoint(object value) : base(value) { }
        }
    }

}
