using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accelbuffalo.Core
{
    class DatabaseCore
    {
        public static string name { get; set; }
        public static string organisation { get; set; }

        public void SetName(string text)
        {
            name = text;
        }
        public void SetOrganisation(string text)
        {
            organisation = text;
        }
        public string GetName()
        {
            return name;
        }
        public string GetOrganisation()
        {
            return organisation;
        }
    }
}
