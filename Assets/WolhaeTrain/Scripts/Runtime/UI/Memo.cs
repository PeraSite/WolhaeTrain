using System.Collections.Generic;
using PeraCore.Runtime;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Memo : SerializedMonoBehaviour {
	public Image Image;
	public TextMeshProUGUI Text;
	public List<Sprite> Sprites;

	public Quest Quest;
	public int Position;

	[Button]
	public void Init(Quest quest, int position) {
		Image.sprite = Sprites.Random();
		Text.text = quest.Title;
		Position = position;
		Quest = quest;
	}
}
