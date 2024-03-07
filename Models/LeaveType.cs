using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Organization_Assessment_Project_NEW.Models
{
    public class LeaveType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? DefaultDays { get; set; }
        public DateTime? DateCreated { get; set; }

        public static implicit operator LeaveType(int v)
        {
            throw new NotImplementedException();
        }
    }
}
