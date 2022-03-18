using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities;
using UnityEngine;

public struct Quest {
	[BoxGroup("정보")]
	public string Title;

	[BoxGroup("정보")]
	[TextArea]
	public string Description;

	[BoxGroup("정보")]
	public CharacterType Talker;

	[BoxGroup("정보")]
	[SuffixLabel("%", true)]
	public int SpawnProbability;

	[BoxGroup("정보")]
	public bool IsStory;

	[OdinSerialize]
	public List<QuestSelection> Selections;

	[OdinSerialize]
	[HideReferenceObjectPicker]
	public List<IQuestCondition> Conditions;

	[OdinSerialize]
	[HideReferenceObjectPicker]
	public List<IQuestAction> Actions;

	public bool CheckConditions() => Conditions.All(condition => condition.Check());

	[ButtonGroup]
	public void ExecuteActions() => Actions.ForEach(action => action.Execute());

	[ButtonGroup]
	private void PrintConditions() => Debug.Log(Conditions.All(condition => condition.Check()));

	public bool Equals(Quest other) {
		return Title == other.Title;
	}

	public override bool Equals(object obj) {
		return obj is Quest other && Equals(other);
	}

	public override int GetHashCode() {
		return (Title != null ? Title.GetHashCode() : 0);
	}
}
