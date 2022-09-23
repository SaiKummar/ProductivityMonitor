namespace ProductivityMonitor.Models.Resource
{
    public class ProjectRes
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public string ProjectStartDate { get; set; }
        public int ProjectManager { get; set; }
        public string ProjectStatus { get; set; }
        public DateTime ProjectLastUpdateDate { get; set; }
    }
}
