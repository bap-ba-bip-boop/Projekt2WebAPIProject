namespace AdministartionWebsite.ViewModels.Project;

public class ProjectIndexViewModel
{
    public List<ProjectIndexVMListItem>? Projects { get; set; }
}

public class ProjectIndexVMListItem
{
    public int ProjectId { get; set; }
    public string? ProjectName { get; set; }
    public string? CustomerName { get; set; }
}
