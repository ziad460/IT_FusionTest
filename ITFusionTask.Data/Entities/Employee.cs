using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.PortableExecutable;

namespace ITFusionTask.Data.Entities
{
    public class Employee
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string E_Name { get; set; }
        
        public DateTime E_JoinDate { get; set; }

        public string E_Phone { get; set; }

        public int E_GenderId { get; set; }

        public decimal E_Salary { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("E_GenderId")]
        public virtual Genders Gender { get; set; }
    }
}
