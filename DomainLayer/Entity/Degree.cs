

namespace DomainLayer.Entity
{
    public class Degree
    {
        public int Id { get; set; }
        public required string DegreeTitle { get; set; }

        public int? DepartmentId { get; set; }
    }
}
