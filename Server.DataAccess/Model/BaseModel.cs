using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.DataAccess.Model
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; }

        public BaseModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
