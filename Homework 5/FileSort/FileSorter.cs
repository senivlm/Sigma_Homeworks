using System.Text;

namespace FileSort
{
    public static class FileSorter
    {
        private static readonly string tempPath = Path.Combine(Environment.CurrentDirectory, "temp.txt");

        public static void SortFile(string originalPath, uint n, char separator) 
        {
            // 1. sort n parts of the file by opening them one by one and then rewriting original file 

            // open stream
            FileStream fsReadWrite = new FileStream(originalPath, FileMode.Open, FileAccess.ReadWrite);
            SortParts(fsReadWrite, n, separator);
            fsReadWrite.Close();

            // create temp file
            if (!File.Exists(tempPath))
                File.Create(tempPath);


            FileStream fsOrig = null;
            FileStream fsTemp = null;
            try
            {
                // 2. sort sorted parts using temp file and rewriting the original file
                fsOrig = new FileStream(originalPath, FileMode.Open, FileAccess.ReadWrite);
                fsTemp = new FileStream(tempPath, FileMode.Open, FileAccess.ReadWrite);

                //MergeAll(fsOrig, fsTemp, n, separator);
                Merge(fsOrig, fsTemp, n, separator);
            }
            catch (Exception)
            {
            }
            finally
            {
                if(fsOrig != null)
                    fsOrig.Close();
                if(fsTemp != null)
                    fsTemp.Close();

                File.Replace(tempPath, originalPath, null);

                // delete temp file
                if (File.Exists(tempPath))
                    File.Delete(tempPath);
            }
        }

        private static void Merge(FileStream fsOrig, FileStream fsTemp, uint n, char separator)
        {
            long l = 0; // left part position
            long r = fsOrig.Length/2; // right part pisition

            while (l < fsOrig.Length/2 && r < fsOrig.Length)
            {
                fsOrig.Position = l;
                int left = GetNextNumber(fsOrig, separator);

                fsOrig.Position = r;
                int right = GetNextNumber(fsOrig, separator);

                if (left < right) 
                {
                    fsTemp.WriteByte((Encoding.ASCII.GetBytes(left.ToString())[0]));
                    fsTemp.WriteByte((byte)separator);
                    l += 2;
                }
                else 
                {
                    fsTemp.WriteByte((Encoding.ASCII.GetBytes(right.ToString())[0]));
                    fsTemp.WriteByte((byte)separator);
                    r += 2;
                }
            }
            while (l < fsOrig.Length / 2)
            {
                fsOrig.Position = l;
                int left = GetNextNumber(fsOrig, separator);

                fsTemp.WriteByte((Encoding.ASCII.GetBytes(left.ToString())[0]));
                fsTemp.WriteByte((byte)separator);
                l += 2;
            }
            while (r < fsOrig.Length)
            {
                fsOrig.Position = r;
                int right = GetNextNumber(fsOrig, separator);

                fsTemp.WriteByte((Encoding.ASCII.GetBytes(right.ToString())[0]));
                fsTemp.WriteByte((byte)separator);
                r += 2;
            }
        }

        private static int GetNextNumber(FileStream fs, char separator)
        {
            int result = 0;

            byte byteChar;
            do
            {
                byteChar = (byte)fs.ReadByte();
            } while (byteChar == 255 || byteChar == separator);

            result = int.Parse(Encoding.ASCII.GetString(new byte[] { byteChar }));

            return result;
        }
        private static void SortParts(FileStream fs, uint n, char separator)
        {
            // for n
            // sort filestream parts
            // rewrite the part 

            long partLength = fs.Length / n;
            for (int i = 0; i < n; i++)
            {
                long partStartPosition = fs.Position;
                // sort filestream part

                var fsArrayRead = new List<byte>();

                // get byte array
                for (int j = 0; j < partLength; j++)
                {
                    byte byteChar = (byte)fs.ReadByte();
                    if(byteChar != 255)
                        fsArrayRead.Add(byteChar);
                }

                // transform byte array into int array
                string strRead = Encoding.ASCII.GetString(fsArrayRead.ToArray());
                if(strRead[strRead.Length - 1] == separator)
                    strRead = strRead.Remove(strRead.Length-1, 1);

                string[] split = strRead.Split(separator);
                List<int> sorted = new List<int>();
                foreach (var item in split)
                {
                    sorted.Add(int.Parse(item));
                }
                
                sorted.Sort();

                // convert back to byte array
                StringBuilder strWrite = new StringBuilder();
                foreach (var item in sorted)
                {
                    strWrite.Append(item.ToString() + ",");
                }

                var fsArrayWrite = Encoding.ASCII.GetBytes(strWrite.ToString());

                // rewrite this part back into the original file
                long partEndPosition = fs.Position;
                fs.Position = partStartPosition;
                for (long j = partStartPosition, k = 0; j < partEndPosition; j++, k++)
                {
                    fs.WriteByte(fsArrayWrite[k]);
                }

                fs.Position = partEndPosition;
            }
        }
    }
}
