using AlephonAssignment.Helpers;
using AlephonAssignment.Logger;
using System;
using System.Text;

namespace AlephonAssignment.FileGenerator
{
    public class FileGenerator
    {
        private const string _path = @"D:\{0}.txt";
        private const string _binPath = @"D:\{0}.bin";
        private const char _delimiter = ',';
        private string _numStringFormat = $"{{0}}{_delimiter}";

        #region part one
        private string GenerateStringNumbersFast(int size, double min, double max, string format)
        {
            var str = new StringBuilder();
            var random = new Random();
            Parallel.For(0, size, i =>
            {
                double randomValue = Math.Round(random.NextDouble() * (max - min) + min, 2);
                str.Append(string.Format(format, randomValue.ToString()));
            });

            string result = new string(str.ToString().Where(c => c != '\0').ToArray());

            return result;
        }

        private string GenerateStringNumbersSlow(int size, double min, double max, string format)
        {
            var str = new StringBuilder();
            var random = new Random();
            for (var i = 0; i < size; i++)
            {
                double randomValue = Math.Round(random.NextDouble() * (max - min) + min, 2);
                str.Append(string.Format(format,randomValue.ToString()));
            }

            return str.ToString();
        }

        private List<double> GenerateNumbers(int size, double min, double max)
        {
            var numList = new List<double>();
            var random = new Random();
            for (var i = 0; i < size; i++)
            {
                double randomValue = Math.Round(random.NextDouble() * (max - min) + min, 2);
                numList.Add(randomValue);
            }

            return numList;  
        }

        public bool GenerateFileFast(string fileName, int size, double min, double max)
        {
            try
            {
                string filePath = string.Format(_path, fileName);
                var fileIO = new FileIO();

                var numStr = GenerateStringNumbersFast(size, min, max, _numStringFormat);

                var result = fileIO.WriteToFile(filePath, numStr);

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
                string filePath = string.Format(_path, fileName);

                var sortHelper = new SortDouble();
                var fileIO = new FileIO();

                var numberList = fileIO.ReadNumbersFromFile(filePath, _delimiter);
                if (!numberList.Any())
                {
                    return false;
                }

                sortHelper.HeapSort(numberList);

                var str = numberList.ToString(_numStringFormat);

                var result = fileIO.WriteToFile(filePath, str);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool ShowFile(string fileName)
        {
            try
            {
                var filePath = string.Format(_path, fileName);

                var fileIO = new FileIO();
                var numberList = fileIO.ReadNumbersFromFile(filePath, _delimiter);

                foreach(var n in numberList)
                {
                    Console.WriteLine(n);
                }

                return true;
            }
            catch(Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
        }

        public bool MergeFile(string fileA, string fileB, string resultFile)
        {
            try
            {
                var fileAPath = string.Format(_path, fileA);
                var fileBPath = string.Format(_path, fileB);
                var resultFilePath = string.Format(_path, resultFile);

                var fileIO = new FileIO();

                var numberListA = fileIO.ReadNumbersFromFile(fileAPath, _delimiter);
                var numberListB = fileIO.ReadNumbersFromFile(fileBPath, _delimiter);

                if (!numberListA.Any() && !numberListB.Any()) return false;

                double[] mergedList = new double[numberListA.Length + numberListB.Length];

                Array.Copy(numberListA, 0, mergedList, 0, numberListA.Length);
                Array.Copy(numberListB, 0, mergedList, numberListA.Length, numberListB.Length);

                var sortHelper = new SortDouble();
                sortHelper.HeapSort(numberListA);

                var str = mergedList.ToString(_numStringFormat);

                var result = fileIO.WriteToFile(resultFilePath, str);

                return result;
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
        }

        #endregion

        #region bonus question

        public bool GenerateFileBin(string fileName, int size, double min, double max)
        {
            try
            {
                string filePath = string.Format(_binPath, fileName);
                var fileIO = new FileIO();

                var numStr = GenerateStringNumbersFast(size, min, max, _numStringFormat);

                var result = fileIO.WriteToFileBin(filePath, numStr);

                return result;
            }
            catch(Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
        }

        public bool ShowFileBin(string fileName)
        {
            try
            {
                var filePath = string.Format(_binPath, fileName);
                var fileIO = new FileIO();

                var numberList = fileIO.ReadNumbersFromFileBin(filePath, _delimiter);

                foreach ( var number in numberList)
                {
                    Console.WriteLine(number);
                }

                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public void GenerateAndSaveRandomNumbers(string fileName, long size, double min, double max)
        {
            var filePath = string.Format(_binPath, fileName);
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            using (BinaryWriter writer = new BinaryWriter(fileStream))
            {
                Random random = new Random();
                for (var i = 0; i < size; i++)
                {
                    double randomValue = Math.Round(random.NextDouble() * (max - min) + min, 2);
                    writer.Write(randomValue);
                }
            }
        }

        public void ReadBinFile(string fileName)
        {
            var filePath = string.Format(_path, fileName);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            using (BinaryReader reader = new BinaryReader(fileStream))
            {
                while (fileStream.Position < fileStream.Length)
                {
                    double randomValue = reader.ReadDouble();
                    Console.WriteLine(randomValue);
                }
            }
        }

        #endregion
    }
}
