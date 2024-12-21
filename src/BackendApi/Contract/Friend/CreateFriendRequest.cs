namespace BackendApi.Contract.Friend
{
    public class CreateFriendRequest
    {
        public int UserId1 { get; set; }

        public int UserId2 { get; set; }

        public DateTime? StartDate { get; set; }
    }
}