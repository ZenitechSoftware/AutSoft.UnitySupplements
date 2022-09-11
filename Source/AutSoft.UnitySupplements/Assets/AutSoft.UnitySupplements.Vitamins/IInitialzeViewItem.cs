#nullable enable

namespace AutSoft.UnitySupplements.Vitamins
{
    public interface IInitialzeViewItem<in T>
    {
        public void Initialize(T? item);
    }
}
