using ApplicationA.Models;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationA.DL
{
    public class AutopartRepository : IAutopartRepository
    {
        private readonly ICollection<Autopart> autoparts;

        public AutopartRepository()
        {
            autoparts = new List<Autopart>();
        }
        public void Add(Autopart autopart)
        {
            autoparts.Add(autopart);
        }

        public IReadOnlyCollection<Autopart> Autoparts => autoparts.ToList().AsReadOnly();
    }
}
