namespace ProductivityMonitor.Models.Entity
{
    public class ProjectEnt
    {
        public int Proj_Id { get; set; }
        public string Proj_Name { get; set; }
        public string Proj_Desc { get; set; }
        public string Proj_Stdate { get; set; }
        public int Proj_Manager { get; set; }
        public string Proj_Status { get; set; }
        public DateTime Proj_Ludate { get; set; }
    }
}
