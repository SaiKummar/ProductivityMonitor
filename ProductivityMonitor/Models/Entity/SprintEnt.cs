namespace ProductivityMonitor.Models.Entity
{
    public class SprintEnt
    {
        public int Sprn_id { get; set; }
        public int Sprn_modl_id { get; set; }
        public string Sprn_master { get; set; }
        public DateTime Sprn_stdate { get; set; }
        public DateTime Sprn_enddate { get; set; }
        public string UserName { get; set; }
    }
}
