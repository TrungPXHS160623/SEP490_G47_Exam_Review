namespace WebClient.Common
{
    public class SpinnerService
    {
        public event Action OnShow;
        public event Action OnHide;

        public void Show()
        {
            this.OnShow?.Invoke();
        }

        public void Hide()
        {
            this.OnHide?.Invoke();
        }
    }
}
