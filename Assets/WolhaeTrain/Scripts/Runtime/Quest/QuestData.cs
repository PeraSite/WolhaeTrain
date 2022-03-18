using System.Collections.Generic;
using System.Linq;
using PeraCore.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

public class QuestData : CustomScriptableObject {
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
}
