using lanius.Measurements;
using System.Text;

namespace lanius
{
    public class TextFileProvider(string path) : IDataStorageProvider
    {
        protected readonly string _file = path;

        public void Flush(IEnumerable<Measurement> measurements)
        {
            StringBuilder result = new();

            foreach (Measurement measurement in measurements)
                result.AppendLine(measurement.ToString());

            using (var fs = new FileStream(_file, FileMode.Append, access: FileAccess.Write, share: FileShare.None))
            using (var file = new StreamWriter(fs))
            {
                file.Write(result);
                file.WriteLine();
            }
        }
    }
}
