using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeFootball.Domain.Entities
{
    public class SearchableColumn
    {
        public int Id { get; set; }
        public string ColumnName { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
    }
}
