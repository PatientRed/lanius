namespace lanius
{
    internal interface IDataStorageProvider
    {
        public void Flush(in IEnumerable<Measurement> measurements);
    }
}
