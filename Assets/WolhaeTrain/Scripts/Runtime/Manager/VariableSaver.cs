using System.Collections.Generic;
using System.Linq;
using PixelCrushers;
using Sirenix.Utilities;
using UnityAtoms;
using UnityAtoms.BaseAtoms;

public class VariableSaver : Saver {
	public AtomBaseVariableList Variables;

	public override string RecordData() {
		var dictionary = Variables.ToDictionary(
			GetAtomID,
			v => v.BaseValue
		);
		return SaveSystem.Serialize(dictionary);
	}

	public override void ApplyData(string s) {
		var dict = SaveSystem.Deserialize<Dictionary<string, object>>(s);
		foreach (var variable in Variables) {
			var id = GetAtomID(variable);
			variable.BaseValue = dict[id];
		}
	}

	public override void OnRestartGame() {
		foreach (var variable in Variables) {
			variable.Reset();
		}
	}

	private string GetAtomID(AtomBaseVariable v) => v.Id.IsNullOrWhitespace() ? v.name : v.Id;
}
