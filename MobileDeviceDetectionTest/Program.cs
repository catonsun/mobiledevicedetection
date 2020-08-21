using System;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using MobileDeviceDetection.DeviceDetection;

namespace MobileDeviceDetectionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is a test for FiftyOne device detection.");

            var test = FiftyOneDegreesMobileDeviceDetector.IsMobileDevice(
                "Mozilla / 5.0(iPhone; CPU iPhone OS 11_2_6 like Mac OS X) AppleWebKit / 604.1.34(KHTML, like Gecko) GSA / 44.0.187102957 Mobile / 15D100 Safari / 604.1"
                );

            Console.WriteLine("Useragent is mobile: " + test);
        }
    }
}
