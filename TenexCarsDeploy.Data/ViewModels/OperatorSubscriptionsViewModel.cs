using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenexCarsDeploy.Data.Models;

namespace TenexCarsDeploy.Data.ViewModels
{
    public class OperatorSubscriptionsViewModel

    {
        public Operator? Operator { get; set; }
        public IEnumerable<Subscription>? Subscriptions { get; set; }
    }
}
