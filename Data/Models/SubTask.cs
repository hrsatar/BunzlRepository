using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class SubTask
    {
        [Key]
        public int subTaskId { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public bool completed { get; set; } = false;
        [ForeignKey(nameof(Taskk))]
        public int taskId { get; set; }
        public Taskk task { get; set; }
    }
}
