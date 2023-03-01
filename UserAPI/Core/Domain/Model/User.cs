using System.ComponentModel.DataAnnotations;

namespace UserAPI.Core.Domain.Model
{
    public partial class User
    {
        public User()
        {

        }

        [Key]
        public string Uid { get; set; } = null!;
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? LastLogout { get; set; }
        public bool IsActive { get; set; }
        public string? Password { get; set; }
        public DateTime? AppTimeStamp { get; set; }
        public string? AppCreatedBy { get; set; }
        public DateTime? AppCreationDate { get; set; }
        public string? AppLastUpdatedBy { get; set; }
        public byte[]? SysTimeStamp { get; set; }       

    }
}
