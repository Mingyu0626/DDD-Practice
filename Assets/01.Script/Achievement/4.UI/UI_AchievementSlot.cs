using Gpm.Ui;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_AchievementSlot : InfiniteScrollItem
{
    public TextMeshProUGUI NameTextUI;
    public TextMeshProUGUI DescriptionTextUI;
    public TextMeshProUGUI RewardCountTextUI;
    public Slider ProgressSlider;
    public TextMeshProUGUI ProgressTextUI;
    public TextMeshProUGUI RewardClaimDate;
    public Button RewardClaimButton;

    private AchievementDTO _achivementDTO;

    public void Refresh(AchievementDTO achievementDTO)
    {
        _achivementDTO = achievementDTO;
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
        if (AchievementManager.Instance.TryClaimReward(_achivementDTO))
        {
            // 꽃가루 뿌려주고
        }
        else
        {
            // 진행도가 부족합니다...
        }
    }
}
