namespace BackendApi.Contract.UserToRule
{
    public class CreateUserToRuleRequest
    {
        public int UserId { get; set; }

        public int RoleId { get; set; }
    }
}