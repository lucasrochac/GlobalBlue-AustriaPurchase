using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalBlue_AustriaPurchases.DomainModel.Domain
{
    public class ResultDTO
    {
        public decimal? Net { get; set; }
        public decimal? Vat { get; set; }
        public decimal? Gross { get; set; }
    }
}
