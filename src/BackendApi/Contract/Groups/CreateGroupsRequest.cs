namespace BackendApi.Contract.Groups
{
    public class CreateGroupsRequest
    {
        public int OwnerGroups { get; set; }

        public string GroupsName { get; set; } = null!;

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateGroups { get; set; }
    }
}