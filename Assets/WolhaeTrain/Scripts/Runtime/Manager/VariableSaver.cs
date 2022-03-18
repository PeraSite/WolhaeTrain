using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PixelCrushers;
using Sirenix.Utilities;
using UnityAtoms;
using UnityAtoms.BaseAtoms;

public class VariableSaver : Saver {
	public AtomBaseVariableList Variables;
	public List<BaseAtomValueList> Lists;

	public string Separator = "뚌";

	public override string RecordData() {
		var varDict = Variables.ToDictionary(
			GetAtomID,
			v => v.BaseValue
		);
		var listDict = Lists.ToDictionary(
			GetAtomID,
			list => list.IList
		);
		var recordData = SaveSystem.Serialize(varDict) + Separator + SaveSystem.Serialize(listDict);

		return recordData;
	}

	public override void ApplyData(string data) {
		var split = data.Split(Separator);
		var varDictString = split[0];
		var listDictString = split[1];

		var varDict = SaveSystem.Deserialize<Dictionary<string, object>>(varDictString);
		foreach (var variable in Variables) {
			var id = GetAtomID(variable);
			variable.BaseValue = varDict[id];
		}

		var listDict = SaveSystem.Deserialize<Dictionary<string, IList>>(listDictString);
		foreach (var list in Lists) {
			var id = GetAtomID(list);
			var valueList = listDict[id];
			list.IList = valueList;
		}
	}

	public override void OnRestartGame() {
		foreach (var variable in Variables) {
			variable.ResetValue();
		}
		foreach (var list in Lists) {
			list.Clear();
		}
	}

	private string GetAtomID(AtomBaseVariable v) => v.Id.IsNullOrWhitespace() ? v.name : v.Id;

	private string GetAtomID(BaseAtomValueList v) => v.name;
}
