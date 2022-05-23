namespace AdministartionWebsite.ViewModels.Customer;

public class CustomerPageViewModel
{
    public int Id { get; set; }
    public string? CustomerName { get; set; }
    public List<CustomerPageListItem>? CustomerProjects { get; set; }
}

public class CustomerPageListItem
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int regAmount { get; set; }
    public DateTime latestRegDate { get; set; }
}