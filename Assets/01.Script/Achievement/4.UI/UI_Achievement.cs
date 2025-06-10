using Gpm.Ui;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_Achievement : MonoBehaviour
{
    private List<UI_AchievementSlot> _slots;
    [SerializeField]
    private GameObject _achievementSlotUIPrefab;
    [SerializeField]
    private VerticalLayoutGroup _verticalLayoutGroup;

    [SerializeField]
    private InfiniteScroll _infiniteScroll;
    private List<UI_AchievementSlotData> _achievementSlotData;

    private void Start()
    {
        _achievementSlotData = new List<UI_AchievementSlotData>();
        //ClearSlot();
        //InitSlot();
        Refresh();
        AchievementManager.Instance.OnDataChanged += Refresh;
        gameObject.SetActive(false);
    }
    
    private void Refresh()
    {
        List<AchievementDTO> achievements = AchievementManager.Instance.Achievements;

        for (int i = 0; i < achievements.Count; i++)
        {
            if (_achievementSlotData.Count <= i)
            {
                _achievementSlotData.Add(new UI_AchievementSlotData(achievements[i]));
                _infiniteScroll.InsertData(_achievementSlotData.Last());
            }
            _achievementSlotData[i].CurrentValue = achievements[i].CurrentValue;
            _achievementSlotData[i].RewardClaimed = achievements[i].RewardClaimed;
            _infiniteScroll.UpdateData(_achievementSlotData[i]);
        }
    }

    private void ClearSlot()
    {
        _slots = _verticalLayoutGroup.gameObject.GetComponentsInChildren<UI_AchievementSlot>(true).ToList();
        foreach (var slot in _slots)
        {
            Destroy(slot.gameObject);
        }
        _slots.Clear();
    }

    private void InitSlot()
    {
        int metaDataCount = AchievementManager.Instance.MetaDataCount;
        for (int i = 0; i < metaDataCount; i++)
        {
            GameObject slot = Instantiate(_achievementSlotUIPrefab, _verticalLayoutGroup.transform);
            slot.name = $"{_achievementSlotUIPrefab.name} ({i})";
            _slots.Add(slot.GetComponent<UI_AchievementSlot>());
        }
    }
}