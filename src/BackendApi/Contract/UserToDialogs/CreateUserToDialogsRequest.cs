namespace BackendApi.Contract.UserToDialogs
{
    public class CreateUserToDialogsRequest
    {
        public int DialogId { get; set; }

        public int UserId { get; set; }

        public DateTime? TimeCreate { get; set; }
    }
}