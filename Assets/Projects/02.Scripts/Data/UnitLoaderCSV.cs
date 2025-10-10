using System.Collections.Generic;
using UnityEngine;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Linq;

public static class UnitLoaderCSV
{
    private static List<UnitDataCSV> _units;
    
    public static List<UnitDataCSV> Units
    {
        get
        {
            if (_units == null)
            {
                LoadUnits();
            }
            return _units;
        }
    }

    private static void LoadUnits()
    {
        TextAsset csvText = Resources.Load<TextAsset>("UnitData");
        if (csvText == null)
        {
            Debug.LogError("CSV file not found!");
            _units = new List<UnitDataCSV>();
            return;
        }

        using (var reader = new StringReader(csvText.text))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
               {
                   Delimiter = ",",
                   TrimOptions = TrimOptions.Trim
               }))
        {
            _units = csv.GetRecords<UnitDataCSV>().ToList();
        }
    }
}