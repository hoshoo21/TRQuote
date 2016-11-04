using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Diagnostics;
using System.Reflection;
using TRQuoteCore.WindowsHelperClasses;
using TRQuoteCore.QuoteStructure;
using TRQuoteCore.Quotes.Common;
using TRQuoteCore.GridStructure;
using TRQuoteCore.Core;
namespace TRQuoteCore.GQuoteMonitor
{
    class Program
    {

        [STAThread]
        static void Main(string[] args)
        
        {
            try{
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new TRQuoteCore.GQuoteMonitor.QuoteMonitorWindow(new GridStructure.QuoteMonitorSettings(), 300));
        
            }
            catch (Exception exx)
            {
                MessageBox.Show(exx.ToString());
            }
        }

        
        //public static ActivApplication CreateActiveApplication()
        //{
        //    Settings settings = new Settings();
        //    settings.ServiceLocationIniFile = "ServiceLocation.xml";
        //    ActivApplication application = new ActivApplication(settings);
        //    return application;
        //}
        //public static ConnectParameters CreateConnectionParams(ActivApplication application)
        //{
        //    string serviceFilePath = Assembly.GetExecutingAssembly().Location;
        //    serviceFilePath = serviceFilePath.Substring(0, serviceFilePath.LastIndexOf(Path.GetFileName(serviceFilePath)));
        //    StatusCode statusCode;
        //    IList<ServiceInstance> serviceInstanceList = new List<ServiceInstance>();
        //    IDictionary<string, object> attributes = new Dictionary<string, object>();

        //    attributes.Add(FileConfiguration.FileLocation, serviceFilePath + application.Settings.ServiceLocationIniFile);

        //    statusCode = ServiceApi.FindServices(ServiceApi.ConfigurationTypeFile, "Service.WorkstationService", attributes, serviceInstanceList);

        //    if (StatusCode.StatusCodeSuccess != statusCode)
        //    {
        //        throw new Exception("FindServices() failed, error - " + EnumDescription.GetDescription(statusCode));
        //    }

        //    ServiceInstance serviceInstance = serviceInstanceList[0];
        //    string _ServiceInstanceId = "";

        //    if (_ServiceInstanceId != null)
        //    {
        //        foreach (ServiceInstance si in serviceInstanceList)
        //        {
        //            if (si.ServiceAccessPointList[0].Id == _ServiceInstanceId)
        //            {
        //                serviceInstance = si;
        //                break;
        //            }
        //        }
        //    }

        //    _ServiceInstanceId = serviceInstance.ServiceAccessPointList[0].Id;

        //    ConnectParameters connectParameters = new ConnectParameters();
        //    connectParameters.ServiceId = serviceInstance.ServiceId;
        //    connectParameters.Url = serviceInstance.ServiceAccessPointList[0].Url;
        //    connectParameters.UserId = GQCore.Configuration.UserID;
        //    connectParameters.Password = GQCore.Configuration.Password;
        //    return connectParameters;
        //}
    }
}
