using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.DAL.DataContext
{
    class AppSetting
    {
        public string SQLConnectionString { get; set; }


        public AppSetting()
        {
            SQLConnectionString = "server=localhost; port=3306; database=GroupSpace; user=root; password=; Persist Security Info=False; Connect Timeout=300";

        }
    }
}
