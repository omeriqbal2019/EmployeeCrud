using Employee.Models.ApiModelsDto.ErrorMessageDto;
using Employee.Models.ApiModelsDto.UserDto;
using Employee.Models.Entities.EmployeeEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employee.Services.EmployeeService
{
    public interface IEmployeeService
    {
        public IList<EmployeeEntity> GetAllEmployee();
        public MessageStatusDto SignUpEmployee(EmployeeDto empDto,int? userId);
        public MessageStatusDto UpdateEmployee(EmployeeDto empDto, int userId,int updatedBy);
        MessageStatusDto DeleteEmployee(int userId, int modifiedById);
        public EmployeeEntity GetEmployeeById(int empId);
    }
}
