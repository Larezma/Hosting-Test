using Domain.Interfaces.Wrapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Interfaces.Repository
{
    public interface ICommentsRepository : IRepositoryBase<Comment>
    {
    }
}