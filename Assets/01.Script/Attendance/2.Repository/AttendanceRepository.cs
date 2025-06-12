using System;
using System.Collections.Generic;
using UnityEngine;

public class AttendanceRepository
{
    private const string SAVE_PREFIX = "ATTENDANCE_";

    // SaveData 하나로 DailyAttendances, StreakAttendances, AttendanceInfo 모두 저장
    public void Save(string email,
        List<DailyAttendanceDTO> dailyAttendanceList,
        List<DailyAttendanceDTO> streakAttendanceList,
        AttendanceHistory history)
    {
        AttendanceSaveData saveData = new AttendanceSaveData(dailyAttendanceList, streakAttendanceList, history);
        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(SAVE_PREFIX + email, json);
    }

    // Load 시 AttendanceSaveData 통째로 받아서 내부 리스트 반환
    public (List<DailyAttendanceSaveData> dailyAttendances, List<DailyAttendanceSaveData> streakAttendances, AttendanceHistory history) Load(string email)
    {
        if (!PlayerPrefs.HasKey(SAVE_PREFIX + email))
        {
            return (null, null, default);
        }

        string json = PlayerPrefs.GetString(SAVE_PREFIX + email);
        AttendanceSaveData saveData = JsonUtility.FromJson<AttendanceSaveData>(json);

        return (saveData.DailyAttendances, saveData.StreakAttendances, saveData.AttendanceInfo);
    }
}
