using System;

using DomainLayer.DTO.Account;

namespace DomainLayer.DTO.Student
{
    public sealed class StudentResponse : UserResponse
    {
        public int? DegreeId { get; set; }
    }
}
