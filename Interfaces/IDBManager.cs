using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IDBManager<T>
    {
        T getObj(int ID);
        List<T> getAll();
        void add(T obj);
        void remove(T obj);
        void update();
    }
}
