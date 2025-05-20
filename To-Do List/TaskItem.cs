using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_List
{
    public class TaskItem
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool IsDone {  get; set; } = false;

        public required string Description { get; set; }

    }
}
