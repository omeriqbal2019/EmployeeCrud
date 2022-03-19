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

                     //select new
                     //   {
                     //      h.FirstName,
                     //      h.LastName,
                     //      h.Email,
                     //      h.EmployeeId,
                     //      h.PhoneNumber,
                     //      h.Username
                     //   });

            if (data != null)
            {
                return data;
                //List<EmployeeDto> listdto = new List<EmployeeDto>();

                //listdto.AddRange()
                //foreach (var item in data)
                //{
                //    EmployeeDto dto = new EmployeeDto
                //    {
                //        FirstName = item.FirstName,
                //        LastName = item.LastName,
                //        Email = item.Email,
                //        PhoneNumber = item.PhoneNumber,
                //        EmployeeId=item.EmployeeId,
                //        Username=item.Username
                //    };
                //    listdto.Add(dto);
                //}

                //return listdto;

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
            //var data = (from h in employeeRepository.GetAll().Where(x=>x.EmployeeId== empId)

            //            select new
            //            {
            //                h.FirstName,
            //                h.LastName,
            //                h.Email,
            //                h.EmployeeId,
            //                h.PhoneNumber,
            //                h.Username
            //            }).SingleOrDefault();

            //if (data != null)
            //{
               
               
            //        EmployeeDto dto = new EmployeeDto
            //        {
            //            FirstName = data.FirstName,
            //            LastName = data.LastName,
            //            Email = data.Email,
            //            PhoneNumber = data.PhoneNumber,
            //            EmployeeId = data.EmployeeId,
            //            Username = data.Username
            //        };

            //    return dto;

            //}


            return null;
        }
        public MessageStatusDto SignUpEmployee(EmployeeDto employeeDto, int userId)
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

            messageReponse.SuccessMessage = "Save Successfully";
            return messageReponse;
        }

        public MessageStatusDto UpdateEmployee(EmployeeDto employee, int empId)
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
                EmployeeData.ModifiedBy = empId;
                EmployeeData.ModifiedDate = DateTime.Now;
                
                employeeRepository.Update(EmployeeData);
                _unitOfWork.Commit();
                return new MessageStatusDto { Id = 1, SuccessMessage = "Save Successfully" , ErrorMessage =null};
            }

            return new MessageStatusDto { Id =0, ErrorMessage = "No Record Found", SuccessMessage =null };
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
                return new MessageStatusDto { Id=1,SuccessMessage="Deleted Successfully" };
            }
            return new MessageStatusDto { Id = 0, SuccessMessage = "No Record Found" };
        }
    }
}

