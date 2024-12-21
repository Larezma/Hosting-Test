namespace BackendApi.Contract.Users
{
    public class CreateUserRequest
    {
        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public int RoleUser { get; set; }

        public string UserImg { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public string AboutMe { get; set; }
    }
}