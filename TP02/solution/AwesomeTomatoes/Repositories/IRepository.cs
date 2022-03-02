using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeTomatoes.Repositories
{
    // TODO TP03: Implementar um método que utiliza "Predicados"
    public interface IRepository<T>
    {
        void Insert(T entity);
        void Delete(T entity);
        T GetById(int id);
    }
}
