using CINQWCF.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Configuration;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CINQWCFHostWPF
{
    class ConsumeWCF
    {
        public string endPoint { get; set; }
        public IEnumerable<PersonalData> GetDataList(string value)
        {
            ServiceReference1.ComponentManagerClient proxy = new ServiceReference1.ComponentManagerClient(value);
            proxy.Open();
            var filtroList = from l in proxy.ReadData()
                             where l.Age > 16
                             orderby l.LastName
                             select l;
            proxy.Close();
            return (filtroList);
        }
    }
}
