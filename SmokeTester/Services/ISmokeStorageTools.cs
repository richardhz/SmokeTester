namespace SmokeTester.Services;

public interface ISmokeStorageTools
{
    Task<TItem> LoadFromFileAsync<TItem>(string filename);
    Task SaveToFileAsync<TItem>(TItem data, string filename, bool isNew = false);
    string DisplayJson<TItem>(TItem data);
    Task ExportFile<TItem>(TItem data, string fileName);
    Task ImportFile<TItem>(string fileName);
}