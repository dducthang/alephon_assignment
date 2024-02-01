using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlephonAssignment.Logger
{
    public class FileIO
    {
        public bool WriteToFile(string filePath, string content)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                using (var fs = File.Create(filePath))
                {
                    byte[] strNums = new UTF8Encoding(true).GetBytes(content.ToString());
                    fs.Write(strNums, 0, strNums.Length);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public double[] ReadNumbersFromFile(string filePath, char delimiter)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return Array.Empty<double>();
                }

                var str = new StringBuilder();
                using (StreamReader sr = File.OpenText(filePath))
                {
                    var s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        str.Append(s);
                    }
                }

                var numberStrs = str.ToString().Split(delimiter);
                var numberList = new double[numberStrs.Length];

                for (var i = 0; i < numberStrs.Length; i++)
                {
                    if (double.TryParse(numberStrs[i], out double number))
                    {
                        numberList[i] = number;
                    }
                }
                return numberList;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Array.Empty<double>();
            }
        }

        public bool WriteToFileBin(string filePath, string content)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                using (BinaryWriter binWriter = new BinaryWriter(File.Open(filePath, FileMode.Create)))
                {
                    binWriter.Write(content);
                }
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public double[] ReadNumbersFromFileBin(string filePath, char delimiter)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return Array.Empty<double>();
                }

                byte[] binaryData;
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    binaryData = new byte[fileStream.Length];
                    fileStream.Read(binaryData, 0, (int)fileStream.Length);
                }

                var str = Encoding.UTF8.GetString(binaryData);

                var numberStrs = str.Split(delimiter);
                var numberList = new double[numberStrs.Length];    

                for(var i=0;i<numberStrs.Length;i++)
                {
                    if (double.TryParse(numberStrs[i], out double number))
                    {
                        numberList[i] = number;
                    }
                }            
                return numberList;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Array.Empty<double>();
            }
        }
    }
}
