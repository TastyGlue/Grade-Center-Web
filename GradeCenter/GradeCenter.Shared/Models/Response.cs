namespace GradeCenter.Shared.Models
{
    public class Response<T>
    {
        public bool Succeeded { get; set; }
        public string? Message { get; set; }
        public T? ReturnValue { get; set; }
    }
}
