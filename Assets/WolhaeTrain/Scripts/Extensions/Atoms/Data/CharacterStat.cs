using System;
using System.Collections.Generic;

[Serializable]
public record CharacterStat {
	public CharacterType Type;
	public int Hunger;
	public int Mental;
	public StatusEffect[] Effects;
}

public enum CharacterType {
	None = 0,
	Dad = 1,
	Mom = 2,
	Son = 3,
	Daughter = 4
}

public enum StatusEffect {
	Exhaust,
	Cold,
	Infect,
	Hurt,
	Crazy,
}

public static class Util {
	public static string GetName(this CharacterType type) =>
		type switch {
			CharacterType.None => "",
			CharacterType.Dad => "정훈",
			CharacterType.Mom => "하나",
			CharacterType.Son => "현승",
			CharacterType.Daughter => "원재",
			_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
		};

	public static string GetName(this StatusEffect effect) =>
		effect switch {
			StatusEffect.Exhaust => "탈진",
			StatusEffect.Cold => "감기",
			StatusEffect.Infect => "감염",
			StatusEffect.Hurt => "상처",
			StatusEffect.Crazy => "미침",
			_ => throw new ArgumentOutOfRangeException(nameof(effect), effect, null)
		};
}
