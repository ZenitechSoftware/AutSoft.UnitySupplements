#nullable enable
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;

namespace AutSoft.UnitySupplements.Samples.VitaminSamples.BindingSamples
{
    public class ListBindingData : ObservableObject
    {
        private readonly ObservableCollection<ListItemData> _items = new();

        public ReadOnlyObservableCollection<ListItemData> Items { get; }

        private ListItemData? _selected;

        public ListItemData? Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        public ListBindingData() => Items = new(_items);

        public void Add(ListItemData item) => _items.Add(item);

        public void ReplaceFirst(ListItemData item)
        {
            if (_items.Count == 0)
            {
                _items.Add(item);
            }
            else
            {
                _items[0] = item;
            }
        }

        public void RemoveSelected()
        {
            if (Selected is null) return;
            _items.Remove(Selected);
            Selected = null;
        }

        public void Order()
        {
            var orderedItems = Items.OrderBy(i => i.Number).ToArray();

            _items.Clear();

            foreach (var item in orderedItems)
            {
                _items.Add(item);
            }
        }

        public void SwapFirstAndLast()
        {
            if (_items.Count <= 2) return;

            _items.Move(_items.Count - 1, 0);
        }
    }

    public class ListItemData : ObservableObject
    {
        private string? _title;

        public string? Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private int? _number;

        public int? Number
        {
            get => _number;
            set => SetProperty(ref _number, value);
        }
    }
}
