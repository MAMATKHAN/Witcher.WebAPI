namespace Witcher.Domain
{
    public class EquipmentCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Equipment>? Equipments { get; set; }
    }
}
