using System.Collections.Generic;
using UnityEngine;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Linq;

public static class CSVLoader
{
    private static readonly Dictionary<string, object> cache = new();

    public static List<T> LoadCSV<T>(string fileName)
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
        var list = LoadCSV<T>(fileName);

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
}
