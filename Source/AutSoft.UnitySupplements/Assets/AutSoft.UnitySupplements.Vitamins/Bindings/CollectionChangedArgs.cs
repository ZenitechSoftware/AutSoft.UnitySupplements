#nullable enable
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace AutSoft.UnitySupplements.Vitamins.Bindings
{
    public class CollectionChangedArgs<T>
    {
        public CollectionChangedArgs(NotifyCollectionChangedEventArgs originalData)
        {
            Action = originalData.Action;
            NewItems = originalData.NewItems?.Cast<T>().ToList();
            NewStartingIndex = originalData.NewStartingIndex;
            OldItems = originalData.NewItems?.Cast<T>().ToList();
            OldStartingIndex = originalData.OldStartingIndex;
            OriginalData = originalData;
        }

        public NotifyCollectionChangedAction Action { get; }
        public IReadOnlyList<T>? NewItems { get; }
        public int NewStartingIndex { get; }
        public IReadOnlyList<T>? OldItems { get; }
        public int OldStartingIndex { get; }

        public NotifyCollectionChangedEventArgs OriginalData { get; }
    }
}
