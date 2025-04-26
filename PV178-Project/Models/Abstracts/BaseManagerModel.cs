using System.Collections.ObjectModel;
using PV178_Project.Models.Abstracts;

namespace PV178_Project.Services;

public class BaseManagerModel<T>()
{
    private ObservableCollection<T> Data { get; set; } = new ObservableCollection<T>();
    public ObservableCollection<T> GetData() => Data;
    public void Add(T model) => Data.Add(model);
    public void Remove(T model) => Data.Remove(model);
}