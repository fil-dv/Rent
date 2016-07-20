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

        public void AddItem(Area area)
        {
            CartLine line = _lineColliction.Where(a => a.Area.AreaID == area.AreaID).FirstOrDefault();

            if (line == null)
            {
                _lineColliction.Add(new CartLine { Area = area });
            }            
        }

        public void RemoveLine(Area area)
        {
            _lineColliction.RemoveAll(a => a.Area.AreaID == area.AreaID);
        }

        public decimal CalculateTotalValue()
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
