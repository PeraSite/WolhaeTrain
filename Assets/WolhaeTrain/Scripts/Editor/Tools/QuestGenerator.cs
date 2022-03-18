using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.WebPages;
using PeraCore.Editor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using UnityAtoms;
using UnityEditor;
using UnityEngine;

public class QuestGenerator : OdinEditorWindow {
	[MenuItem("Tools/WolhaeTrain/Quest Generator")]
	private static void OpenWindow() {
		GetWindow<QuestGenerator>().Show();
	}

	[FoldoutGroup("Credentials")]
	public string ServiceAccountID = "perasite@spreadsheet-api-330305.iam.gserviceaccount.com";

	[FoldoutGroup("Credentials")]
	public string PrivateKey =
		"-----BEGIN PRIVATE KEY-----MIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQCQ8Uyb9yJl9nd4dq1/k7r/su+ITgjb7r12EAsxqkPRnr64WdxD7EV4mW5hYdryWgfhkWkTKYzRXtd5XW5FQ+PmPHOPug2ver7Kcn7i0pdluYEi3G4K+gU7Oga9YVfu2mYy7FkwYthjHeAU9c1ezTi4VShbF7k/kz4lqVU1menQg/1uoMBxBg83Z/VRdE5NGZsDXaPLgsJMFa0hrd7s0ZSR+cwMJxpdjJA7AeA4UvXBXcIODhftTAIrOwOmkQWmfAgz6z8H/vjSHRhBoF2euxfBxBBS3u/k0DbvzvcadOruYLxXKTxxXq4mbdCc4iZ/XOVhUgQ+0YyBY1ZV972I3KAFAgMBAAECggEALdowFpz5ZFEynUjJ/TSCcVatgSzSj6T4ictshyMqfjtecVp4HIK3vX2oViVLSuThXmIOYBICas+6kbnVYxMlmxXfpwXAQ5WsmsXazy242GT1bc5W/6D1m7VxIWMUmsq7jvuHAZAbjUuLsoXKGOYKPgIumJaqM2sEu4xUmPbeaWiF/BJx4JkXldGBiHZ1ogJfw1GCnbyjQOBrXPrDCDCUEsOLDTgdZN6XRhLxBvfR2I+Hie6afI6jVxEKsUDi7vsiz0rJGaW833iTAHFTOScCCvGqLuYyQze8sXSc+DiXujfROzRC8gyqtqhDGLw88QZXROQ9NCQBx5Ue21hcFzwbVwKBgQDJIA+oG6mdeQOkiCrlBHmNZ0EOPgyGLgmyxXVlw528zSrpl8BQcG1DBWO3R+7Me0l4acN3gtEjpdFQ+F95Ui0nJSUviHGCY+32h9L1RfbNXRzsclm9lViF+pCdvIPp/c0K9dvqPUkuEsQDxxIj6yaiOloFaRfUD1kF1LRzv+OXOwKBgQC4fRAqoEmnqUz+Ez5ogZRFIRAVOB+SejoU4NisSyt66/YiXQzChI4wXO/FbfUh51L1Hy5NRsZdXgASbwcQOT2v/RQB0u07S10zmuqlIpNkG6To6NTy3/YI3yZNgLfXIFslnZC88Joha8iMkkD4rdFXJYSMgic4sDAUsu9qtY2xvwKBgA6704nMJPvNPrAR7ZqDXmg2dTSW5RH7U2iOQJBo6ShIm2krXcJGyipLvcdSdLL0ISi6DsC1i56h6hiVaWEY3QfuF7BIvZAAxBD4WELxkifvN0w3AE38H9UIywlTIxLELf4sjqS1QqQmacehELoi4tyli6yzyzQUv7/GmkWUfBa7AoGBALgcfxS7D6ZVi7OHiuGKZ2ixvTYf40ov1mTdmv4eqk6qyuCyUbiRxsC3DsXNnTPvgdeD/ZY7Cl1FIPdEfB0RsuE9xEipsfxZkrKcaIzSO5tNBz80lMepAAUMhVIVIeJ7tNqOK1KTHo453VOD5XkHJgI9O9FpVjD5i5IsnEe21ahLAoGBALc9bxCzd0FqVV8oKzl9o2Cil02C4jMJG8L5oGH9/8w7/Fqm4qu4p+Eq7q3imhW+Z1edCePdr5bwHniw+aUSAtf107vIh2SGZRA8rh0KW+QK8AI4O+sYP4d7ElGeRrjfdaBfmYyB9X4CnXe3H3tDVJ4vKdJho5XpgLKya+gHYgNF-----END PRIVATE KEY-----";

	public string SheetID = "19-SoXt0dwAxYS40rmlOw1TiTy9NEWmuRaxrp77HR3Ec";

	public string SheetName = "안건";

	public int SkipRow = 2;

	[FolderPath]
	public string TargetFolder = "Assets/WolhaeTrain/Data/Quests";

	public bool GenerateAsset;

	public QuestValueList List;

	[Button]
	private void Generate() {
		SheetAPI.InitService(ServiceAccountID, PrivateKey);
		var request = SheetAPI.Service.Spreadsheets.Get(SheetID);
		request.IncludeGridData = true;
		var spreadsheet = request.Execute();
		var worksheets = spreadsheet.Sheets;
		var worksheet = worksheets.First(s => s.Properties.Title.Equals(SheetName));
		var grid = worksheet.Data.SelectMany(data =>
			data.RowData
				.Skip(SkipRow)
				.Select(row => row.Values
					.Select(cell => cell.FormattedValue).ToList()
				).Where(row => !row.IsNullOrEmpty()).ToList()).ToList();

		foreach (var row in grid) {
			Debug.Log(string.Join(",", row));
		}

		if (GenerateAsset) {
			if(!List.SafeIsUnityNull()) List.Clear();

			grid.Select(GenerateQuestionData)
				.ForEach(data => {
					if (data.Value.Title == "n") return;
					data.CreateAsset(Path.Combine(TargetFolder, $"{data.Value.Title}.asset"));
					if (!List.SafeIsUnityNull()) {
						List.Add(data.Value);
					}
				});
			AssetDatabase.Refresh();
		}
	}


	private QuestConstant GenerateQuestionData(List<string> data) {
		var instance = CreateInstance<QuestConstant>();
		instance.InitConstant(new Quest {
			Title = data[1],
			Talker = data[2] switch {
				"아빠" => CharacterType.Dad,
				"엄마" => CharacterType.Mom,
				"딸" => CharacterType.Daughter,
				"아들" => CharacterType.Son,
				_ => CharacterType.None
			},
			Description = data[3],
			SpawnProbability = data[7].Replace("%", "").AsInt(0)
		});

		var selection1 = new QuestSelection {
			ButtonText = data[8],
			Fuel = data[9].AsInt(0),
			Clean = data[10].AsInt(0),
			Hunger = data[11].AsInt(0),
			Mental = data[12].AsInt(0),
			ResultText = data[13],
			canSelectIfHaveEffect = data[6] == "선택지 1"
		};

		var selection2 = new QuestSelection {
			ButtonText = data[14],
			Fuel = data[15].AsInt(0),
			Clean = data[16].AsInt(0),
			Hunger = data[17].AsInt(0),
			Mental = data[18].AsInt(0),
			ResultText = data[19],
			canSelectIfHaveEffect = data[6] == "선택지 2"
		};

		instance.Value.Selections.Add(selection1);
		instance.Value.Selections.Add(selection2);
		return instance;
	}
}
