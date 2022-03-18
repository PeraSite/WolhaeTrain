using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class Quest {
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

	public List<QuestSelection> Selections = new();

	[HideReferenceObjectPicker]
	public List<IQuestCondition> Conditions = new();

	[HideReferenceObjectPicker]
	public List<IQuestAction> Actions = new();

	public bool CheckConditions() => Conditions.All(condition => condition.Check());

	[ButtonGroup]
	public void ExecuteActions() => Actions.ForEach(action => action.Execute());

	[ButtonGroup]
	private void PrintConditions() => Debug.Log(Conditions.All(condition => condition.Check()));

	protected bool Equals(Quest other) {
		return Title == other.Title;
	}

	public override bool Equals(object obj) {
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((Quest) obj);
	}

	public override int GetHashCode() {
		return (Title != null ? Title.GetHashCode() : 0);
	}
}
