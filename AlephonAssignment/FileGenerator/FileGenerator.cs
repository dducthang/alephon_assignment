using System;
using System.IO;
using System.Reflection.Metadata;
using System.Text;

namespace AlephonAssignment.FileGenerator
{
    public class FileGenerator
    {
        const string Path = "./Files";
        private string GenerateStringOfNumbers(int size, double min, double max)
        {
            var str = new StringBuilder();

            var random = new Random();

            for (var i = 0; i < size; i++)
            {
                double randomValue = Math.Round(random.NextDouble() * (max - min) + min, 2);
                str.Append(string.Format("{0},", randomValue.ToString()));
            }

            return str.ToString();  
        }

        private bool WriteToFile(string filePath, string content)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                using (FileStream fs = File.Create(filePath))
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

        private List<double> ReadNumbersFromFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return new List<double>();
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

                var numbersStr = str.ToString().Split(',');

                var numberList = new List<double>();
                foreach (var n in numbersStr)
                {
                    if (double.TryParse(n, out double number))
                    {
                        numberList.Add(number);
                    }
                }

                return numberList;
            }
            catch(Exception ex)
            {
                return new List<double>();
            }
        }

        static void HeapSortAlgorithm(List<double> list)
        {
            int n = list.Count;

            // Build max heap
            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(list, n, i);

            // Extract elements from the heap one by one
            for (int i = n - 1; i > 0; i--)
            {
                // Move the current root to the end
                Swap(list, 0, i);

                // Call max heapify on the reduced heap
                Heapify(list, i, 0);
            }
        }

        static void Heapify(List<double> list, int n, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < n && list[left] > list[largest])
                largest = left;

            if (right < n && list[right] > list[largest])
                largest = right;

            if (largest != i)
            {
                Swap(list, i, largest);
                Heapify(list, n, largest);
            }
        }

        static void Swap(List<double> list, int a, int b)
        {
            double temp = list[a];
            list[a] = list[b];
            list[b] = temp;
        }
        public bool GenerateFile(string fileName, int size, double min, double max)
        {
            try
            {
                string filePath = string.Format(@"D:\Dev\Source\AlephonAssignment\AlephonAssignment\Files\{0}.txt", fileName);

                var str = GenerateStringOfNumbers(size, min, max);

                var result = WriteToFile(filePath, str);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool SortFile(string fileName)
        {

            try
            {
                string filePath = string.Format(@"D:\Dev\Source\AlephonAssignment\AlephonAssignment\Files\{0}.txt", fileName);

                if (!File.Exists(filePath))
                {
                    return false;
                }

                var numberList = ReadNumbersFromFile(filePath);
                HeapSortAlgorithm(numberList);

                var str = new StringBuilder();
                foreach(var n in numberList)
                {
                    str.Append(string.Format("{0},",n.ToString()));
                }
                WriteToFile(filePath, str.ToString());

                return true;
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
                return false;
            }

        }
    }
}
