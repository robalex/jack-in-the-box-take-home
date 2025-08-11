namespace JackInTheBoxTakeHome.Model
{
    public class Region
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<Store> Stores { get; set; } = new List<Store>();
    }
}
