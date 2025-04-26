using System.Collections.ObjectModel;
using PV178_Project.Models.Abstracts;

namespace PV178_Project.Services;

public class BaseManagerModel<T>()
{
    private static ObservableCollection<T> Data { get; set; } = new ObservableCollection<T>();
    public static ObservableCollection<T> GetData() => Data;
    public static void Add(T model) => Data.Add(model);
    public static void Remove(T model) => Data.Remove(model);
}