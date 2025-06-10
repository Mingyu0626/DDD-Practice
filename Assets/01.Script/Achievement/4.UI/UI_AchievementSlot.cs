using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_AchievementSlot : MonoBehaviour
{
    public TextMeshProUGUI NameTextUI;
    public TextMeshProUGUI DescriptionTextUI;
    public TextMeshProUGUI RewardCountTextUI;
    public Slider ProgressSlider;
    public TextMeshProUGUI ProgressTextUI;
    public TextMeshProUGUI RewardClaimDate;
    public Button RewardClaimButton;

    public void Refresh(AchievementDTO achievement)
    {
        NameTextUI.text = achievement.Name;
        DescriptionTextUI.text = achievement.Description;
        RewardCountTextUI.text = achievement.RewardAmount.ToString();
        ProgressSlider.value = (float)achievement.CurrentValue / achievement.GoalValue;
        ProgressTextUI.text = $"{achievement.CurrentValue} / {achievement.GoalValue}";
        
        // 등등....
    }
}
