﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using PixelCrushers;
using Sirenix.Utilities;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class VariableSaver : Saver {
	public AtomBaseVariableList Variables;

	public QuestDatabase QuestDatabase;
	public List<QuestValueList> QuestLists;

	public List<AtomEventBase> Events;

	public string Separator = "뚌";

	public override string RecordData() {
		var varDict = Variables.ToDictionary(
			GetAtomID,
			v => v.BaseValue
		);
		var listDict = QuestLists.ToDictionary(
			GetAtomID,
			list => list.Select(q => q.ID).ToList()
		);
		var recordData = SaveSystem.Serialize(varDict) + Separator + SaveSystem.Serialize(listDict);

		return recordData;
	}

	public override void ApplyData(string data) {
		if(data == null)return;
		var split = data.Split(Separator);
		var varDictString = split[0];
		var listDictString = split[1];

		var varDict = SaveSystem.Deserialize<Dictionary<string, object>>(varDictString);
		foreach (var variable in Variables) {
			var id = GetAtomID(variable);
			variable.BaseValue = varDict[id];

			var listDict = SaveSystem.Deserialize<Dictionary<string, List<int>>>(listDictString);
		foreach (var list in QuestLists) {
			var dictName = GetAtomID(list);
			var valueList = listDict[dictName];
			var result = valueList.Select(id => QuestDatabase.First(q => q.Value.ID == id)).ToList();
			if (!result.IsNullOrEmpty()) {
				list.IList = result.Select(qc => qc.Value).ToList();
			}
		}
	}

	public override void OnRestartGame() {
		foreach (var atomEventBase in Events) {
			atomEventBase.ResetBuffer();
		}
		foreach (var variable in Variables) {
			Debug.Log("Resetting " + variable.name);
			variable.ResetValue();
		}
		foreach (var list in QuestLists) {
			list.Clear();
		}
	}

	private string GetAtomID(AtomBaseVariable v) => v.Id.IsNullOrWhitespace() ? v.name : v.Id;

	private string GetAtomID(BaseAtomValueList v) => v.name;
}
