namespace BackendApi.Contract.GroupMembers
{
    public class CreateGroupMembersRequest
    {
        public int UserId { get; set; }

        public DateTime? JoinDate { get; set; }
    }
}