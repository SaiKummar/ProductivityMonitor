namespace ProductivityMonitor.Models.Resource
{
    public class SprintRes
    {
        public int SprintId { get; set; }
        public int SprintModuleId { get; set; }
        public string SprintMaster { get; set; }
        public DateTime SprintStartDate { get; set; }
        public DateTime SprintEndDate { get; set; }
    }
}
