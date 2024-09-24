using lanius.Measurements;

namespace lanius.DataStorage
{
    public interface IDataStorageProvider
    {
        public void Flush(IEnumerable<Measurement> measurements);
    }
}
