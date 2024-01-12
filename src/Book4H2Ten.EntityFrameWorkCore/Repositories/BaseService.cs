using Book4H2Ten.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book4H2Ten.EntityFrameWorkCore.Repositories
{
    public interface IBaseService<T> where T : class
    {

    }

    public class BaseService<T> :IBaseService<T> where T : BaseEntity
    {

    }
}
