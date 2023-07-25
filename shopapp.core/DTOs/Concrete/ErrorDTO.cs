namespace shopapp.core.DTOs.Concrete
{
    public class ErrorDTO
    {
        public List<string> Errors { get; private set; } = new List<string>();
        public bool IsShow { get; set; }

        public ErrorDTO(string error, bool isShow)
        {
            Errors.Add(error);
            IsShow = isShow;
        }

        public ErrorDTO(List<string> errors, bool isShow)
        {
            Errors = errors;
            IsShow = isShow;
        }
    }
}
