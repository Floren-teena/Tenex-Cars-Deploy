using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenexCarsDeploy.Data.Models;

namespace TenexCarsDeploy.Data.Repositories.Interfaces
{
	public interface IVehicleRequestRepository
	{
		Task<VehicleRequest> AddVehicleRequestLog(VehicleRequest request);
		Task<VehicleRequest?> GetVehicleRequestBySubscriberId(string id);
	}
}
