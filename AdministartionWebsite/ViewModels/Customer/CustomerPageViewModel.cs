namespace AdministartionWebsite.ViewModels.Customer;

public class CustomerPageViewModel
{
    public int CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public List<CustomerPageProjectListItem>? CustomerProjects { get; set; }
}

public class CustomerPageProjectListItem
{
    public int ProjectId { get; set; }
    public string? ProjectName { get; set; }
    public int regAmount { get; set; }
    public string? latestRegDate { get; set; }
}