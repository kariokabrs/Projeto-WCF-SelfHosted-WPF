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
    public interface IWCFFactory
    {
        IEnumerable<PersonalData> GetDataList(string value);
    }
    public class CINQWCFService : IWCFFactory
    {
        public IEnumerable<PersonalData> GetDataList(string value)
        {
            ServiceReference1.ComponentManagerClient proxy = new ServiceReference1.ComponentManagerClient(value);
            proxy.Open();
            var filtroList = from l in proxy.ReadData()
                             where l.Age > 16
                             orderby l.LastName
                             select l;
            proxy.Close();
            return filtroList;
        }
    }
    public abstract class WCFFactory
    {
        public string endPoint { get; set; }
        public abstract IWCFFactory GetWCF(string WCFServer);
    }
    public class ConcreteWCFFactory : WCFFactory
    {
        public override IWCFFactory GetWCF(string WCFServer)
        {
            switch (WCFServer)
            {
                case "CINQWCF":
                    return new CINQWCFService();
                default:
                    throw new ApplicationException(string.Format("WCF '{0}' cannot be created", WCFServer));
            }
        }
    }
}
