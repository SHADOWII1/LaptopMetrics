#pragma warning disable CA1416 // Platform compatibility warnings
#pragma warning disable CS8602  // Dereference of a possibly null reference
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using Prometheus;
using System.IO;
using System.Text.RegularExpressions;

namespace SystemMonitoringApp
{
    class GetMetrics
    {
        public record NetworkData(string InterfaceDescr, long NetworkSpeed, float BytesSent, float BytesReceived, string Status);
        public record DiskInfo(string DiskName, float FreeSpace, float UsedSpace, float TotalSpace);

        // Method to get CPU usage
        public static float GetCPUUsage()
        {
            float temp = 0;

            // Check if the application is running on Windows
            if (OperatingSystem.IsWindows())
            {
                // Get CPU usage using PerformanceCounter
                PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                temp = cpuCounter.NextValue();
                Thread.Sleep(1000);
                temp = cpuCounter.NextValue();
            }
            else
            {
                // Alternative for Linux: use /proc/stat
                using (var reader = new StreamReader("/proc/stat"))
                {
                    var line = reader.ReadLine();
                    var values = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (values.Length > 4)
                    {
                        long totalCpuTime = 0;
                        long idleTime = long.Parse(values[4]); // Idle time

                        for (int i = 1; i < values.Length; i++)
                        {
                            totalCpuTime += long.Parse(values[i]);
                        }

                        // Calculate CPU usage (percentage)
                        temp = (1 - (idleTime / (float)totalCpuTime)) * 100;
                    }
                }
            }

            Metrics.CreateGauge("cpu_usage", "CPU Utilization").Set(temp);
            return temp;
        }

        // Method to get RAM usage
        public static float GetRAMUsage()
        {
         // Define variables to store total and used memory
    float totalMemory = 0;
    float freeMemory = 0;

    // Read the contents of /proc/meminfo
    try
    {
        string[] lines = File.ReadAllLines("/proc/meminfo");
        foreach (var line in lines)
        {
            if (line.StartsWith("MemTotal:"))
            {
                totalMemory = ParseMemory(line);
            }
            else if (line.StartsWith("MemAvailable:"))
            {
                freeMemory = ParseMemory(line);
            }

            // Break the loop once we have both values
            if (totalMemory > 0 && freeMemory > 0)
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error reading memory info: {ex.Message}");
    }

    // Calculate used memory
    float usedMemory = totalMemory - freeMemory;
    float ramUsage = usedMemory / (1024 * 1024); // Convert to MB

    // Set the gauge metric for RAM usage
    Metrics.CreateGauge("ram_usage", "RAM Utilization").Set(ramUsage);
    
    return ramUsage;
}

        // Method to get network data
        public static NetworkData[] GetNetworkData()
        {
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

            NetworkData[] networkDatas = new NetworkData[interfaces.Length];
            int i = 0;

            foreach (var networkInterface in interfaces)
            {
                var stats = networkInterface.GetIPv4Statistics();
                bool isActive = networkInterface.OperationalStatus == OperationalStatus.Up &&
                        networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                        networkInterface.NetworkInterfaceType != NetworkInterfaceType.Tunnel &&
                        !networkInterface.Description.ToLower().Contains("virtual") &&
                        !networkInterface.Description.ToLower().Contains("pseudo");

        networkDatas[i] = new NetworkData(
            networkInterface.Description,
            networkInterface.Speed / 1000000,
            stats.BytesSent / (1024 * 1024), // Convert to MB
            stats.BytesReceived / (1024 * 1024), // Convert to MB,
            isActive ? "Active" : "Inactive"
        );

        // Prometheus metrics
        Metrics.CreateGauge($"network_bytes_sent_{SanitizeLabel(networkInterface.Description)}", "Network Bytes Sent").Set(stats.BytesSent);
        Metrics.CreateGauge($"network_bytes_received_{SanitizeLabel(networkInterface.Description)}", "Network Bytes Received").Set(stats.BytesReceived);
        Metrics.CreateGauge($"network_interface_status_{SanitizeLabel(networkInterface.Description)}", "Network Interface Status (1 = Active, 0 = Inactive)").Set(isActive ? 1 : 0);

        i++;
            }

            return networkDatas;
        }

        // Method to get disk usage
        public static DiskInfo[] GetDiskUsage()
{
    DriveInfo[] drives = DriveInfo.GetDrives();
    List<DiskInfo> diskInfos = new List<DiskInfo>();

    foreach (var drive in drives)
    {
        if (drive.IsReady) // Check if the drive is ready
        {
            // Calculate disk usage
            float freeSpaceMB = drive.TotalFreeSpace / (1024f * 1024f); // Convert to GB
            float usedSpaceMB = (drive.TotalSize - drive.TotalFreeSpace) / (1024f * 1024f); // Convert to GB
            float totalSizeMB = drive.TotalSize / (1024f * 1024f); // Convert to GB

            // Create DiskInfo object
            diskInfos.Add(new DiskInfo(
                drive.Name,
                freeSpaceMB,
                usedSpaceMB,
                totalSizeMB
            ));

            // Create a gauge for used storage
            string sanitizedDriveName = drive.Name.Replace(":", "").Replace("\\", "_").Replace("/", "_").Replace(" ", "_").Replace(".", "_");
            Metrics.CreateGauge("drive" + sanitizedDriveName + "_used_storage", "Drive Used Storage").Set(usedSpaceMB);
            Metrics.CreateGauge("drive" + sanitizedDriveName + "_total_storage", "Drive Total Storage").Set(totalSizeMB);
        }
    }
    
    return diskInfos.ToArray(); // Return as an array
}


        // Helper method to sanitize metric labels for Prometheus
        private static string SanitizeLabel(string label)
        {
            return Regex.Replace(label, @"[^a-zA-Z0-9_]", "_");
        }
    
    // Helper method to parse memory values from the line
        private static float ParseMemory(string line)
            {
                // Use regex to extract the memory value
                var match = Regex.Match(line, @"\d+");
                return match.Success ? float.Parse(match.Value) : 0;
            }
    }
}


