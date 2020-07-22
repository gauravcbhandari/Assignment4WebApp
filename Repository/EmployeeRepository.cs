using WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private EmployeeContext dbcontext;

        public EmployeeRepository(EmployeeContext context)
        {
            dbcontext = context;
        }

        public async Task<List<Employee>> GetAllEmployee()
        {
            return dbcontext.Employee.ToList();
        }
    
        public async Task<int> SaveEmployee(Employee employee)
        {
            dbcontext.Employee.Add(employee);
            return dbcontext.SaveChanges();
        }
    }
}
