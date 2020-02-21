using Riganti.Utils.Infrastructure.Core;

namespace DataProvider.Entities
{
    public class Light : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Intensity { get; set; }
        public string Color { get; set; }
        public bool TurnOn { get; set; }

        public User User { get; set; }
    }
}
