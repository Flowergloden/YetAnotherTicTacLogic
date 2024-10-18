using System.IO;
using System.Linq;
using OfficeOpenXml;
using UnityEditor;
using UnityEngine;

public class ConfigImporter
{
    private const string RootPath = "Assets/YATTL/Resources/Editor/Config/";

    [MenuItem("Tools/配置表重导入")]
    public static void Import()
    {
        ImportGameConfig();
        ImportMapConfig();
        ImportAIConfig();
    }

    private static void ImportGameConfig()
    {
        var fs = new FileStream(RootPath + "游戏配置.xlsx", FileMode.Open);
        var xlsx = new ExcelPackage(fs);

        var sheet = xlsx.Workbook.Worksheets[0];
        var so = AssetDatabase.FindAssets("t:GameConfigScriptableObject")
            .Select(AssetDatabase.GUIDToAssetPath)
            .Select(AssetDatabase.LoadAssetAtPath<GameConfigScriptableObject>)
            .First();

        so.bCircleFirst = bool.Parse(sheet.Cells["B2"].Value.ToString());
        so.bPlayerMoveFirst = bool.Parse(sheet.Cells["B3"].Value.ToString());
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        fs.Close();
    }

    private static void ImportMapConfig()
    {
        var fs = new FileStream(RootPath + "棋盘数据.xlsx", FileMode.Open);
        var xlsx = new ExcelPackage(fs);

        var sheet = xlsx.Workbook.Worksheets[0];
        var so = AssetDatabase.FindAssets("t:MapConfigScriptableObject")
            .Select(AssetDatabase.GUIDToAssetPath)
            .Select(AssetDatabase.LoadAssetAtPath<MapConfigScriptableObject>)
            .First();

        so.mapSize = int.Parse(sheet.Cells["B2"].Value.ToString());
        so.elementSize = float.Parse(sheet.Cells["B3"].Value.ToString());
        so.splitSize = float.Parse(sheet.Cells["B4"].Value.ToString());
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        fs.Close();
    }

    private static void ImportAIConfig()
    {
        var fs = new FileStream(RootPath + "AI配置.xlsx", FileMode.Open);
        var xlsx = new ExcelPackage(fs);

        var so = AssetDatabase.FindAssets("t:AIConfigScriptableObject")
            .Select(AssetDatabase.GUIDToAssetPath)
            .Select(AssetDatabase.LoadAssetAtPath<AIConfigScriptableObject>)
            .First();
        so.mctsConfig.Clear();
        so.minimaxConfig.Clear();

        for (var i = Difficulty.Begin + 1; i < Difficulty.End; i++)
        {
            var sheet = xlsx.Workbook.Worksheets[(int)i - 1];

            var bMCTS = bool.Parse(sheet.Cells["B2"].Value.ToString());

            if (bMCTS)
            {
                var data = new Constants.MCTSData
                {
                    difficulty = i,
                    maxDepth = int.Parse(sheet.Cells["B3"].Value.ToString()),
                    c = float.Parse(sheet.Cells["B10"].Value.ToString()),
                    gamesPerSimulation = int.Parse(sheet.Cells["B11"].Value.ToString()),
                    maxIteration = int.Parse(sheet.Cells["B12"].Value.ToString())
                };

                so.mctsConfig.Add(data);
            }
            else
            {
                var data = new Constants.MinimaxData()
                {
                    difficulty = i,
                    maxDepth = int.Parse(sheet.Cells["B3"].Value.ToString()),
                    win = float.Parse(sheet.Cells["B5"].Value.ToString()),
                    oneStep2Win = float.Parse(sheet.Cells["B6"].Value.ToString()),
                    twoSteps2Win = float.Parse(sheet.Cells["B7"].Value.ToString()),
                    treeSteps2Win = float.Parse(sheet.Cells["B8"].Value.ToString())
                };

                so.minimaxConfig.Add(data);
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        fs.Close();
    }
}