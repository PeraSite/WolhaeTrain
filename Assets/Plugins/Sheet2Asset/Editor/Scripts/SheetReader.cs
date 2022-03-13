using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PeraCore.Editor;
using PeraCore.Runtime;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;

[HideMonoScript]
public class SheetReader : SingletonScriptableObject<SheetReader> {
	private const int SkipRow = 2;

	[FoldoutGroup("Credentials")]
	public string ServiceAccountID;

	[FoldoutGroup("Credentials")]
	public string PrivateKey;

	[FolderPath]
	public string SheetsPath = "Assets/Plugins/Sheet2Asset/Runtime/Data";

	[ListDrawerSettings(
		CustomAddFunction = nameof(CreateCachedSheet),
		CustomRemoveElementFunction = nameof(DeleteCachedSheet),
		OnBeginListElementGUI = nameof(OnBeginSheetListElementGUI),
		DraggableItems = false)
	]
	[InlineEditor(InlineEditorObjectFieldModes.CompletelyHidden)]
	public List<SheetFile> Sheets = new List<SheetFile>();

	private SheetFile CreateCachedSheet() {
		var instance = CreateInstance<SheetFile>();
		instance.name = "Sheet";
		if (!Directory.Exists(SheetsPath))
			Directory.CreateDirectory(SheetsPath);
		instance.CreateAsset(Path.Combine(SheetsPath, "Sheet.asset"));
		return instance;
	}

	private void DeleteCachedSheet(SheetFile selected) {
		foreach (var worksheet in selected.Worksheets) {
			worksheet.DeleteObject();
		}
		Sheets.Remove(selected);
		selected.DeleteObject();
	}

	private void OnBeginSheetListElementGUI(int elementIndex) {
		var sheet = Sheets[elementIndex];
		if (SirenixEditorGUI.IconButton(EditorIcons.Refresh)) {
			SheetAPI.InitService(ServiceAccountID, PrivateKey);
			TypeMap.Init();

			var request = SheetAPI.Service.Spreadsheets.Get(sheet.ID);
			request.IncludeGridData = true;
			var response = request.Execute();
			sheet.name = response.Properties.Title;
			sheet.URL = response.SpreadsheetUrl;

			AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(sheet), response.Properties.Title);
			AssetDatabase.SaveAssets();

			if (LinqExtensions.IsNullOrEmpty(sheet.Worksheets)) {
				sheet.Worksheets = response.Sheets.Select(worksheet => {
					var instance = CreateInstance<Sheet>();
					instance.name = worksheet.Properties.Title;
					instance.Parent = sheet;
					sheet.AddSubObject(instance);
					return instance;
				}).ToList();
			} else {
				foreach (var cachedWorksheet in sheet.Worksheets) {
					var sheetType = cachedWorksheet.Type;
					if (sheetType == null) continue;

					var worksheet = response.Sheets.First(target => target.Properties.Title == cachedWorksheet.Name);

					var grid = worksheet.Data.SelectMany(data =>
						data.RowData
							.Skip(SkipRow)
							.Select(row => row.Values
								.Select(cell => cell.FormattedValue)
								.Where(cellText => !cellText.IsNullOrWhitespace()).ToList()
							).Where(row => !LinqExtensions.IsNullOrEmpty(row)).ToList()).ToList();

					var sheetFields = sheetType.GetFields();
					var fields = sheetFields.Select(fieldInfo => TypeMap.Map[fieldInfo.FieldType]).ToList();
					cachedWorksheet.WeakData.Clear();

					foreach (var rawData in grid) {
						var instance = Activator.CreateInstance(sheetType);

						rawData.ForEach((cell, index) => {
							var field = fields[index];
							var value = field.Read(cell);
							sheetFields[index].SetValue(instance, value);
						});

						cachedWorksheet.WeakData.Add(instance);
					}
				}
			}
			Sheets[elementIndex] = sheet;
		}
	}
}
