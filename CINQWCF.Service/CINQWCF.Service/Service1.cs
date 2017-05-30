using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CINQWCF.Service
{
  
    public class Service1 : IComponentManager
    {
        public IEnumerable<PersonalData> ReadData()
        {

            var listOfPerson = new List<PersonalData>();

            listOfPerson.Add(new PersonalData() { FirstName = "Sérgio", LastName = "Rezende", Country = "Brazil", Age = 43 });
            listOfPerson.Add(new PersonalData() { FirstName = "Cláudio", LastName = "Augusto", Country = "Italy", Age = 49 });
            listOfPerson.Add(new PersonalData() { FirstName = "Ana", LastName = "Cristina", Country = "Argentina", Age = 46 });
            listOfPerson.Add(new PersonalData() { FirstName = "Ricardo", LastName = "Santos", Country = "Brazil", Age = 15 });

            return listOfPerson;
        }
     
    }
}


