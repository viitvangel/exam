using ApplicationA.BL.Interfaces;
using ApplicationA.DL;
using ApplicationA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationA.BL.Services
{
    public class AutopartService : IAutopartService
    {
        private readonly IAutopartRepository autopartRepository;

        public AutopartService(IAutopartRepository autopartRepository)
        {
            this.autopartRepository = autopartRepository;
        }

        public void Create(Autopart autopart)
        {
            autopartRepository.Add(autopart);
        }

        public IEnumerable<Autopart> GetAll()
        {
            return autopartRepository.Autoparts;
        }
    }
}
