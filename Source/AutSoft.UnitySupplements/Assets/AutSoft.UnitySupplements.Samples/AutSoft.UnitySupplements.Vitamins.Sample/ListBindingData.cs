#nullable enable
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutSoft.UnitySupplements.Vitamins.Sample
{
    public class ListBindingData : ObservableObject
    {
        private ListItemData[] _items = Array.Empty<ListItemData>();

        public IReadOnlyList<ListItemData> Items
        {
            get => _items;
            private set => SetProperty(ref _items, value.ToArray());
        }

        private ListItemData? _selected = new();

        public ListItemData? Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        public void Add(ListItemData item) => Items = Items.Append(item).ToArray();

        public void RemoveSelected()
        {
            if (Selected is null) return;
            Items = Items.Except(new[] { Selected }).ToArray();
            Selected = null;
        }

        public void Order() => Items = Items.OrderBy(i => i.Number).ToArray();
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
