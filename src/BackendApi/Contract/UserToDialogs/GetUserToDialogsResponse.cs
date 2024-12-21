namespace BackendApi.Contract.UserToDialogs
{
    public class GetUserToDialogsResponse
    {
        public int Id { get; set; }

        public int DialogId { get; set; }

        public int UserId { get; set; }

        public DateTime? TimeCreate { get; set; }
    }
}