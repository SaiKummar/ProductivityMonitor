namespace ProductivityMonitor.Models.Entity
{
    public class TaskEnt
    {
        public int Task_Id { get; set; }
        public string Task_Name { get; set; }
        public DateTime Task_Cdatetime { get; set; }
        public string Task_Type { get; set; }
        public int Task_Ref_Task_Id { get; set; }
        public string Task_Category { get; set; }
        public string Task_Desc { get; set; }
        public int Task_Creator { get; set; }
        public Decimal Task_Noh_Reqd { get; set; }
        public DateTime Task_exp_datetime { get; set; }
        public DateTime Task_cmp_datetime { get; set; }
        public int Task_supervisor { get; set; }
        public string Task_remarks { get; set; }
        public string Task_status { get; set; }
    }
}
