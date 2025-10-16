using System.Collections.Generic;
using UnityEngine;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Linq;

public static class UnitLoaderCSV
{
    private static List<UnitData> units;
    
    private static void LoadUnits()
    {
        TextAsset csvText = Resources.Load<TextAsset>("UnitData");
        if (csvText == null)
        {
            units = new List<UnitData>();
            return;
        }

        using (var reader = new StringReader(csvText.text))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
               {
                   Delimiter = ",",
                   TrimOptions = TrimOptions.Trim
               }))
        {
            units = csv.GetRecords<UnitData>().ToList();
        }
    }
    
    public static UnitData GetUnitByName(string unitName)
    {
        if (units == null)
        {
            LoadUnits();
        }
        
        var unit = units.FirstOrDefault(u => 
            u.Name.Equals(unitName, System.StringComparison.OrdinalIgnoreCase));
        
        return unit;
    }
}