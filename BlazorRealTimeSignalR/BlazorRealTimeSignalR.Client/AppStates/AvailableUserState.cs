namespace BlazorRealTimeSignalR.Client.AppStates
{
    public class AvailableUserState
    {
        public string ReceiverId { get; private set; } = string.Empty;
        public string FullName { get; private set; } = string.Empty;

        public void SetStates(string fullname, string receiverId)
        {
            FullName = fullname;
            ReceiverId = receiverId;
        } 
    }
}
