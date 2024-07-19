using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTOs.TaskDTOs
{
    public class TaskCreateUpdateDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        public string name { get; set; }
        [Required]
        public bool completed { get; set; }
    }
}
