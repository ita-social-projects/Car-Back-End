using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarBackEnd.Model
{
    public class UserModel
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string OfficeLocation { get; set; }

        public string EmailAddress { get; set; }

        public string AzureId { get; set; }
    }
}
