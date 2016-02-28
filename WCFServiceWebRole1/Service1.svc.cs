using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Yammer.api;

namespace WCFServiceWebRole1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        static string tenant = "https://aspc1605.sharepoint.com/ASPC/";
        static string userName = "admin@aspc1605.onmicrosoft.com";
        static string passwordString = "pass@word1";

        public string CreateAlert(double coordinateX, double coordinateY, int UniqueID)
        {
            try {
                CreateListItem(coordinateX, coordinateY, UniqueID);
                var client = new YammerClient("954496-BjOb9f4eISyMX0HI0d3XTg");
                client.PostMessage(string.Format("ALERT from MSBand! ID: {0} Coordinates: {1},{2}",UniqueID,coordinateX,coordinateY), Convert.ToInt64(7465273), "Alert");
                return string.Format("Alert added to list!");
            }
            catch(Exception exp)
            {
                return string.Format("Error sending the alert: {0}"+exp.Message);
            }

            
        }

        private static void CreateListItem(double coordinateX, double coordinateY, int UniqueID)
        {
            // Get access to source site
            using (var ctx = new ClientContext(tenant))
            {
                //Provide count and pwd for connecting to the source
                var passWord = new SecureString();
                foreach (char c in passwordString.ToCharArray()) passWord.AppendChar(c);
                ctx.Credentials = new SharePointOnlineCredentials(userName, passWord);
                // Actual code for operations
                Web web = ctx.Web;
                ctx.Load(web);
                ctx.ExecuteQuery();

                List myList = web.Lists.GetByTitle("Alerts");

                ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                ListItem newItem = myList.AddItem(itemCreateInfo);
                newItem["AlertCoordinatesX"] = coordinateX.ToString();
                newItem["AlertCoordinatesY"] = coordinateY.ToString();
                newItem["AlertID"] = UniqueID.ToString();
                newItem.Update();

                ctx.ExecuteQuery();
            }

        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public void SendMonitoringData(int UniqueID, string type, double value)
        {
            // Get access to source site
            using (var ctx = new ClientContext(tenant))
            {
                //Provide count and pwd for connecting to the source
                var passWord = new SecureString();
                foreach (char c in passwordString.ToCharArray()) passWord.AppendChar(c);
                ctx.Credentials = new SharePointOnlineCredentials(userName, passWord);
                // Actual code for operations
                Web web = ctx.Web;
                ctx.Load(web);
                ctx.ExecuteQuery();

                List myList = web.Lists.GetByTitle("MonitorLiveData");

                ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                ListItem newItem = myList.AddItem(itemCreateInfo);
                newItem["AlertID"] = UniqueID.ToString();
                newItem["DataType"] = type.ToString();
                newItem["DataValue"] = value;
                newItem.Update();

                ctx.ExecuteQuery();
            }
        }
    }
}
