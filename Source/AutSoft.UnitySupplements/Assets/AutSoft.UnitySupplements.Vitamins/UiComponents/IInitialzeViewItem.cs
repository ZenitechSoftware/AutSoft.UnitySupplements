#nullable enable

namespace AutSoft.UnitySupplements.Vitamins.UiComponents
{
    public interface IInitialzeViewItem<in T>
    {
        public void Initialize(T? item);
    }
}
