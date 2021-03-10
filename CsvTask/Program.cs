  using System;
using System.Collections.Generic;
using System.Linq;

namespace CsvTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var record = GetLines(@"AssetTracking.csv");


            var maxBattery = record.OrderByDescending(item => item.BatteryPercentage).First().BatteryPercentage;
            // Devices with max battery
            var DevicesMaxBattery = record.Where(item => item.BatteryPercentage == maxBattery);

            Console.WriteLine("----------Devices with max Battery------------------------------------------------------------------------------------\n");
            Console.WriteLine("Max Battery = " + maxBattery+"%");
            foreach (var items in DevicesMaxBattery)
            {
                Console.WriteLine("{Device Name : " + items.DeviceName + ", Os Type: " + items.OsType + " }" );
            }
            
            var below20 = record.Where(item => item.BatteryPercentage < 20);
            Console.WriteLine("\n----------Devices below 20% Battery------------------------------------------------------------------------------------");

            foreach (var items in below20)
            {
                Console.WriteLine("{Device Name: "+items.DeviceName+", OS Type: "+items.OsType + ", Battery: "+ items.BatteryPercentage+"% }");
            }
            
            Console.WriteLine("\n---------Devices with max Deplyment Count------------------------------------------------------------------------------");
            var maxDeployed = record.OrderByDescending(item => item.DeployedJobs).First().DeployedJobs;
            var DeviceMaxDeployed = record.Where(item => item.DeployedJobs == maxDeployed);
            foreach (var items in DeviceMaxDeployed)
            {
                Console.WriteLine("{Device Name: " + items.DeviceName + ", OS Type: " + items.OsType +", Deployment Count : " + items.DeployedJobs+ ", Battery: " + items.BatteryPercentage + "% }");
            }



        }

        public static List<AssetTrack> GetLines(string file)
        {
            return System.IO.File.ReadAllLines(file)
                .Skip(2)
                .Select(AssetTrack.ParseRow)
                .ToList();
        }
        public class AssetTrack
        {
            public string DeviceName { get; set; }
            public string DeviceModel { get; set; }
            public string OsVersion { get; set; }
            public string OsType { get; set; }
            public int BatteryPercentage { get; set; }
            public string BatteryStatus{ get; set; }
            public int FreeStorageMemory { get; set; }
            public int FreeProgramMemory { get; set; }
            public int SheduledJobs { get; set; }
            public int InprogressJobs { get; set; }
            public int FailedJobs { get; set; }
            public int DeployedJobs { get; set; }

            internal static AssetTrack ParseRow(String row)
            {
                var columns = row.Split(',');
                if (columns[4] == "N/A" || int.Parse(columns[4]) >100 || int.Parse(columns[4]) < 0) columns[4] = "0";
                if (columns[6] == "N/A") columns[6] = "0";
                if (columns[7] == "N/A") columns[7] = "0";
                if (columns[8] == "N/A") columns[8] = "0";
                if (columns[9] == "N/A") columns[9] = "0";
                if (columns[10] == "N/A") columns[10] = "0";
                if (columns[11] == "N/A") columns[11] = "0";
                return new AssetTrack()
                {
                    DeviceName = columns[0],
                    DeviceModel = columns[1],
                    OsVersion = columns[2],
                    OsType = columns[3],
                    BatteryPercentage = int.Parse(columns[4]),
                    BatteryStatus = columns[5],
                    FreeStorageMemory = int.Parse(columns[6]),
                    FreeProgramMemory = int.Parse(columns[7]),
                    SheduledJobs = int.Parse(columns[8]),
                    InprogressJobs = int.Parse(columns[9]),
                    FailedJobs = int.Parse(columns[10]),
                    DeployedJobs = int.Parse(columns[11])


                };
            }
        }
    }
}