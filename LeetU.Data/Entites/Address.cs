namespace LeetU.Data.Entites
{
    public partial class Address
    {
        public Address()
        {
            Students = new HashSet<Student>();
        }

        public long Id { get; set; }
        public string AddressLine1 { get; set; } = null!;
        public string? AddressLine2 { get; set; }
        public string? AddressLine3 { get; set; }
        public string? Town { get; set; }
        public string? County { get; set; }
        public string? PostCode { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
