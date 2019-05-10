using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Entities.Categorys
{
    [Table("Category")]
    public class Category
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public bool IsMenu { get; set; }

        public string SysResource { get; set; }

        public string ResourceID { get; set; }

        public string FatherResource { get; set; }

        public string FatherID { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string RouteName { get; set; }

        public string CssClass { get; set; }

        public int Sort { get; set; }

        public bool IsDisable { get; set; }
    }
}
