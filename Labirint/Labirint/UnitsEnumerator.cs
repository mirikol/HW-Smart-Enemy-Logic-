using System.Collections;
using System.Collections.Generic;

namespace Labirint
{
    public class UnitsEnumerator : IEnumerable<Unit>
    {
        private List<Unit> _units = new List<Unit>();

        public void AddUnit(Unit unit)
        {
            _units.Add(unit);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Unit> GetEnumerator()
        {
            foreach (Unit unit in _units)
            {
                yield return unit;
            }
        }
    }
}
