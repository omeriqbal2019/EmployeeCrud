using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Employee.Models.ApiModelsDto.ErrorMessageDto
{
    public class MessageStatusDto
    {

        [Key]
        public int Id { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
    }
}
