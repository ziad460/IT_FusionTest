namespace ITFusionTask.Data.Dtos.API_Dtos
{
    public class EmployeesRequestRoot
    {
        public List<EmployeesDatum> EmployeesData { get; set; }
    }

    public class EmployeesDatum
    {
        public string E_Name { get; set; }
        public string E_Phone { get; set; }
        public decimal E_Salary { get; set; }
        public string E_JoinDate { get; set; }
        public string E_Gender { get; set; }
    }
}
