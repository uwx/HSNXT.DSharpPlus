﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HSNXT.Portable
{
    public class ObservableCollection<T> : Collection<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        private readonly SimpleMonitor _monitor = new SimpleMonitor();
        private const string CountString = "Count";
        private const string IndexerName = "Item[]";

        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public ObservableCollection()
        {
        }

        public ObservableCollection(List<T> list)
            : base(list != null ? new List<T>(list.Count) : list)
        {
            CopyFrom(list);
        }

        public ObservableCollection(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            CopyFrom(collection);
        }

        private void CopyFrom(IEnumerable<T> collection)
        {
            var items = Items;
            if (collection == null)
                return;
            foreach (var obj in collection)
                items.Add(obj);
        }

        public void Move(int oldIndex, int newIndex)
        {
            MoveItem(oldIndex, newIndex);
        }

        protected override void ClearItems()
        {
            base.ClearItems();
            OnPropertyChanged(CountString);
            OnPropertyChanged(IndexerName);
            OnCollectionReset();
        }

        protected override void RemoveItem(int index)
        {
            var obj = this[index];
            base.RemoveItem(index);
            OnPropertyChanged(CountString);
            OnPropertyChanged(IndexerName);
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, obj, index);
        }

        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            OnPropertyChanged(CountString);
            OnPropertyChanged(IndexerName);
            OnCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
        }

        protected override void SetItem(int index, T item)
        {
            var obj = this[index];
            base.SetItem(index, item);
            OnPropertyChanged(IndexerName);
            OnCollectionChanged(NotifyCollectionChangedAction.Replace, obj, item, index);
        }

        protected virtual void MoveItem(int oldIndex, int newIndex)
        {
            var obj = this[oldIndex];
            base.RemoveItem(oldIndex);
            base.InsertItem(newIndex, obj);
            OnPropertyChanged(IndexerName);
            OnCollectionChanged(NotifyCollectionChangedAction.Move, obj, newIndex, oldIndex);
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var propertyChanged = PropertyChanged;
            if (propertyChanged != null)
                propertyChanged(this, e);
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            var collectionChanged = CollectionChanged;
            if (collectionChanged != null)
            {
                using (BlockReentrancy())
                    collectionChanged(this, e);
            }
        }

        protected IDisposable BlockReentrancy()
        {
            _monitor.Enter();
            return _monitor;
        }

        private void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index, int oldIndex)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index, oldIndex));
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object oldItem, object newItem,
            int index)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, newItem, oldItem, index));
        }

        private void OnCollectionReset()
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        [Serializable]
        private class SimpleMonitor : IDisposable
        {
            private int _busyCount;

            public bool Busy
            {
                get { return _busyCount > 0; }
            }

            public void Enter()
            {
                ++_busyCount;
            }

            public void Dispose()
            {
                --_busyCount;
            }
        }
    }

    public interface INotifyPropertyChanged
    {
        event PropertyChangedEventHandler PropertyChanged;
    }

    public delegate void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e);

    public class PropertyChangedEventArgs : EventArgs
    {
        private readonly string _propertyName;

        public PropertyChangedEventArgs(string propertyName)
        {
            _propertyName = propertyName;
        }

        public virtual string PropertyName
        {
            get { return _propertyName; }
        }
    }

    public interface INotifyCollectionChanged
    {
        event NotifyCollectionChangedEventHandler CollectionChanged;
    }

    public delegate void NotifyCollectionChangedEventHandler(object sender, NotifyCollectionChangedEventArgs e);

    public class NotifyCollectionChangedEventArgs : EventArgs
    {
        private int _newStartingIndex = -1;
        private int _oldStartingIndex = -1;
        private NotifyCollectionChangedAction _action;
        private IList _newItems;
        private IList _oldItems;

        public NotifyCollectionChangedAction Action
        {
            get { return _action; }
        }

        public IList NewItems
        {
            get { return _newItems; }
        }

        public IList OldItems
        {
            get { return _oldItems; }
        }

        public int NewStartingIndex
        {
            get { return _newStartingIndex; }
        }

        public int OldStartingIndex
        {
            get { return _oldStartingIndex; }
        }

        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action)
        {
            if (action != NotifyCollectionChangedAction.Reset)
                throw new ArgumentException(
                    $"Constructor supports only the '{NotifyCollectionChangedAction.Reset}' action.", nameof(action));
            InitializeAdd(action, null, -1);
        }

        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object changedItem)
        {
            if (action != NotifyCollectionChangedAction.Add && action != NotifyCollectionChangedAction.Remove &&
                action != NotifyCollectionChangedAction.Reset)
                throw new ArgumentException("Constructor only supports either a Reset, Add, or Remove action.",
                    nameof(action));
            if (action == NotifyCollectionChangedAction.Reset)
            {
                if (changedItem != null)
                    throw new ArgumentException("Reset action must be initialized with no changed items.",
                        nameof(action));
                InitializeAdd(action, null, -1);
            }
            else
                InitializeAddOrRemove(action, new[] {changedItem}, -1);
        }

        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object changedItem, int index)
        {
            if (action != NotifyCollectionChangedAction.Add && action != NotifyCollectionChangedAction.Remove &&
                action != NotifyCollectionChangedAction.Reset)
                throw new ArgumentException("Constructor only supports either a Reset, Add, or Remove action.<",
                    nameof(action));
            if (action == NotifyCollectionChangedAction.Reset)
            {
                if (changedItem != null)
                    throw new ArgumentException("Reset action must be initialized with no changed items.",
                        nameof(action));
                if (index != -1)
                    throw new ArgumentException("Reset action must be initialized with index -1.", nameof(action));
                InitializeAdd(action, null, -1);
            }
            else
                InitializeAddOrRemove(action, new[] {changedItem}, index);
        }

        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList changedItems)
        {
            if (action != NotifyCollectionChangedAction.Add && action != NotifyCollectionChangedAction.Remove &&
                action != NotifyCollectionChangedAction.Reset)
                throw new ArgumentException("Constructor only supports either a Reset, Add, or Remove action.",
                    nameof(action));
            if (action == NotifyCollectionChangedAction.Reset)
            {
                if (changedItems != null)
                    throw new ArgumentException("Reset action must be initialized with no changed items.",
                        nameof(action));
                InitializeAdd(action, null, -1);
            }
            else
            {
                if (changedItems == null)
                    throw new ArgumentNullException(nameof(changedItems));
                InitializeAddOrRemove(action, changedItems, -1);
            }
        }

        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList changedItems,
            int startingIndex)
        {
            if (action != NotifyCollectionChangedAction.Add && action != NotifyCollectionChangedAction.Remove &&
                action != NotifyCollectionChangedAction.Reset)
                throw new ArgumentException("Constructor only supports either a Reset, Add, or Remove action.",
                    nameof(action));
            if (action == NotifyCollectionChangedAction.Reset)
            {
                if (changedItems != null)
                    throw new ArgumentException("Reset action must be initialized with no changed items.",
                        nameof(action));
                if (startingIndex != -1)
                    throw new ArgumentException("Reset action must be initialized with index -1.", nameof(action));
                InitializeAdd(action, null, -1);
            }
            else
            {
                if (changedItems == null)
                    throw new ArgumentNullException(nameof(changedItems));
                if (startingIndex < -1)
                    throw new ArgumentException("Index cannot be negative.", nameof(startingIndex));
                InitializeAddOrRemove(action, changedItems, startingIndex);
            }
        }

        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object newItem, object oldItem)
        {
            if (action != NotifyCollectionChangedAction.Replace)
                throw new ArgumentException(
                    $"Constructor supports only the '{NotifyCollectionChangedAction.Replace}' action.", nameof(action));
            InitializeMoveOrReplace(action, new[] {newItem}, new[] {oldItem}, -1, -1);
        }

        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object newItem, object oldItem,
            int index)
        {
            if (action != NotifyCollectionChangedAction.Replace)
                throw new ArgumentException(
                    $"Constructor supports only the '{NotifyCollectionChangedAction.Replace}' action.", nameof(action));
            InitializeMoveOrReplace(action, new[] {newItem}, new[] {oldItem}, index, index);
        }

        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList newItems, IList oldItems)
        {
            if (action != NotifyCollectionChangedAction.Replace)
                throw new ArgumentException(
                    $"Constructor supports only the '{NotifyCollectionChangedAction.Replace}' action.", nameof(action));
            if (newItems == null)
                throw new ArgumentNullException(nameof(newItems));
            if (oldItems == null)
                throw new ArgumentNullException(nameof(oldItems));
            InitializeMoveOrReplace(action, newItems, oldItems, -1, -1);
        }

        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList newItems, IList oldItems,
            int startingIndex)
        {
            if (action != NotifyCollectionChangedAction.Replace)
                throw new ArgumentException(
                    $"Constructor supports only the '{NotifyCollectionChangedAction.Replace}' action.", nameof(action));
            if (newItems == null)
                throw new ArgumentNullException(nameof(newItems));
            if (oldItems == null)
                throw new ArgumentNullException(nameof(oldItems));
            InitializeMoveOrReplace(action, newItems, oldItems, startingIndex, startingIndex);
        }

        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object changedItem, int index,
            int oldIndex)
        {
            if (action != NotifyCollectionChangedAction.Move)
                throw new ArgumentException(
                    $"Constructor supports only the '{NotifyCollectionChangedAction.Move}' action.", nameof(action));
            if (index < 0)
                throw new ArgumentException("Index cannot be negative.", nameof(index));
            var objArray = new[] {changedItem};
            InitializeMoveOrReplace(action, objArray, objArray, index, oldIndex);
        }

        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList changedItems, int index,
            int oldIndex)
        {
            if (action != NotifyCollectionChangedAction.Move)
                throw new ArgumentException(
                    $"Constructor supports only the '{NotifyCollectionChangedAction.Move}' action.", nameof(action));

            if (index < 0)
                throw new ArgumentException("Index cannot be negative.", nameof(index));
            InitializeMoveOrReplace(action, changedItems, changedItems, index, oldIndex);
        }

        internal NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList newItems, IList oldItems,
            int newIndex,
            int oldIndex)
        {
            _action = action;
            _newItems = newItems == null ? null : ArrayList.ReadOnly(newItems);
            _oldItems = oldItems == null ? null : ArrayList.ReadOnly(oldItems);
            _newStartingIndex = newIndex;
            _oldStartingIndex = oldIndex;
        }

        private void InitializeAddOrRemove(NotifyCollectionChangedAction action, IList changedItems, int startingIndex)
        {
            if (action == NotifyCollectionChangedAction.Add)
                InitializeAdd(action, changedItems, startingIndex);
            else
            {
                if (action != NotifyCollectionChangedAction.Remove)
                    return;
                InitializeRemove(action, changedItems, startingIndex);
            }
        }

        private void InitializeAdd(NotifyCollectionChangedAction action, IList newItems, int newStartingIndex)
        {
            _action = action;
            _newItems = newItems == null ? null : ArrayList.ReadOnly(newItems);
            _newStartingIndex = newStartingIndex;
        }

        private void InitializeRemove(NotifyCollectionChangedAction action, IList oldItems, int oldStartingIndex)
        {
            _action = action;
            _oldItems = oldItems == null ? null : ArrayList.ReadOnly(oldItems);
            _oldStartingIndex = oldStartingIndex;
        }

        private void InitializeMoveOrReplace(NotifyCollectionChangedAction action, IList newItems, IList oldItems,
            int startingIndex,
            int oldStartingIndex)
        {
            InitializeAdd(action, newItems, startingIndex);
            InitializeRemove(action, oldItems, oldStartingIndex);
        }
    }

    public enum NotifyCollectionChangedAction
    {
        Add,
        Remove,
        Replace,
        Move,
        Reset,
    }
}