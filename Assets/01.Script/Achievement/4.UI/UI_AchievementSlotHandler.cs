using System.Collections.Generic;
using UnityEngine;

public class UI_AchievementSlotHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _achievementSlotUIPrefab;

    private void Awake()
    {
    }

    private void Start()
    {
        AdjustSlots();
    }

    private List<GameObject> GetCurrentSlots()
    {
        List<GameObject> result = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.GetComponent<UI_AchievementSlot>() != null)
            {
                result.Add(child.gameObject);
            }
        }
        return result;
    }
    private void AdjustSlots()
    {
        List<GameObject> result = GetCurrentSlots();

        int currentSlotCount = result.Count;
        int metaDataCount = AchievementManager.Instance.MetaDataCount;

        if (currentSlotCount < metaDataCount)
        {
            for (int i = currentSlotCount; i < metaDataCount; i++)
            {
                GameObject slot = Instantiate(_achievementSlotUIPrefab, transform);
                slot.name = $"{_achievementSlotUIPrefab.name} ({i})";
            }
        }
        else
        {
            for (int i = currentSlotCount - 1; i > metaDataCount; i--)
            {
                Destroy(result[i]);
            }
        }
    }
}
