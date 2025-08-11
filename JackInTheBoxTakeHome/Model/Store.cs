namespace JackInTheBoxTakeHome.Model
{
    public class Store
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Address Address { get; set; }

        public Store(int id, string name, Address address)
        {
            Id = id;
            Name = name;
            Address = address;
        }
    }
}
