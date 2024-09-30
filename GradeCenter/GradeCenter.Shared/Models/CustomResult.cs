namespace GradeCenter.Shared.Models
{
    public class CustomResult<T>
    {
        public bool Succeeded { get; set; }
        public string? Message { get; set; }
        public T? ReturnValue { get; set; }
    }
}
