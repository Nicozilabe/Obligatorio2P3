using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorio<T> where T : class
    {
        void Add(T obj);
        void Remove(int id);
        void Update(T obj);
        T FindById(int id);
        List<T> FindAll();
    }
}