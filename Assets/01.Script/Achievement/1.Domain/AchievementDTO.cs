using System;
using Unity.VisualScripting;

[Serializable]
public class AchievementDTO
{
    public readonly string ID;
    public readonly string Name;
    public readonly string Description;
    public readonly EAchievementCondition Condition;
    public readonly int GoalValue;
    public readonly ECurrencyType RewardCurrencyType;
    public readonly int RewardAmount;
    public readonly int CurrentValue;
    public readonly bool RewardClaimed;


    public AchievementDTO(string id, int currentValue, bool rewardClaimed)
    {
        ID = id;
        CurrentValue = currentValue;
        RewardClaimed = rewardClaimed;
    }

    public AchievementDTO(Achievement achievement)
    {
        ID = achievement.ID;
        Name = achievement.Name;
        Description = achievement.Description;
        Condition = achievement.Condition;
        GoalValue = achievement.GoalValue;
        RewardCurrencyType = achievement.RewardCurrencyType;
        RewardAmount = achievement.RewardAmount;
        CurrentValue = achievement.CurrentValue;
        RewardClaimed = achievement.RewardClaimed;
    }

    public AchievementDTO(
        string id,
        string name,
        string description,
        EAchievementCondition condition,
        int goalValue,
        ECurrencyType rewardCurrencyType,
        int rewardAmount,
        int currentValue,
        bool rewardClaimed)
    {
        ID = id;
        Name = name;
        Description = description;
        Condition = condition;
        GoalValue = goalValue;
        RewardCurrencyType = rewardCurrencyType;
        RewardAmount = rewardAmount;
        CurrentValue = currentValue;
        RewardClaimed = rewardClaimed;
    }

    // ���¸� �����ϴ� �޼��尡 �ƴ�, ���¸� ��ȸ�ϴ� �޼��������� DTO�� ���� �ȴ�.
    // DTO�� ������ ���� ��ü��, �ַ� �����͸� �б� ���� �뵵�� ���Ǹ�, ���� ���� ������ ������ �𵨿��� ó���ϴ� ���� ����.
    public bool CanClaimReward()
    {
        return !RewardClaimed && GoalValue <= CurrentValue;
    }
}
