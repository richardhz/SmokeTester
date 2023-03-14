namespace SmokeTester.Services;

public interface ISmokeStorageTools
{
    Task<TItem> LoadFromFileAsync<TItem>(string filename);
    Task SaveToFileAsync<TItem>(TItem data, string filename);
}