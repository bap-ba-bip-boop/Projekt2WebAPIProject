namespace AdministartionWebsite.ViewModels.Customer;

public class CustomerIndexViewModel
{
    public List<CustomerIndexVMListItem>? Customers { get; set; }
}

public class CustomerIndexVMListItem
{
    public int CustomerId { get; set; }
    public string? Name { get; set; }
}