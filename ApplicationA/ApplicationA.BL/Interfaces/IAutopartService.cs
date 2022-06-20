using ApplicationA.Models;
using System.Collections.Generic;

namespace ApplicationA.BL.Interfaces
{
    public interface IAutopartService
    {
        void Create(Autopart autopart);

        IEnumerable<Autopart> GetAll();
    }
}
