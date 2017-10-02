using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace Client.DataAccess.Model
{
    public class BaseModel
    {
        [Column(Name = "ID", IsDbGenerated = true, IsPrimaryKey = true, DbType = "VARCHAR")]
        [Key]
        public string Id { get; set; }

        public BaseModel()
        {
            Id = string.Empty;
        }
    }
}
