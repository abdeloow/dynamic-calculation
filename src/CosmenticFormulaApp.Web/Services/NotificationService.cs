namespace CosmenticFormulaApp.Web.Services;

public interface INotificationService
{
    void ShowSuccess(string message);
    void ShowError(string message);
    void ShowInfo(string message);
    void ShowWarning(string message);
    event Action<ToastMessage>? OnToastAdded;
}

public class NotificationService : INotificationService
{
    public event Action<ToastMessage>? OnToastAdded;

    public void ShowSuccess(string message)
    {
        OnToastAdded?.Invoke(new ToastMessage { Type = ToastType.Success, Message = message });
    }

    public void ShowError(string message)
    {
        OnToastAdded?.Invoke(new ToastMessage { Type = ToastType.Error, Message = message });
    }

    public void ShowInfo(string message)
    {
        OnToastAdded?.Invoke(new ToastMessage { Type = ToastType.Info, Message = message });
    }

    public void ShowWarning(string message)
    {
        OnToastAdded?.Invoke(new ToastMessage { Type = ToastType.Warning, Message = message });
    }
}

public class ToastMessage
{
    public ToastType Type { get; set; }
    public string Message { get; set; } = "";
    public DateTime Timestamp { get; set; } = DateTime.Now;
    public string Id { get; set; } = Guid.NewGuid().ToString();
}

public enum ToastType
{
    Success,
    Error,
    Info,
    Warning
}