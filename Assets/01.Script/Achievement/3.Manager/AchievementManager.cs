using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance;


    [SerializeField]
    private List<AchievementSO> _metaDatas;

    private List<Achievement> _achievements;
    public List<AchievementDTO> Achievements => _achievements.ConvertAll((a) => new AchievementDTO(a));

    private AchievementRepository _repository;

    public event Action OnDataChanged;
    public event Action<AchievementDTO> OnNewAchievementAchieved;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
    }

    private void Init()
    {
        _achievements = new List<Achievement>();
        _repository = new AchievementRepository();

        List<AchievementSaveData> saveDatas = _repository.Load();
        foreach (var metaData in _metaDatas)
        {
            // 중복 검사
            Achievement duplicatedAchievement = FindByID(metaData.ID);
            if (duplicatedAchievement != null)
            {
                throw new Exception($"업적 ID({metaData.ID})가 중복됩니다.");
            }
            
            AchievementSaveData saveData = 
                saveDatas?.Find(a => a.ID == metaData.ID) ?? new AchievementSaveData();
            Achievement achievement = new Achievement(metaData, saveData);
            _achievements.Add(achievement);
        }
    }

    private Achievement FindByID(string id)
    {
        return _achievements.Find(a => a.ID == id);
    }

    public void Increase(EAchievementCondition condition, int value)
    {
        foreach (var achievement in _achievements)
        {
            if (achievement.Condition == condition)
            {
                bool prevCanClaimReward = achievement.CanClaimReward();
                achievement.Increase(value);
                bool canClaimReward = achievement.CanClaimReward();

                if (prevCanClaimReward != canClaimReward && canClaimReward)
                {
                    // 보상 획득 가능 상태로 변경되었을 때 Notify를 여기서 시켜줘야한다.
                    OnNewAchievementAchieved?.Invoke(new AchievementDTO(achievement));
                }
            }
        }
        _repository.Save(Achievements);
        OnDataChanged?.Invoke();
    }

    public bool TryClaimReward(AchievementDTO achievementDto)
    {
        Achievement achievement = FindByID(achievementDto.ID);
        if (achievement == null)
        {
            return false;
        }

        if (achievement.TryClaimReward())
        {
            CurrencyManager.Instance.Add(achievement.RewardCurrencyType, achievement.RewardAmount);
            OnDataChanged?.Invoke();
            return true;
        }
        return false;
    }
}
