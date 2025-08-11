namespace JackInTheBoxTakeHome.Model
{
    public class Hierarchy
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<Franchise> Franchises { get; set; } = new List<Franchise>();
    }
}
