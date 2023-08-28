using System.Collections.Generic;
using Biblioseca.DataAccess.Partners;
using Biblioseca.Model;

namespace Biblioseca.Service
{
    public class PartnerService
    {
        private readonly PartnerDao partnerDao;

        public PartnerService(PartnerDao partnerDao)
        {
            this.partnerDao = partnerDao;
        }

        public IEnumerable<Partner> GetAll()
        {
            return partnerDao.GetAll();
        }

        public Partner Get(int partnerId)
        {
            return partnerDao.Get(partnerId);
        }
    }
}