using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public interface IQuestCondition {
	public bool Check();
}

public class BoolVariableQuestCondition : IQuestCondition {
	public BoolVariable Variable;

	public bool Check() {
		return Variable.Value;
	}
}

public class IntVariableCompareQuestCondition : IQuestCondition {
	[HorizontalGroup(""), HideLabel]
	public IntVariable Variable;

	[HorizontalGroup("", 20), HideLabel]
	[ValueDropdown("@CompareType.Values")]
	public CompareType Compare;

	[HorizontalGroup("", 50), HideLabel]
	public int Value;


	public bool Check() {
		return Compare.Compare(Variable.Value, Value);
	}
}

public class CharacterStatCompareQuestCondition : IQuestCondition {
	[HorizontalGroup("Horiz"), HideLabel]
	[VerticalGroup("Horiz/Stat")]
	public CharacterStatVariable Stat;

	[HorizontalGroup("Horiz"), HideLabel]
	[VerticalGroup("Horiz/Stat")]
	public CharacterStatType StatType;

	[HorizontalGroup("Horiz", 20), HideLabel]
	[ValueDropdown("@CompareType.Values")]
	public CompareType Compare;

	[HorizontalGroup("Horiz", 50), HideLabel]
	public int Value;


	public bool Check() {
		var characterStat = StatType == CharacterStatType.Hunger ? Stat.Value.Hunger : Stat.Value.Mental;
		return Compare.Compare(characterStat, Value);
	}

	public enum CharacterStatType {
		Hunger,
		Mental
	}
}

[Serializable]
public struct CompareType {
	public static readonly CompareType EQUALS = new() {Name = "="};
	public static readonly CompareType LESS = new() {Name = "<"};
	public static readonly CompareType MORE = new() {Name = ">"};
	public static readonly CompareType EQUALS_OR_LESS = new() {Name = "<="};
	public static readonly CompareType EQUALS_OR_MORE = new() {Name = ">="};

	[HideInInspector]
	public string Name;

	public bool Compare(int a, int b) {
		if (Equals(EQUALS))
			return a == b;
		if (Equals(LESS))
			return a < b;
		if (Equals(MORE))
			return a > b;
		if (Equals(EQUALS_OR_LESS))
			return a <= b;
		if (Equals(EQUALS_OR_MORE))
			return a >= b;
		return false;
	}


	public static IEnumerable Values = new ValueDropdownList<CompareType> {
		{EQUALS.Name, EQUALS},
		{MORE.Name, MORE},
		{LESS.Name, LESS},
		{EQUALS_OR_MORE.Name, EQUALS_OR_MORE},
		{EQUALS_OR_LESS.Name, EQUALS_OR_LESS},
	};
}
