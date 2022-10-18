namespace Integrations.Diadoc.Data.Apt.Entities
{
    public class ScalarString
    {
        public string? Value { get; set; }

        public override string ToString()
        {
            return Value!.ToString();
        }
    }
}
