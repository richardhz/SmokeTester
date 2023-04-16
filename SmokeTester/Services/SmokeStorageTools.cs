using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using System.IO;
using System.Text.Json;

namespace SmokeTester.Services;
public class SmokeStorageTools : ISmokeStorageTools
{
    public async Task SaveToFileAsync<TItem>(TItem data, string filename, bool isNew = false ) 
    {
        var json = JsonSerializer.Serialize(data);
        var path = Path.Combine(FileSystem.AppDataDirectory, filename);
        await File.WriteAllTextAsync(path, json);
        if (!isNew)
        {
            await App.Current.MainPage.DisplayAlert("Saved", $"Saved {filename}", "OK");
        }
        
    }

    public async Task<TItem> LoadFromFileAsync<TItem>(string filename) 
    {
        var path = Path.Combine(FileSystem.AppDataDirectory, filename);
        if (!File.Exists(path))
        {
            var newdata = new List<TItem>();
            await SaveToFileAsync(newdata, filename, true);
        }
        var json = await File.ReadAllTextAsync(path);
        var data = JsonSerializer.Deserialize<TItem>(json);
        return data;
    }



}
