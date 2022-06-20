using System.Collections.Generic;
using ApplicationA.Models;

namespace ApplicationA.DL
{
    public interface IAutopartRepository
    {
        void Add(Autopart autopart);

        IReadOnlyCollection<Autopart> Autoparts { get; }
    }
}
