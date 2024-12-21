namespace BackendApi.Contract.GroupMembers
{
    public class GetGroupMembersResponse
    {
        public int GroupsId { get; set; }

        public int UserId { get; set; }

        public DateTime? JoinDate { get; set; }
    }
}