namespace Witcher.Domain
{
    public class EnvironmentCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Environment>? Environments { get; set; }
    }
}
