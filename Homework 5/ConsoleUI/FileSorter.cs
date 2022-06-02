using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorLibrary;
using VectorLibrary.Utils;

namespace ConsoleUI
{
    /// <summary>
    /// <para> 
    /// EMBARRASSING MESS that DOESNT WORK 
    /// </para>
    /// 
    /// <para> 
    /// Divides the text file into billions of temporary intermediary files using quicksort algorith fashion 
    /// </para>
    /// 
    /// <para> 
    /// Creates a new txt file to sort each time recursion iteration starts
    /// and saves the results into two txt files and then merges them
    /// </para>
    /// 
    /// <para> 
    /// This way you can sort the file if you can only open the half of it at a time
    /// </para>
    /// </summary>
    public class FileSorter
    {
        public void SortFile(string path, char separator)
        {
            string sortingDir = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "sorting");
            System.IO.Directory.CreateDirectory(sortingDir);

            SortFile2(path, separator, sortingDir);

            // override the original file

            //System.IO.Directory.Delete(sortingDir, true);
        }
        private string SortFile2(string path, char separator, string sortingDir)
        {
            FileStream fs = File.OpenRead(path);
            if (fs.Length <= 2)
            {
                fs.Close();
                return path;
            }
            string firstHalf = SortFile3(fs, separator, fs.Length / 2, sortingDir);
            string firstHalfPath = Path.Combine(sortingDir, firstHalf);

            fs = File.OpenRead(path);
            fs.Position = fs.Length / 2;
            string secondHalf = SortFile3(fs, separator, fs.Length / 2, sortingDir);
            string secondHalfPath = Path.Combine(sortingDir, secondHalf);
            fs.Close();

            string sorted = Path.Combine(sortingDir, $"sorted_{Path.GetRandomFileName()}.txt");
            MergeFiles(firstHalfPath, secondHalfPath, sorted);

            return sorted;
        }
        private string SortFile3(FileStream fs, char separator, long fsLength, string sortingDir)
        {
            List<int> array = fsToList(fs, separator, fsLength);

            if (array.Count <= 1)
            {
                fs.Close();
                return CreateSortingFile(0, new List<int>(), sortingDir, separator, true);
            }

            int pivot;
            List<int> less;
            List<int> greater;
            QuickSort(array.ToList(), out less, out greater, out pivot);


            string greaterPath = CreateSortingFile(pivot, greater, sortingDir, separator, false);
            greater = null; // disposing memory

            greaterPath = Path.Combine(sortingDir, greaterPath);
            greaterPath = SortFile2(greaterPath, separator, sortingDir);

            string lessPath = CreateSortingFile(pivot, less, sortingDir, separator, true);
            less = null; // disposing memory

            lessPath = Path.Combine(sortingDir, lessPath);
            lessPath = SortFile2(lessPath, separator, sortingDir);

            // add pivot
            StreamWriter sw = File.AppendText(lessPath);
            sw.WriteLine($"{pivot},");
            sw.Close();

            // merge
            string mergedName = $"{Path.GetRandomFileName()}.txt";
            string mergedPath = Path.Combine(sortingDir, mergedName);
            MergeFiles(lessPath, greaterPath, mergedPath);

            return mergedPath;
        }
        private void MergeFiles(string pathFile1, string pathFile2, string pathResult)
        {
            string merged = File.ReadAllText(pathFile1) + File.ReadAllText(pathFile2);
            if(merged.Length > 2)
                merged = merged.Substring(0, merged.Length - 2);
            File.WriteAllText(pathResult, merged);

            //File.WriteAllLines(pathResult, (File.ReadAllText(pathFile1) + File.ReadAllText(pathFile2)).Split('\n'));
        }
        private List<int> fsToList(FileStream fs, char separator, long fsLength)
        {
            if (fsLength == 0)
            {
                fs.Close();
                return new List<int>();
            }

            byte[] fsArray = new byte[fsLength];
            for (int i = 0; i < fsLength; i++)
            {
                fsArray[i] = (byte)fs.ReadByte();
            }
            string str = Encoding.ASCII.GetString(fsArray);

            int trim;
            if (str.Length > 2)
            {
                if (str.EndsWith(separator))
                    trim = 1;
                else
                    trim = 2;
            }
            else
                trim = 0;

            string[] line = str.Remove(fsArray.Length - trim).Split(separator);
            line = line.Where(x => x != "").ToArray();
            List<int> array = Array.ConvertAll(line, int.Parse).ToList();

            fs.Close();
            return array;
        }

        private string CreateSortingFile(int pivot, List<int> list, string sortingDir, char separator, bool less)
        {
            string fileName;
            if(less)
                fileName = $"{pivot}l_{Path.GetRandomFileName()}.txt";
            else
                fileName = $"{pivot}g_{Path.GetRandomFileName()}.txt";


            string filePath = Path.Combine(sortingDir, fileName);

            StreamWriter outfs = new StreamWriter(filePath);
            foreach (var item in list)
            {
                outfs.Write(item.ToString());
                outfs.Write(separator);
            }
            outfs.Close();

            return fileName;
        }

        private void QuickSort(List<int> list, out List<int> less, out List<int> greater, out int pivot)
        {
            pivot = list[0];
            less = new List<int>();
            greater = new List<int>();

            for (int i = 1; i < list.Count; i++)
            {
                if (list[i] < pivot) 
                {
                    less.Add(list[i]);
                }
                else 
                {
                    greater.Add(list[i]);
                }
            }

        }
    }
}