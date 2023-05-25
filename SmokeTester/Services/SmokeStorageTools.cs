using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using System.IO;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

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


    public  string DisplayJson<TItem>(TItem data)
    {
        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        return json;
    }

    public async Task ExportFile<TItem>(TItem data, string fileName)
    {
        var personalFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        string targetFile = System.IO.Path.Combine(personalFolder, fileName);
        using FileStream outputStream = System.IO.File.OpenWrite(targetFile);
        using StreamWriter streamWriter = new StreamWriter(outputStream);
        await streamWriter.WriteAsync(json);
    }

    public async Task ImportFile<TItem>(string fileName)
    {
        var personalFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        string targetFile = System.IO.Path.Combine(personalFolder, fileName);
        using FileStream InputStream = System.IO.File.OpenRead(targetFile);
        using StreamReader reader = new StreamReader(InputStream);
        var json = await reader.ReadToEndAsync();
        var data = JsonSerializer.Deserialize<TItem>(json);
        await SaveToFileAsync(data, fileName);
    }


}
