namespace AdministartionWebsite.ViewModels.Home;

public class HomeIndexViewModel
{
    public int CustomerAmount { get; set; }
    public string? NewestCustomerName { get; set; }
    public int ProjectAmount { get; set; }
    public int MostProjectsForCustomer { get; set; }
    public string? MostRegsProject { get; set; }
    public int AmountOfRegsToday { get; set; }
}