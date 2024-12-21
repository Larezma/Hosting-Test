using System.ComponentModel.DataAnnotations.Schema;

namespace BackendApi.Contract.Users
{
    public class GetUserResponse
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public int? RoleUser { get; set; }

        public string UserImg { get; set; } = null!;

        public DateTime? CreateAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public string AboutMe { get; set; } = null!;
    }
}