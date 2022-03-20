using Employee.Models.Entities.EmployeeEntity;
using Employee.DBCore.Uow;
using Employee.Models.ApiModelsDto.ErrorMessageDto;
using Employee.Models.ApiModelsDto.UserDto;
using Employee.Utilities.Hashing;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Employee.Utilities.ErrorMessages;

namespace Employee.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly AppSettings _appSettings;

        public EmployeeService(IUnitOfWork unitOfWork, IOptions<AppSettings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _appSettings = appSettings.Value;
        }

        public IList<EmployeeEntity> GetAllEmployee()
        {
            var employeeRepository = _unitOfWork.GetRepository<EmployeeEntity>();

            var data =  employeeRepository.GetAll().Where(x => x.IsDeleted != true).ToList();

            if (data != null)
            {
                return data;
            }
         
            return null;
        }
        public EmployeeEntity GetEmployeeById(int empId)
        {
            var employeeRepository = _unitOfWork.GetRepository<EmployeeEntity>();
            var data = employeeRepository.GetAll().Where(x => x.EmployeeId == empId).SingleOrDefault();
            if (data != null)
            {
                return data;
            }
          
            return null;
        }
        public MessageStatusDto SignUpEmployee(EmployeeDto employeeDto, int? userId)
        {
            MessageStatusDto messageReponse = new MessageStatusDto();
            var employeeRepository = _unitOfWork.GetRepository<EmployeeEntity>();
            EmployeeEntity employee = new EmployeeEntity
            {
                FirstName= employeeDto.FirstName,
                LastName=employeeDto.LastName,
                Email=employeeDto.Email,
                PhoneNumber=employeeDto.PhoneNumber,
                Username=employeeDto.Username,
                Password= Sha.GenerateSha512String(employeeDto.Password),
                IsDeleted=false,
                CreatedBy= userId,
                ModifiedBy=userId,
                CreatedDate=DateTime.Now,
                ModifiedDate=DateTime.Now
                
            };
            employeeRepository.Add(employee);
            _unitOfWork.Commit();

            messageReponse.SuccessMessage = MessageDetail.Success;
            return messageReponse;
        }

        public MessageStatusDto UpdateEmployee(EmployeeDto employee, int empId,int updatedBy)
        {
            var employeeRepository = _unitOfWork.GetRepository<EmployeeEntity>();
            var EmployeeData = employeeRepository.GetAll().SingleOrDefault(x => x.EmployeeId == empId);

            if (EmployeeData != null)
            {
                EmployeeData.FirstName= employee.FirstName;
                EmployeeData.LastName= employee.LastName;
                EmployeeData.PhoneNumber= employee.PhoneNumber;
                EmployeeData.Email = employee.Email;
                EmployeeData.Username= employee.Username;
                EmployeeData.ModifiedBy = updatedBy;
                EmployeeData.ModifiedDate = DateTime.Now;
                
                employeeRepository.Update(EmployeeData);
                _unitOfWork.Commit();
                return new MessageStatusDto { Id = 1, SuccessMessage = MessageDetail.Success, ErrorMessage =null};
            }

            return new MessageStatusDto { Id =0, ErrorMessage = MessageDetail.NoRecordFound, SuccessMessage =null };
        }

        public MessageStatusDto DeleteEmployee(int employeeId, int modifiedById)
        {

            var employeeRepository = _unitOfWork.GetRepository<EmployeeEntity>();
            var _Empdata = employeeRepository.GetAll().SingleOrDefault(x => x.EmployeeId == employeeId);

            if (_Empdata != null)
            {
                _Empdata.ModifiedBy = modifiedById;
                _Empdata.ModifiedDate = DateTime.Now;
                _Empdata.IsDeleted = true;
                employeeRepository.Update(_Empdata);
                _unitOfWork.Commit();
                return new MessageStatusDto { Id=1,SuccessMessage= MessageDetail.Delete, ErrorMessage=null};
            }
            return new MessageStatusDto { Id = 0, SuccessMessage = MessageDetail.NoRecordFound ,ErrorMessage=null };
        }
    }
}

