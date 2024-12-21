namespace BackendApi.Contract.MessageUsers
{
    public class GetMessageUsersResponse
    {
        public int MessageId { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public string MessageContent { get; set; } = null!;

        public DateTime? DateMessage { get; set; }

        public DateTime? DateUpMessage { get; set; }

    }
}