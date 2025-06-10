using System;

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

    // 상태를 변경하는 메서드가 아닌, 상태를 조회하는 메서드정도는 DTO에 들어가도 된다.
    // DTO는 데이터 전송 객체로, 주로 데이터를 읽기 위한 용도로 사용되며, 상태 변경 로직은 도메인 모델에서 처리하는 것이 좋다.
    public bool CanClaimReward()
    {
        return !RewardClaimed && GoalValue <= CurrentValue;
    }
}
