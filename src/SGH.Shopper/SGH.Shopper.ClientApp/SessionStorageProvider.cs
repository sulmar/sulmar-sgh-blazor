using Microsoft.JSInterop;
using System.ComponentModel;

namespace SGH.Shopper.ClientApp;

public class SessionStorageProvider : IStorageProvider
{
    private readonly IJSInProcessRuntime js;

    public SessionStorageProvider(IJSInProcessRuntime js)
    {
        this.js = js;
    }

    public string GetItem(string key)
    {
        return js.Invoke<string>("sessionStorage.getItem", key);
    }

    public T GetItem<T>(string key)
    {
        string value = GetItem(key);

        if (string.IsNullOrEmpty(value))
        {
            return default(T);
        }
        else
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));

            T result = (T)converter.ConvertFromString(value);

            return result;
        }
    }

    public void SetItem(string key, string value)
    {
        js.InvokeVoid("sessionStorage.setItem", key, value);
    }

    public void SetItem<T>(string key, T value)
    {
        SetItem(key, value.ToString());
    }
}
