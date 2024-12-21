namespace BackendApi.Contract.Dialogs
{
    public class CreateDialogsRequest
    {
        public string TextDialogs { get; set; } = null!;

        public DateTime? TimeCreate { get; set; }

        public DateTime? EndTime { get; set; }
    }
}