﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

namespace HSNXT.Linq.Charting
{
    public partial class Doughnut 
    {
        public new partial class DataPoint : PieOr_<DataPoint>.DataPoint
        {
            public DataPoint(object value) : base(value) { }
        }
    }
}