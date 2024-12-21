namespace BackendApi.Contract.Roles
{
    public class GetRolesResponse
    {
        public int Id { get; set; }

        public string Roles { get; set; } = null!;
    }
}