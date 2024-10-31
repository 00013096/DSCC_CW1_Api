using DSCC_CW1_Api.Data;
using DSCC_CW1_Api.Interfaces;
using DSCC_CW1_Api.Models;

namespace DSCC_CW1_Api.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(EmployeeContext context) : base(context) { }
    }
}
