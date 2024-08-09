using System.Collections.Generic;
using System.Linq;

namespace Blasphemous.AtriumOfAtonement;

public class ItemList<T>
{
    public IEnumerable<T> Items
    {
        get
        {
            return GetType()
                .GetProperties()
                .Where(p => (p.PropertyType == typeof(T))
                    || (p.PropertyType.IsSubclassOf(typeof(T))))
                .Select(p => (T)p.GetValue(this, null));
        }
    }
}
