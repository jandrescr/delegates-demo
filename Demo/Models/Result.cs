namespace Demo.Models
{
    public class Result
    {
        public string Name { get; set; }
        public int Length { get; set; }

        public override string ToString() => $"Name: {Name} and length: {Length}";
    }
}
