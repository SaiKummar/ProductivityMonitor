namespace ProductivityMonitor.Models.Resource
{
    public class TaskRes
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public DateTime TaskCreationDate { get; set; }
        public string TaskType { get; set; }
        public int TaskReferenceTaskId { get; set; }
        public string TaskCategory { get; set; }
        public string TaskDescription { get; set; }
        public string TaskCreator { get; set; }
        public Decimal NumberOfHoursRequired { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CompletionDate { get; set; }
        public string TaskSupervisor { get; set; }
        public string TaskRemarks { get; set; }
        public string TaskStatus { get; set; }
    }
}
