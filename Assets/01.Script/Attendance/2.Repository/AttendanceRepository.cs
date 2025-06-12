using System;
using System.Collections.Generic;
using UnityEngine;

public class AttendanceRepository
{
    private const string SAVE_PREFIX = "ATTENDANCE_";

    public void Save(string email,
        List<DailyAttendanceDTO> dailyAttendanceList,
        List<DailyAttendanceDTO> streakAttendanceList,
        AttendanceHistory history)
    {
        AttendanceSaveData saveData = new AttendanceSaveData(dailyAttendanceList, streakAttendanceList, history);
        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(SAVE_PREFIX + email, json);
    }

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
