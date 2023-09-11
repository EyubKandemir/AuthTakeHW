using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Data.Tables
{
    [Table("site_passwords")]
    public class Sites
    {
        [Key]
        [Column("objectid")]
        public int Id { get; set; }

        [Column("site_name")]
        public string? SiteName { get; set; }

        [Column("user_name")]
        public string? UserName { get; set; }

        [Column("site_password")]
        public string? Password { get; set; }

    }
}
