namespace ProductivityMonitor.Models.Input
{
    public class TaskModel
    {
        public string TaskName { get; set; }
        public string TaskType { get; set; }
        public int? TaskReferenceTaskId { get; set; }
        public string TaskCategory { get; set; }
        public string TaskDescription { get; set; }
        public string TaskCreator { get; set; }
        public decimal NumberOfHoursRequired { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string TaskSupervisor { get; set; }
    }
}
