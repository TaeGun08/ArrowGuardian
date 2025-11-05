using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using UnityEngine;

public static class EnemyLoaderCSV
{
    private static List<EnemyData> enemys;
    
    private static void LoadUnits()
    {
        TextAsset csvText = Resources.Load<TextAsset>("EnemyData");
        if (csvText == null)
        {
            enemys = new List<EnemyData>();
            return;
        }

        using (var reader = new StringReader(csvText.text))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
               {
                   Delimiter = ",",
                   TrimOptions = TrimOptions.Trim
               }))
        {
            enemys = csv.GetRecords<EnemyData>().ToList();
        }
    }
    
    public static EnemyData GetEnemyByElementType(ElementType elementType)
    {
        if (enemys == null)
        {
            LoadUnits();
        }
        
        var unit = enemys.FirstOrDefault(u => 
            u.ElementType.Equals(elementType));
        
        return unit;
    }
}
