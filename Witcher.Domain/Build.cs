namespace Witcher.Domain
{
    public class Build
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Text { get; set; }
        public string? ImageName { get; set; }
        public string? ImageSource { get; set; }
        public DateTime CreatedDate { get; set; }
        
        //public List<Equipment>? Equipments { get; set; }
    }
}
