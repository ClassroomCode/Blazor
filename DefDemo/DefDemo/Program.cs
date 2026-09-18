using Microsoft.EntityFrameworkCore;
using SvcLayer;

namespace DefDemo
{
    internal class Program
    {
        async static Task Main(string[] args) {

            var svc = NorthwindServiceFactory.Create();
            var q = svc.GetCustomersDef();
            var q2 = q.Where(c => c.CompanyName.StartsWith('A'));
            var r = await svc.ToAnArray(q2);

            foreach (var customer in q2) {
                Console.WriteLine(customer.CompanyName);
            }
        }
    }
}
