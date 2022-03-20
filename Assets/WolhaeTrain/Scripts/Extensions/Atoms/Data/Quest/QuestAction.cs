﻿using System.Linq;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public interface IQuestAction {
	void Execute();
}

public class LogQuestAction : IQuestAction {
	public string Message;

	public void Execute() {
		Debug.Log(Message);
	}
}

public class SetBoolVariableAction : IQuestAction {
	[HorizontalGroup]
	public BoolVariable Variable;

	[HideLabel, HorizontalGroup]
	public bool Value;

	public void Execute() {
		Variable.Value = Value;
	}
}

public class AddStatusEffect : IQuestAction {

	[HorizontalGroup(), HideLabel]
	public CharacterStatVariable Stat;

	[HorizontalGroup(100), HideLabel]
	public StatusEffect Effect;

	[HorizontalGroup(50), HideLabel, SuffixLabel("%", true)]
	public float Percent;

	public void Execute() {
		if (Random.Range(0, 100) <= Percent) {
			var effects = Stat.Value.Effects.ToList();
			effects.Add(Effect);
			Stat.Value = Stat.Value with {
				Effects = effects.ToArray()
			};
		}
	}
}
