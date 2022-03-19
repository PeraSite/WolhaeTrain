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
	None,
	Dad,
	Mom,
	Son,
	Daughter
}

public enum StatusEffect {
	Exhaust,
	Cold,
	Infect,
	Hurt,
	Crazy,
}

public static class StatusEffectUtil {
	public static string GetName(this StatusEffect effect) => effect switch {
		StatusEffect.Exhaust => "탈진",
		StatusEffect.Cold => "감기",
		StatusEffect.Infect => "감염",
		StatusEffect.Hurt => "상처",
		StatusEffect.Crazy => "미침",
		_ => throw new ArgumentOutOfRangeException(nameof(effect), effect, null)
	};
}
