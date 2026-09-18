using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public interface INorthwindService
    {
        Task<Customer[]> GetCustomers();
        IQueryable<Customer> GetCustomersDef();

        Task<Customer[]> ToAnArray(IQueryable<Customer> q);
    }
}
