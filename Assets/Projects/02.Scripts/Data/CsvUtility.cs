using System.Collections.Generic;
using UnityEngine;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Linq;

public static class CsvUtility
{
    private static readonly Dictionary<string, object> cache = new();

    public static List<T> LoadCsv<T>(string fileName)
    {
        if (cache.TryGetValue(fileName, out var cached))
            return cached as List<T>;

        TextAsset csvText = Resources.Load<TextAsset>(fileName);
        if (csvText == null)
            return new List<T>();

        using (var reader = new StringReader(csvText.text))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            var list = csv.GetRecords<T>().ToList();
            cache[fileName] = list;
            return list;
        }
    }

    public static T LoadById<T>(string fileName, int id)
    {
        var list = LoadCsv<T>(fileName);

        var prop = typeof(T).GetProperty("Id");
        if (prop == null)
        {
            return default;
        }

        return list.FirstOrDefault(item =>
        {
            var value = prop.GetValue(item);
            return value != null && (int)value == id;
        });
    }
    
    public static void SaveCsv<T>(string fileName, List<T> data)
    {
#if UNITY_EDITOR
        SaveToResources<T>(fileName, data);
#else
        SaveToPersistent<T>(fileName, data);
#endif
    }

#if UNITY_EDITOR
    private static void SaveToResources<T>(string fileName, List<T> data)
    {
        string path = Path.Combine(Application.dataPath, "Resources", $"{fileName}.csv");

        using (var writer = new StreamWriter(path))
        using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            csv.WriteHeader<T>();
            csv.NextRecord();
            csv.WriteRecords(data);
        }

        UnityEditor.AssetDatabase.Refresh();
        Debug.Log($"CSV saved to Resources: {path}");
    }
#endif

    private static void SaveToPersistent<T>(string fileName, List<T> data)
    {
        string path = Path.Combine(Application.persistentDataPath, $"{fileName}.csv");

        using (var writer = new StreamWriter(path))
        using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            csv.WriteHeader<T>();
            csv.NextRecord();
            csv.WriteRecords(data);
        }

        Debug.Log($"CSV saved to persistentDataPath: {path}");
    }
}
