namespace BackendApi.Contract.Trainer
{
    public class GetTrainerResponse
    {
        public int TrainerId { get; set; }

        public int UserId { get; set; }

        public string FirstName { get; set; } = null!;

        public string MiddleName { get; set; } = null!;

        public string? LastName { get; set; }

        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public DateTime? CreateAt { get; set; }

        public string Password { get; set; } = null!;
    }
}