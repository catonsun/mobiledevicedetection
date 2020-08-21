using FiftyOne.DeviceDetection;
using FiftyOne.Pipeline.Core.FlowElements;
using FiftyOne.Pipeline.Engines;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;

namespace MobileDeviceDetection.DeviceDetection
{
    public class FiftyOneDegreesMobileDeviceDetector
    {
        public static bool IsMobileDevice(
            string userAgent
            )
        {
            string resourceFileName = "51Degrees-LiteV4.1.hash";
            string resourceName = Assembly.GetExecutingAssembly().GetManifestResourceNames()
                .Where(n => n.Contains(resourceFileName))
                .FirstOrDefault();
            var assembly = Assembly.GetExecutingAssembly();
            var resourceStream = assembly.GetManifestResourceStream(resourceName);
            var fileInfo = new FileInfo(resourceName);

            // Testing for directory; disregard
            //var asdf = Assembly.GetTypes();//GetAssembly(typeof(FiftyOneDegreesMobileDeviceDetector)).Name;
            //using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            //{
            //    return await reader.ReadToEndAsync();
            //}
            //var folder1 = Assembly.GetExecutingAssembly();
            //string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //string folder2 = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
            //string folder3 = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            //var myType = typeof(FiftyOneDegreesMobileDeviceDetector);
            //var n = myType.Namespace;
            //string baseDirectory = AppContext.BaseDirectory;
            //string AssemblyPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
            //var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
            //while (directory != null && !directory.GetFiles("*.sln").Any())
            //{
            //    directory = directory.Parent;
            //}
            //var a = directory != null;

            var pipelineInstance = new DeviceDetectionPipelineBuilder()
                .UseOnPremise(resourceName, null, true)
                .SetPerformanceProfile(PerformanceProfiles.LowMemory)
                .UseResultsCache()
                .Build();

            // Create a new FlowData instance ready to be populated with evidence for the
            // Pipeline.
            var data = pipelineInstance.CreateFlowData();

            // Add a User-Agent string to the evidence collection.
            data.AddEvidence(FiftyOne.Pipeline.Core.Constants.EVIDENCE_QUERY_USERAGENT_KEY, userAgent);

            // Process the supplied evidence.
            data.Process();

            // Get device data from the flow data.
            var device = data.Get<IDeviceData>();

            return device.IsMobile.HasValue ? device.IsMobile.Value : false;
        }

        public static string GetFiftyOneFileName()
        {
            var fileInfo = new FileInfo("51Degrees-LiteV4.1.hash");
            return fileInfo.FullName;
        }

        private FiftyOneDegreesMobileDeviceDetector()
        {
            // Empty
        }

        //private static readonly IPipeline pipelineInstance = new DeviceDetectionPipelineBuilder()
        //    .UseOnPremise(fileInfo.FullName, null, true)
        //    .SetPerformanceProfile(PerformanceProfiles.LowMemory)
        //    .UseResultsCache()
        //    .Build();
    }
}
