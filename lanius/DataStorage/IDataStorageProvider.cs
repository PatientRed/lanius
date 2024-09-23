using lanius.Measurements;

namespace lanius
{
    public interface IDataStorageProvider
    {
        public void Flush(IEnumerable<Measurement> measurements);
    }
}
