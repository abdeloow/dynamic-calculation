namespace CosmenticFormulaApp.Web.Services
{
    public interface IUIStateService
    {
        event Action? OnStateChanged;
        void SetLoading(bool isLoading);
        bool IsLoading { get; }
        void NotifyStateChanged();
    }
}
