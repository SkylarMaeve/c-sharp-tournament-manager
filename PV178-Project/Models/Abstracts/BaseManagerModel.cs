using System.Collections.ObjectModel;

namespace PV178_Project.Services;

public class BaseManagerModel<T>(BaseService<T> service) where T : class
{
    private ObservableCollection<T> Data { get; set; } = new ObservableCollection<T>();
    private BaseService<T> BaseService { get; set; } = service;
    public ObservableCollection<T> GetAll() => Data;
    public async Task Add(T model, bool dbAccess = true)
    {
        if (dbAccess) //Needed for the way loading works
        {
            await BaseService.AddAsync(model);
        }
        Data.Add(model);
    }

    public async Task Update(T newValuesModel, T updatedModel)
    {
        var properties = typeof(T).GetProperties();
        foreach (var property in properties)
        {
            if (property.Name == "Id") continue;
            if (property.CanWrite)
            {
                var newValue = property.GetValue(newValuesModel);
                property.SetValue(updatedModel, newValue);
            }
        }
        await BaseService.UpdateAsync(updatedModel);
    }

    public async Task Remove(T model)
    {
        await BaseService.DeleteAsync(model);
        Data.Remove(model);
    }
}