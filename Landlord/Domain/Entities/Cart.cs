using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cart
    {
        List<CartLine> _lineColliction = new List<CartLine>();

        public IEnumerable<CartLine> Lines { get { return _lineColliction; } } 

        public bool AddItem(Area area)
        {
            CartLine line = _lineColliction.Where(a => a.Area.AreaID == area.AreaID).FirstOrDefault();

            if (line == null)
            {
                _lineColliction.Add(new CartLine { Area = area });
                return true;
            }
            else return false;
        }

        public void RemoveLine(Area area)
        {
            _lineColliction.RemoveAll(a => a.Area.AreaID == area.AreaID);
        }

        public decimal CalculateTotalSum()
        {
            return _lineColliction.Sum(a => a.Area.MonthPrice);
        }

        public void Clear()
        {
            _lineColliction.Clear();
        }
    }

    public class CartLine
    {
        public Area Area { get; set; }        
    }
}
