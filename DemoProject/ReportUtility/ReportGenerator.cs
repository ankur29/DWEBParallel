using DemoProject.Library;
using NUnit.Framework;
using RelevantCodes.ExtentReports;
using System;
using System.Collections.Generic;
using System.IO;
using excel = Microsoft.Office.Interop.Excel;

namespace DemoProject.ReportUtility
{
   [SetUpFixture]
    public class ReportGenerator
    {
        public List<string> passedTestCases = new List<string>();
        public List<string> totalTestCases;
      public  static excel.Application xlApp= new excel.Application();
        public static excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"E:\TestData.xlsx");
        public static excel.Worksheet x1Worksheet = xlWorkbook.Sheets[1];
        public static excel.Range xlRange = x1Worksheet.UsedRange;
        // ReadTestData readTestData = new ReadTestData();


        [OneTimeTearDown]
        public void endTest() // This method will be fired at the end of the test
        {
            try
            {
                Console.WriteLine("passedTestCases.Count="+passedTestCases.Count);
                ReadTestData.writeTestData(passedTestCases, totalTestCases);
                System.Threading.Thread.Sleep(5000);
                //    xlApp.Interactive = false;
                //     driver.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception=" + e);

            }
        }
        //  CreateReportPath reportPath = new CreateReportPath();

        public static ExtentReports report = new ExtentReports(CreateReportPath.dynamicPath() + "\\Report.html", false);
        //create html report in bin/debug/reports
        public ExtentReports createReport()
        {
            ExtentReports report;
            //report = new ExtentReports(CreateReportPath.dynamicPath() + "\\"+ browserName + "Report.html", false);
            //       report = new ExtentReports(reportPath.dynamicPath() + "\\Report.html", true);
                report = new ExtentReports("E:/Report.html", false);
        //    report = new ExtentReports(file, false);
            return report;
        }



        private byte[] ReadAllBytes2(string filePath, FileAccess fileAccess = FileAccess.ReadWrite, FileShare shareMode = FileShare.ReadWrite)
        {
            using (var fs = new FileStream(filePath, FileMode.Open, fileAccess, shareMode))
            {
                using (var ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    return ms.ToArray();
                }
            }
        }
    }
}
    
