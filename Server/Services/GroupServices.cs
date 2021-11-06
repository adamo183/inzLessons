using Common.DAL.UnitOfWork;
using inzLessons.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inzLessons.Server.Services
{
    public interface IGroupServices
    {
        
    }

    public class GroupServices : IGroupServices
    {
        UnitOfWork _unitOfWork = new UnitOfWork();


    }
}
