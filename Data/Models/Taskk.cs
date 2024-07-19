using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Taskk
    {
        [Key]
        public int taskId { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public bool completed { get; set; } = false;
        public IEnumerable<SubTask> subTasks { get; set; }
        [NotMapped]
        public int subTaskCount { get; set; }
    }
}
