namespace Storage
{
    public class StorageManager
    {
        private StorageAdaptor _storageAdaptor;

        #region UNITY METHODS

        public StorageManager()
        {
            Initializer();
        }

        #endregion

        #region PRIVATE METHODS

        private void Initializer()
        {
            _storageAdaptor ??= new StorageAdaptor();
        }

        #endregion

        #region PUBLIC METHODS

        public void SaveData<T>(string key, T data)
        {
            _storageAdaptor.SaveData(key, data);
        }

        public T GetData<T>(string key)
        {
            T data = _storageAdaptor.GetData<T>(key);
            return data;
        }

        #endregion
    }
}