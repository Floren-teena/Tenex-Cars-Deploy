using TenexCarsDeploy.Data.Enums;

namespace TenexCarsDeploy.Data.ViewModels
{
    public class QueryObject
    {
        public string? CarMake { get; set; } 
        public string? CarModel { get; set; }
        public CarType? CarType {get; set; } 
        public string? CompanyName { get; set; }
    }
}
