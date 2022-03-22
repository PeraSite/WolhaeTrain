using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Random = UnityEngine.Random;

public interface IQuestAction {
	void Execute();
}

public class LogQuestAction : IQuestAction {
	public string Message;

	public void Execute() {
		DebugUtils.Log(Message);
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

public class EndingAction : IQuestAction {
	public EndingDataEvent Event;
	public EndingDataVariable Variable;

	[InlineProperty, HideLabel]
	public EndingDataConstant Data;

	public void Execute() {
		Variable.Value = Data.Value;
		Event.Raise(Data.Value);
	}
}

public class CharacterStatAction : IQuestAction {
	[HorizontalGroup("Horiz"), HideLabel]
	[VerticalGroup("Horiz/Stat")]
	public CharacterStatVariable Stat;

	[HorizontalGroup("Horiz"), HideLabel]
	[VerticalGroup("Horiz/Stat")]
	public CharacterStatType StatType;

	[HorizontalGroup("Horiz", 50), HideLabel]
	public int Value;

	public enum CharacterStatType {
		Hunger,
		Mental
	}

	public void Execute() {
		if (StatType == CharacterStatType.Hunger)
			Stat.Value = Stat.Value with {
				Hunger = Mathf.Clamp(Stat.Value.Hunger + Value, 0, 100)
			};
		else
			Stat.Value = Stat.Value with {
				Mental = Mathf.Clamp(Stat.Value.Mental + Value, 0, 100)
			};
	}
}

public class ClearStatusEffect : IQuestAction {
	[Title("ClearStatusEffect")]
	[HideLabel]
	public List<CharacterStatVariable> Characters = new();

	public void Execute() {
		Characters.ForEach(c => c.Value = c.Value with {
			Effects = Array.Empty<StatusEffect>()
		});
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
