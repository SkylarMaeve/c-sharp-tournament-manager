using System.Collections.ObjectModel;
using PV178_Project.Models.Abstracts;

namespace PV178_Project.Services;

public class BaseManagerModel<T>()
{
    private ObservableCollection<T> Data { get; set; } = new ObservableCollection<T>();
    public ObservableCollection<T> GetData() => Data;
    public void Add(T model) => Data.Add(model);

    public void Update(T newValuesModel, T updatedModel)
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
    }

    public void Remove(T model) => Data.Remove(model);
}