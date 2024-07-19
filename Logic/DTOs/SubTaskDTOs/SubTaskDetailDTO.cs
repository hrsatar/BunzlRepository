using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTOs.SubTaskDTOs
{
    public class SubTaskDetailDTO
    {
        public int subTaskId { get; set; }
        public string name { get; set; }
        public bool completed { get; set; } = false;
        public int taskId { get; set; }
    }
}
