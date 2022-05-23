namespace AdministartionWebsite.ViewModels.Project;

public class ProjectPageViewModel
{
    public int ProjectId { get; set; }
    public string? ProjectName { get; set; }
    public string? CustomerName { get; set; }
    public List<ProjectPageViewModelListItem>? TimeRegs { get; set; }
    public int TotalMinutesSpent { get; set; }
}

public class ProjectPageViewModelListItem
{
    public DateTime Date { get; set; }
    public int MinutesSpent { get; set; }
    public string? Description { get; set; }
}