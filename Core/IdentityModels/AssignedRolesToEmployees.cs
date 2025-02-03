namespace Core.IdentityModels
{
    public class AssignedRolesToEmployees
    {
        public int RoleEmpID { get; set; }
        public int RoleID { get; set; }
        public required string EmpUserName { get; set; }
        public string? Note { get; set; }
    }
}