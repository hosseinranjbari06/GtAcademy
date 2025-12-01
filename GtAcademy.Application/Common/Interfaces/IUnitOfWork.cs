using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
