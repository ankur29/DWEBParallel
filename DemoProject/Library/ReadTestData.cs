using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Linq;
using DemoProject.ReportUtility;
using System.Diagnostics;

namespace DemoProject.Library
{
   public class ReadTestData
    {
        //excel.Application xlApp;
        //static excel.Workbook xlWorkbook;
        //static excel.Worksheet x1Worksheet;
        //excel.Range xlRange;
        public  List<string> keyCount;
        //public ReadTestData()
        //{
            //xlApp = new excel.Application();          
            ////     xlApp.Visible = false;
            ////xlApp.ScreenUpdating = false;
            //xlWorkbook = xlApp.Workbooks.Open(@"E:\TestData.xlsx");
            //x1Worksheet = xlWorkbook.Sheets[1];
            //xlRange = x1Worksheet.UsedRange;
            //keyCount = new List<string>();
            ////xlApp.Calculation = excel.XlCalculation.xlCalculationManual;
            //xlApp.Application.Interactive = false;

   //     }
        //get Total row count
        public   int totalRowCount()
        {
            return  5;
        }

        //get Total Column Count
        public  int totalColumnCount()
        {
            return 6;
        }

      

        public  Dictionary<string, string>  readExcelData(String pageName)
        {
          excel.Application xlApp = new excel.Application();
            excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"E:\TestData.xlsx", false, true);
            excel.Worksheet x1Worksheet = xlWorkbook.Sheets[1];
            excel.Range xlRange = x1Worksheet.UsedRange;
            keyCount = new List<string>();

            //   xlApp.Application.Interactive = false;
            //load excel
            //excel.Application xlApp = new excel.Application();
            //excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\temp\TestData.xlsx");
            //excel.Worksheet x1Worksheet = xlWorkbook.Sheets[1];
            //excel.Range xlRange = x1Worksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            var map = new Dictionary<string, string>();
            for (int i = 2; i <= rowCount; i++)
            {
                String executionStatus = xlRange.Cells[i, 4].Value2.ToString();
                Console.WriteLine("Execution status=" + xlRange.Cells[i, 4].Value2.ToString());
                
                String methodName = xlRange.Cells[i, 3].Value2.ToString();

                if (executionStatus.Equals("Y") && methodName.Equals(pageName))
                {
                    for (int j = 2; j <= colCount; j++)
                {
                        Console.WriteLine("Add values to Map");
                        //Console.WriteLine(xlRange.Cells[1, j].Value2.ToString() + "_" + i);
                        //Console.WriteLine(xlRange.Cells[i, j].Value2.ToString());
                     map.Add(xlRange.Cells[1, j].Value2.ToString()+"_"+i, xlRange.Cells[i, j].Value2.ToString());
                        if (!keyCount.Contains(i.ToString()))
                        {
                            keyCount.Add(i.ToString());
                        }
                    }

                }
            }
            Console.WriteLine(map.Count);
            Console.WriteLine(keyCount[0]);

            if (xlWorkbook != null)
            {
                xlWorkbook.Close(false, Type.Missing, Type.Missing);
                xlApp.Workbooks.Close();
                Marshal.ReleaseComObject(xlWorkbook);
            }


            xlApp.Quit();
            GC.Collect();
            Marshal.FinalReleaseComObject(xlApp);


           

            //xlApp.Quit();
            //xlApp.Quit();
            //xlWorkbook.Save();
            //xlWorkbook.Close();

            return map;         
           
        }

        //public static void closeWorkbook()
        //{
        //   xlWorkbook.Save();
        //    xlWorkbook.Close();
        //}

        public static void writeTestData(List<string> passedTestCases, List<string> totalTestCases)
        {
            Process[] AllProcesses = Process.GetProcessesByName("Excel");

            foreach (Process ExcelProcess in AllProcesses)
            {
                ExcelProcess.Kill();

                Process[] AllProcesses1 = Process.GetProcessesByName("Excel");
                if (AllProcesses1.Length < 1)
                {
                    break;
                }

            }

            excel.Application xlApp = new excel.Application();
            excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"E:\TestData.xlsx");
            excel.Worksheet x1Worksheet = xlWorkbook.Sheets[1];
                xlApp.Application.Interactive = false;
            List<string> distinctItems = passedTestCases.Distinct().ToList();

            var firstNotSecond = totalTestCases.Except(distinctItems).ToList();


            for (int i = 0; i < firstNotSecond.Count; i++)
            {
                int rowNumber = Convert.ToInt32(firstNotSecond[i]);
                Console.WriteLine("rowNumber="+ rowNumber);
                x1Worksheet.Cells[rowNumber, 4] = "N";
         //   x1Worksheet.Cells[4, 4] = "N";
            }
            Console.WriteLine("Write is called");
            xlWorkbook.Save();
            xlWorkbook.Close();

        }
    }

    }

