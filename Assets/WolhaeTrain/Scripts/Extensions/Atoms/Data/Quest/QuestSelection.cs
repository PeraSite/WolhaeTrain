using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public struct QuestSelection {
	[BoxGroup("선택지")]
	[TextArea]
	public string ButtonText, ResultText;

	[BoxGroup("스탯")]
	public int Fuel, Clean, Hunger, Mental;

	[Tooltip("말하는 이가 상태이상이 있다면 이 값이 True인 선택지만 고를 수 있습니다")]
	public bool canSelectIfHaveEffect;

	[HideReferenceObjectPicker]
	public List<IQuestAction> Actions;

	[Button]
	public void InvokeActions() => Actions.ForEach(a => a.Execute());
}
