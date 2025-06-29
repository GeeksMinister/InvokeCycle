namespace InvokeCycle.CoreUtils.Extensions;

public static class JSRuntimeFunc
{
    //Local Storage
    public static async Task LocalStorage_Clear(this IJSRuntime JS)
        => await JS.InvokeVoidAsync("localStorage.clear");
    public static async Task LocalStorage_RemoveItem(this IJSRuntime JS, string key)
        => await JS.InvokeVoidAsync("localStorage.removeItem", key);
    public static async Task LocalStorage_SetItem(this IJSRuntime JS, string key, object value)
        => await JS.InvokeVoidAsync("localStorage.setItem", key, value);
    public static async Task LocalStorage_SetSerializedItem<T>(this IJSRuntime JS, string key, T value)
        => await JS.InvokeVoidAsync("localStorage.setItem", key, JsonSerializer.Serialize(value));
    public static async ValueTask<T> LocalStorage_GetItem<T>(this IJSRuntime JS, string key)
        => await JS.InvokeAsync<T>("localStorage.getItem", key);
    public static async ValueTask<T?> LocalStorage_GetDeserializedItem<T>(this IJSRuntime JS, string key)
    {
        try
        {
            string? item = await JS.InvokeAsync<string?>("localStorage.getItem", key);
            if (item == null) return default;
            return JsonSerializer.Deserialize<T>(item);
        }
        catch (JsonException)
        {
            return default;
        }
    }
}


