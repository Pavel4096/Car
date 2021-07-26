namespace Car
{
    public interface IAnalytics
    {
        void GameStartTime();
        void MenuEntered(string menuName);
        void Send(string name, params (string name, object value)[] eventParameters);
    }
}
