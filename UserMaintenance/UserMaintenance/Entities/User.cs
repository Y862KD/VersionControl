using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMaintenance.Entities
{
    public class User
    {
        public Guid ID { get; set; } = Guid.NewGuid();

        public  string FullName { get; set; }
             
    }
}
