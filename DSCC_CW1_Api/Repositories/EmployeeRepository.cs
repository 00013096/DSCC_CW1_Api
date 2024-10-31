using DSCC_CW1_Api.Data;
using DSCC_CW1_Api.Interfaces;
using DSCC_CW1_Api.Models;

namespace DSCC_CW1_Api.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EmployeeContext context) : base(context) { }
    }
}
