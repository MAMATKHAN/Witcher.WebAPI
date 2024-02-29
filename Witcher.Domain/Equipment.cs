namespace Witcher.Domain
{
    public class Equipment
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Text { get; set; }
        public string? ImageName { get; set; }
        public string? ImageSource { get; set; }
        public int? Damage { get; set; }
        public int? Armor { get; set; }
        public string? Effect { get; set; }
        public Guid? TypeId { get; set; }
        public EquipmentType? Type { get; set; }
        public Guid? CategoryId { get; set; }
        public EquipmentCategory? Category { get; set; }
        //public List<Build>? Builds { get; set; }
    }
}
