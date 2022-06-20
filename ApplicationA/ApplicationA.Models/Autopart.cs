using MessagePack;

namespace ApplicationA.Models
{
    [MessagePackObject]
    public class Autopart
    {
        [Key(0)]
        public int Id { get; set; }

        [Key(1)]
        public string AutopartName { get; set; }

        [Key(2)]
        public string CategoryName { get; set; }

        [Key(3)]
        public string Model { get; set; }

        [Key(4)]
        public string CarBrand { get; set; }
    }
}