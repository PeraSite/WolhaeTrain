using Sirenix.OdinInspector;
using UnityEngine;

public struct QuestSelection {
	[BoxGroup("선택지")]
	public string ButtonText, ResultText;

	[BoxGroup("스탯")]
	public int Fuel, Clean, Hunger, Mental;

	[Tooltip("말하는 이가 상태이상이 있다면 이 값이 True인 선택지만 고를 수 있습니다")]
	public bool canSelectIfHaveEffect;
}
