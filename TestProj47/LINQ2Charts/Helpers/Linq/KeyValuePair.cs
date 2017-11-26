// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace HSNXT
{
    public static class KVPair
    {
        public static KeyValuePair<TK, TV> Create<TK, TV>(TK key, TV value)
        {
            return new KeyValuePair<TK, TV>(key, value);
        }
    }
}