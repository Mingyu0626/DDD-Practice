using TMPro;
using System.Collections;
using UnityEngine;

public class UI_AchievementNotification : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _nameTMP;
    [SerializeField]
    private TextMeshProUGUI _descriptionTMP;
    [SerializeField]
    private float _fadeOutDuration = 2f;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    private void OnEnable()
    {
        _canvasGroup.alpha = 1f;
    }

    private void Start()
    {
        AchievementManager.Instance.OnNewAchievementAchieved += Notification;
        gameObject.SetActive(false);
    }

    private void Notification(AchievementDTO achievement)
    {
        gameObject.SetActive(true);
        Refresh(achievement);
        StartCoroutine(FadeOutCoroutine());
    }

    private void Refresh(AchievementDTO achievement)
    {
        _nameTMP.text = $"[{achievement.Name}] 업적 달성!";
        _descriptionTMP.text = achievement.Description;
    }

    private IEnumerator FadeOutCoroutine()
    {
        float time = 0f;
        float startAlpha = _canvasGroup.alpha;
        while (time < _fadeOutDuration)
        {
            _canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, time / _fadeOutDuration);
            time += Time.deltaTime;
            yield return null;
        }
        _canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }
}