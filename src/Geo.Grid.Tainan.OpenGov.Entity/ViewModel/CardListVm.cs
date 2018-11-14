using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geo.Grid.Tainan.OpenGov.Entity.Enumeration;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    public class CardListVm
    {
        public CardColor Color { get; set; }
        public List<CardVm> Cards { get; set; }
        //public IQueryable<CardVm> Red { get; set; }
        //public IQueryable<CardVm> Yellow { get; set; }
    }
}
