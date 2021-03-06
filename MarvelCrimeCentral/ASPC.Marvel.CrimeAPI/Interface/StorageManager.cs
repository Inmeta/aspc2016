﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace ASPC.Marvel.CrimeAPI
{
    public static class StorageManager
    {
        static StorageManager()
        {
            //var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);

            //using (var clientContext = spContext.CreateUserClientContextForSPHost())
            //{
            //    Web web = clientContext.Web;
            //    clientContext.Load(web);
            //    clientContext.ExecuteQuery();

            //    ListCollection lists = web.Lists;
            //    clientContext.Load<ListCollection>(lists);
            //    clientContext.ExecuteQuery();
            //}
            //Database.Add(new Crime() { Name = "Robbery", Location = new Geolocation(59.16185080545863, 10.627836064862777), Source = "Hulk" });
            //Database.Add(new Crime() { Name = "Violence", Location = new Geolocation(59.288478375006096, 10.700847142654197), Source = "Spiderman" });
            //Database.Add(new Crime() { Name = "Narcotics", Location = new Geolocation(59.388478375006096, 10.700847142654197), Source = "Blackwidow" });
            //Database.Add(new Crime() { Name = "Traffic offenses", Location = new Geolocation(59.488478375006096, 10.700847142654197), Source = "Thor" });
            //Database.Add(new Crime() { Name = "Vandalism", Location = new Geolocation(59.588478375006096, 10.700847142654197), Source = "Hulk" });
            //Database.Add(new Crime() { Name = "N/A", Location = new Geolocation(59.688478375006096, 10.700847142654197), Source = "Ironman" });

            //Database.Add(new Agent() { Name = "Spiderman", Location = new Geolocation(59.9750629, 10.6641534), BPM = 92, GSR = 34, UV = 12, Barometer = 23 });
            //Database.Add(new Agent() { Name = "Blackwidow", Location = new Geolocation(59.9750529, 10.6641634), BPM = 74, GSR = 56, UV = 12, Barometer = 23 });
            //Database.Add(new Agent() { Name = "Thor", Location = new Geolocation(59.9750729, 10.6641734), BPM = 75, GSR = 74, UV = 12, Barometer = 23 });
            //Database.Add(new Agent() { Name = "Hulk", Location = new Geolocation(59.9750429, 10.6641434), BPM = 120, GSR = 90, UV = 12, Barometer = 23 });
            //Database.Add(new Agent() { Name = "Ironman", Location = new Geolocation(59.9750729, 10.6641234), BPM = 83, GSR = 63, UV = 12, Barometer = 23 });


        }



        internal static MemoryStorage<T> Storage<T>() where T : Node
        {
            var ps = new MemoryStorage<T>(Database);
            //ps.OnChange += delegate (object sender, ChangeType crud) { Changes<T>(sender as T, crud); };
            return ps;
        }

        public static List<Node> Database = new List<Node>();
    }
}