namespace JackInTheBoxTakeHome.Model
{
    public class Franchise
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<Region> Regions { get; set; } = new List<Region>();
    }
}
