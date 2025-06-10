using Gpm.Ui;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_AchievementSlotData : InfiniteScrollData
{
    public readonly string ID;
    public readonly string Name;
    public readonly string Description;
    public readonly EAchievementCondition Condition;
    public readonly int GoalValue;
    public readonly ECurrencyType RewardCurrencyType;
    public readonly int RewardAmount;

    public int CurrentValue;
    public bool RewardClaimed;

    public UI_AchievementSlotData(AchievementDTO dto)
    {
        ID = dto.ID;
        Name = dto.Name;
        Description = dto.Description;
        Condition = dto.Condition;
        GoalValue = dto.GoalValue;
        RewardCurrencyType = dto.RewardCurrencyType;
        RewardAmount = dto.RewardAmount;
        CurrentValue = dto.CurrentValue;
        RewardClaimed = dto.RewardClaimed;
    }
}


public class UI_AchievementSlot : InfiniteScrollItem
{
    public TextMeshProUGUI NameTextUI;
    public TextMeshProUGUI DescriptionTextUI;
    public TextMeshProUGUI RewardCountTextUI;
    public Slider ProgressSlider;
    public TextMeshProUGUI ProgressTextUI;
    public TextMeshProUGUI RewardClaimDate;
    public Button RewardClaimButton;

    private AchievementDTO _achievementDTO;

    public override void UpdateData(InfiniteScrollData scrollData)
    {
        base.UpdateData(scrollData);
        UI_AchievementSlotData achievementSlotData = scrollData as UI_AchievementSlotData;

        _achievementDTO = new AchievementDTO(
            achievementSlotData.ID,
            achievementSlotData.Name,
            achievementSlotData.Description,
            achievementSlotData.Condition,
            achievementSlotData.GoalValue,
            achievementSlotData.RewardCurrencyType,
            achievementSlotData.RewardAmount,
            achievementSlotData.CurrentValue,
            achievementSlotData.RewardClaimed
        );
        Refresh(_achievementDTO);
    }

    public void Refresh(AchievementDTO achievementDTO)
    {
        NameTextUI.text = achievementDTO.Name;
        DescriptionTextUI.text = achievementDTO.Description;
        RewardCountTextUI.text = achievementDTO.RewardAmount.ToString();
        ProgressSlider.value = (float)achievementDTO.CurrentValue / achievementDTO.GoalValue;
        ProgressTextUI.text = $"{achievementDTO.CurrentValue} / {achievementDTO.GoalValue}";

        RewardClaimButton.interactable = achievementDTO.CanClaimReward();
        RewardClaimButton.image.color = achievementDTO.CanClaimReward() ? Color.red : Color.white;

        // 등등....
    }

    public void ClaimReward()
    {
        if (AchievementManager.Instance.TryClaimReward(_achievementDTO))
        {
            // 꽃가루 뿌려주고
        }
        else
        {
            // 진행도가 부족합니다...
        }
    }
}
