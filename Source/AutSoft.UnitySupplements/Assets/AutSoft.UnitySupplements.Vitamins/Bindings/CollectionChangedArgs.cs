#nullable enable
using System.Collections.Specialized;

namespace AutSoft.UnitySupplements.Vitamins.Bindings
{
    public class CollectionChangedArgs<T> where T : class
    {
        public CollectionChangedArgs(NotifyCollectionChangedAction action, T? newItem, int newStartingIndex, T? oldItem, int oldStartingIndex, NotifyCollectionChangedEventArgs originalData)
        {
            Action = action;
            NewItem = newItem;
            NewStartingIndex = newStartingIndex;
            OldItem = oldItem;
            OldStartingIndex = oldStartingIndex;
            OriginalData = originalData;
        }

        public NotifyCollectionChangedAction Action { get; }
        public T? NewItem { get; }
        public int NewStartingIndex { get; }
        public T? OldItem { get; }
        public int OldStartingIndex { get; }

        public NotifyCollectionChangedEventArgs OriginalData { get; }
    }
}
