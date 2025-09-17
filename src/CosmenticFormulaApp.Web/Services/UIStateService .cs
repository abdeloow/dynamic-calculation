namespace CosmenticFormulaApp.Web.Services
{
    public class UIStateService : IUIStateService
    {
        public event Action? OnStateChanged;
        public bool IsLoading { get; private set; }
        public void SetLoading(bool isLoading)
        {
            IsLoading = isLoading;
            NotifyStateChanged();
        }
        public void NotifyStateChanged()
        {
            OnStateChanged?.Invoke();
        }
    }
}
