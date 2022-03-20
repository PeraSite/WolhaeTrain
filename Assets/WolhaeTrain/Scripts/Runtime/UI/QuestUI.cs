using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using PixelCrushers;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using TMPro;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestUI : SerializedMonoBehaviour {
	[Header("일차")]
	public IntVariable Day;

	public IntEvent DayChangedEvent;
	public TextMeshProUGUI DayText;

	[Header("퀘스트")]
	public QuestValueList ActiveQuest;

	public Memo MemoPrefab;
	public List<RectTransform> MemoPositions = new();
	private Dictionary<int, Memo> CreatedMemo = new(); //Dictionary<position, CreatedMemo>
	public Canvas Canvas;

	public QuestEvent OnActiveQuestAddEvent;
	public QuestEvent OnActiveQuestRemoveEvent;

	[Header("연료")]
	public IntVariable Fuel;
	public IntEvent FuelChangedEvent;
	public TextMeshProUGUI FuelText;
	public Image FuelGauge;


	[Header("청결")]
	public IntVariable Clean;
	public IntEvent CleanChangedEvent;
	public TextMeshProUGUI CleanText;
	public List<GameObject> Checks;


	[Header("지도")]
	public RectTransform Map;

	public RectTransform ParentPinTransform;
	public RectTransform PinTransform;
	public float PinStartY = 8;
	public float PinEndY = 35;

	public List<StatusEffect> ExploreRestrictEffects = new();
	public CharacterStatEvent CharacterStatChangedEvent;
	public List<Image> CharacterIcons;
	public List<CharacterStatVariable> CharacterStats;
	public List<GameObject> Circles;
	public CharacterStatEvent ExploreSelectedEvent;

	private static bool shouldInit;

	private void OnEnable() {
		DayChangedEvent.Register(OnDayChanged);
		SaveSystem.saveDataApplied += OnSaveLoaded;

		OnActiveQuestAddEvent.Register(OnActiveQuestAdd);
		OnActiveQuestRemoveEvent.Register(OnActiveQuestRemove);

		FuelChangedEvent.Register(OnFuelChanged);
		CleanChangedEvent.Register(OnCleanChanged);

		CharacterStatChangedEvent.Register(OnCharacterStatChanged);
#if UNITY_EDITOR
		if (shouldInit) {
			OnDayChanged(Day.InitialValue);
			OnFuelChanged(Fuel.InitialValue);
			OnCleanChanged(Clean.InitialValue);
		}
#endif
	}

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	public static void Test() {
		if (SceneManager.GetActiveScene().buildIndex != 0) {
			shouldInit = true;
		} else {
			shouldInit = false;
		}
	}

	private void OnDisable() {
		DayChangedEvent.Unregister(OnDayChanged);
		SaveSystem.saveDataApplied -= OnSaveLoaded;

		OnActiveQuestAddEvent.Unregister(OnActiveQuestAdd);
		OnActiveQuestRemoveEvent.Unregister(OnActiveQuestRemove);

		FuelChangedEvent.Unregister(OnFuelChanged);
		CleanChangedEvent.Unregister(OnCleanChanged);

		CharacterStatChangedEvent.Unregister(OnCharacterStatChanged);

		foreach (var memo in CreatedMemo.Values) {
			Destroy(memo.gameObject);
		}
		CreatedMemo.Clear();
	}

	private void OnCleanChanged(int clean) {
		Debug.Log("Clean changed to" + clean);
		CleanText.text = clean switch {
			>= 90 => "VERY\nCLEAN",
			>= 80 => "CLEAN",
			>= 60 => "USABLE",
			>= 40 => "DIRTY",
			>= 20 => "VERY\nDIRTY",
			_ => "<color=#840808>DEATH</color>"
		};
		var checkAmount = clean / 20;
		Checks.ForEach((check, index) => { check.SetActive(index <= checkAmount); });
	}

	private void OnFuelChanged(int fuel) {
		Debug.Log("Fuel changed to" + fuel);
		FuelText.text = fuel.ToString();
		FuelGauge.fillAmount = fuel / 100f;
	}

	private void OnDayChanged(int day) {
		Debug.Log("Day changed to" + day);
		DayText.text = day + "일 째";

		ParentPinTransform.gameObject.SetActive(false);
		Circles.ForEach((obj, index) => { obj.SetActive(false); });
	}

	private void OnSaveLoaded() {
		foreach (var pair in CreatedMemo) {
			var (position, createdMemo) = pair;
			Destroy(createdMemo.gameObject);
		}

		CreatedMemo.Clear();
		foreach (var quest in ActiveQuest) {
			CreateMemo(quest);
		}
	}

	private void OnActiveQuestAdd(Quest quest) {
		CreateMemo(quest);
	}

	private void OnActiveQuestRemove(Quest quest) {
		DeleteMemo(quest);
	}

	private void CreateMemo(Quest quest) {
		Debug.Log("CreateMemo: " + quest.Title);

		var parent = MemoPositions.Where((pos, index) => !CreatedMemo.ContainsKey(index)).First();
		var index = MemoPositions.IndexOf(parent);
		var newMemo = Instantiate(MemoPrefab, parent);
		newMemo.Init(quest, index, Canvas);
		CreatedMemo[index] = newMemo;
	}

	private void DeleteMemo(Quest quest) {
		Debug.Log("DeleteMemo: " + quest.Title);

		var pair = CreatedMemo.FirstOrDefault(pair => pair.Value.Quest.Title == quest.Title);
		if (pair.Equals(null)) {
			return;
		}

		var (index, memo) = pair;
		CreatedMemo.Remove(index);
		memo.Destroy();
	}

	public void MoveMapPin() {
		ParentPinTransform.gameObject.SetActive(true);

		var screenPosition = Input.mousePosition;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(Map, screenPosition, null, out var localPosition);
		ParentPinTransform.anchoredPosition = localPosition;
		PinTransform.DOKill();
		PinTransform.anchoredPosition = new Vector2(0, PinStartY);
		PinTransform.DOAnchorPosY(PinEndY, 0.2f);
	}

	private void OnCharacterStatChanged(CharacterStat stat) {
		if (stat.Type == CharacterType.None) return;
		var ID = (int) (stat.Type - 1);
		CharacterIcons[ID].color = stat.Effects.Any(e => ExploreRestrictEffects.Contains(e))
			? new Color(1f, 1f, 1f, 0.5f)
			: new Color(1f, 1f, 1f, 1f);
	}

	public void SetExploreCharacter(int selected) {
		var stat = CharacterStats[selected].Value;
		if (stat.Effects.Any(e => ExploreRestrictEffects.Contains(e))) return;

		Circles.ForEach((obj, index) => { obj.SetActive(selected == index); });

		Debug.Log("탐험 지정 : " + stat.Type);
		ExploreSelectedEvent.Raise(stat);
	}
}
