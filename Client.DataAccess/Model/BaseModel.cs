using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace Client.DataAccess.Model
{
    public class BaseModel
    {
        [Column(Name = "ID", IsPrimaryKey = true, DbType = "VARCHAR")]
        [Key]
        public string Id { get; set; }

        public BaseModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
