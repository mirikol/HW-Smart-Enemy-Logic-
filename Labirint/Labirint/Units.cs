using System.Collections;
using System.Collections.Generic;

namespace Labirint
{
    public class Units : IEnumerable<Unit>
    {
        private List<Unit> _units = new();

        public void Add(Unit unit) => _units.Add(unit);
        public void Remove(Unit unit) => _units.Remove(unit);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<Unit> GetEnumerator()
        {
            foreach (var unit in _units)
            {
                yield return unit;
            }
        }
    }
}
