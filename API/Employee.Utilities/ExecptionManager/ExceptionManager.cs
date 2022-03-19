using Employee.DBCore.Uow;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employee.Utilities.ExecptionManager
{
    public class ExceptionManager:IExectpionManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public ExceptionManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
    }
}
