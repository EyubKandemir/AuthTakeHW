using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data.Tables
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("objectid")]
        public int Id { get; set; }

        [Column("user_name")]
        public string? UserName { get; set; }

        [Column("password")]
        public string? Password { get; set; }

        [Column("name")]
        public string? FirstName { get; set; }

        [Column("surname")]
        public string? LastName { get; set; }

        [Column("email")]
        public string? Email { get; set; }
    }
}
