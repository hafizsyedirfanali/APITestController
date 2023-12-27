using Newtonsoft.Json;

namespace ClientSide.Models
{
    public class ResponseModel<T>
    {
        public T Value { get; set; }
    }
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
