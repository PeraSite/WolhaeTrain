using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using PixelCrushers;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using TMPro;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : SerializedMonoBehaviour {
	[Header("일차")]
	public IntVariable Day;

	public IntEvent DayChangedEvent;
	public TextMeshProUGUI DayText;

	[Header("퀘스트")]
	public QuestValueList QuestDatabase;

	public QuestValueList ActiveQuest;

	public Memo MemoPrefab;
	public List<RectTransform> MemoPositions = new();
	private Dictionary<int, Memo> CreatedMemo = new(); //Dictionary<position, CreatedMemo>
	public Canvas Canvas;

	public QuestEvent MakeQuestActiveEvent;
	public QuestEvent OnActiveQuestAddEvent;
	public QuestEvent OnActiveQuestRemoveEvent;
	public QuestEvent OnClearQuestAddEvent;
	public QuestEvent OnClearQuestRemoveEvent;

	[Header("연료")]
	public IntVariable FuelVariable;

	public IntEvent FuelChangedEvent;
	public TextMeshProUGUI FuelText;
	public Image FuelGauge;


	[Header("청결")]
	public IntVariable CleanVariable;

	public IntEvent CleanChangedEvent;
	public TextMeshProUGUI CleanText;
	public List<GameObject> Checks;


	[Header("지도")]
	public RectTransform Map;

	public RectTransform ParentPinTransform;
	public RectTransform PinTransform;
	public float PinStartY = 8;
	public float PinEndY = 35;

	private void OnEnable() {
		DayChangedEvent.Register(OnDayChanged);
		SaveSystem.saveDataApplied += OnSaveLoaded;

		OnActiveQuestAddEvent.Register(OnActiveQuestAdd);
		OnActiveQuestRemoveEvent.Register(OnActiveQuestRemove);

		FuelChangedEvent.Register(OnFuelChanged);
		CleanChangedEvent.Register(OnCleanChanged);
	}

	private void OnDisable() {
		DayChangedEvent.Unregister(OnDayChanged);
		SaveSystem.saveDataApplied -= OnSaveLoaded;

		OnActiveQuestAddEvent.Unregister(OnActiveQuestAdd);
		OnActiveQuestRemoveEvent.Unregister(OnActiveQuestRemove);

		FuelChangedEvent.Unregister(OnFuelChanged);
		CleanChangedEvent.Unregister(OnCleanChanged);

		foreach (var memo in CreatedMemo.Values) {
			Destroy(memo.gameObject);
		}
		CreatedMemo.Clear();
	}

	private void OnCleanChanged(int clean) {
		Debug.Log("Clean changed to" + clean);
		CleanText.text = clean switch {
			>= 100 => "VERY\nCLEAN",
			>= 80 => "CLEAN",
			>= 60 => "USABLE",
			>= 40 => "DIRTY",
			>= 20 => "VERY\nDIRTY",
			_ => "<color=#840808>DEATH</color>"
		};
		var checkAmount = clean / 20;
		Debug.Log(checkAmount);
		Checks.ForEach((check, index) => {
			check.SetActive(index <= checkAmount);
		});
	}

	private void OnFuelChanged(int fuel) {
		Debug.Log("Fuel changed to" + fuel);
		FuelText.text = fuel.ToString();
		FuelGauge.fillAmount = fuel / 100f;
	}

	private void OnDayChanged(int day) {
		Debug.Log("Day changed to" + day);
		DayText.text = day + "일 째";
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
		Destroy(memo.gameObject);
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
}
