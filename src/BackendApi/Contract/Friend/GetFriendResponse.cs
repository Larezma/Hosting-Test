namespace BackendApi.Contract.Friend
{
    public class GetFriendResponse
    {
        public int FriendId { get; set; }

        public int UserId1 { get; set; }

        public int UserId2 { get; set; }

        public DateTime? StartDate { get; set; }
    }
}