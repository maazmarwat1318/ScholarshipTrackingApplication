namespace DataLayer.Mapper
{
    internal static class UserMapper
    {
        public static DomainLayer.Entity.User MapDbEntityToDomain(DataLayer.Entity.User dbEntity)
        {
            return new DomainLayer.Entity.User() { FirstName = dbEntity.FirstName, LastName = dbEntity.LastName, Email= dbEntity.Email, Password=dbEntity.Password, Role = dbEntity.Role, Id=dbEntity.Id
           };
        }

        public static DataLayer.Entity.User MapDomainEntityToDb(DomainLayer.Entity.User domainEntity)
        {
            return new DataLayer.Entity.User()
            {
                FirstName = domainEntity.FirstName,
                LastName = domainEntity.LastName,
                Email = domainEntity.Email,
                Password = domainEntity.Password,
                Role = domainEntity.Role,
                Id = domainEntity.Id
            };
        }
    }
}
