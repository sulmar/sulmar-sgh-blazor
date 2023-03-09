namespace SGH.Shopper.ClientApp;

public interface IStorageProvider
{
    void SetItem(string key, string value);
    void SetItem<T>(string key, T value);
    string GetItem(string key);
    public T GetItem<T>(string key);

}
