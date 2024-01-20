using lanius.Measurements;

namespace lanius
{
    public interface IDataStorageProvider
    {
        public void Flush(in IEnumerable<Measurement> measurements);
    }
}
