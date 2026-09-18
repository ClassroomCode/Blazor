using System.Net.Http.Json;

public class NorthwindService(HttpClient httpClient) : INorthwindService
{
    public async Task<Customer[]> GetCustomers() {
        var customers = await httpClient.GetFromJsonAsync<Customer[]>("customer");
        return customers ?? [];
    }
}