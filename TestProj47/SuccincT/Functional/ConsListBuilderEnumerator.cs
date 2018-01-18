﻿using System;
using System.Collections;
using System.Collections.Generic;
using static HSNXT.SuccincT.Functional.ConsNodeState;

namespace HSNXT.SuccincT.Functional
{
    internal sealed class ConsListBuilderEnumerator<T> : IEnumerator<ConsNode<T>>
    {
        private readonly IEnumerator<T> _enumerator;

        public bool MoveNext()
        {
            if (!_enumerator.MoveNext()) return false;

            Current = new ConsNode<T>
            {
                Value = _enumerator.Current,
                State = HasValue
            };

            return true;
        }

        public void Reset() => throw new NotSupportedException();

        public ConsNode<T> Current { get; private set; }

        public void Dispose() => _enumerator.Dispose();
    }
}