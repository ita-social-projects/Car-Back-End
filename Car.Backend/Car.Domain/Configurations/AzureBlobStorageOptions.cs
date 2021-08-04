using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.Domain.Configurations
{
    public class AzureBlobStorageOptions
    {
        public virtual string? AccessKey { get; set; }

        public virtual string? Container { get; set; }
    }
}
