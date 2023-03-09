using Microsoft.JSInterop;
using System.ComponentModel;

namespace SGH.Shopper.ClientApp;

public interface IStorageProvider
{
    void SetItem(string key, string value);
    void SetItem<T>(string key, T value);
    string GetItem(string key);
    public T GetItem<T>(string key);

}

// na podst. https://developer.mozilla.org/en-US/docs/Web/API/Window/localStorage

// Mozna skorzystać z gotowej biblioteki:
// https://github.com/Blazored/LocalStorage/tree/main/src/Blazored.LocalStorage
public class LocalStorageProvider : IStorageProvider
{
    private readonly IJSInProcessRuntime js;

    public LocalStorageProvider(IJSInProcessRuntime js)
    {
        this.js = js;
    }

    public void SetItem(string key, string value) {
        js.InvokeVoid("localStorage.setItem", key, value);
    }

    public void SetItem<T>(string key, T value)
    {
        SetItem(key, value.ToString());
    }

    public string GetItem(string key)
    {
        return js.Invoke<string>("localStorage.getItem", key);
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
            
            T result = (T) converter.ConvertFromString(value);

            return result;
        }
        
    }
}
