namespace Sara.LogReader.Service
{
    /// <summary>
    /// This is a place holder for the Notification System.
    /// For now it will send the warning to the output console
    /// </summary>
    public static class NotificationService
    {
        public static void Warning(string message)
        {
            OutputService.Log(message);
        }
    }
}
