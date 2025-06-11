using System;
using UnityEngine;

public class AttendanceRepository
{
    private const string SAVE_PREFIX = "ATTENDANCE_";
    public void Save(string email, AttendanceDTO attendanceDto)
    {
        AttendanceSaveData attendanceSaveData = new AttendanceSaveData(attendanceDto);
        string json = JsonUtility.ToJson(attendanceSaveData);
        PlayerPrefs.SetString(SAVE_PREFIX + email, json);
    }

    public AttendanceDTO Load(string email)
    {
        if (!PlayerPrefs.HasKey(SAVE_PREFIX + email))
        {
            return null;
        }
        string json = PlayerPrefs.GetString(SAVE_PREFIX + email);
        return JsonUtility.FromJson<AttendanceDTO>(json);
    }
}

public class AttendanceSaveData
{
    public string Email; // 이메일
    public int TotalAttendanceCount; // 누적 출석 일수
    public DateTime LastAttendanceDate; // 마지막 출석 날짜
    public int CurrentStreakCount; // 연속 출석 카운트
    public int MaxStreakCount; // 최대 연속 출석 카운트
    public int ClaimRewardCount; // 획득한 일반 보상 수
    public int ClaimStreakRewardCount; // 획득한 연속 보상 수

    public AttendanceSaveData(AttendanceDTO attendanceDto)
    {
        Email = attendanceDto.Email;
        TotalAttendanceCount = attendanceDto.TotalAttendanceCount;
        LastAttendanceDate = attendanceDto.LastAttendanceDate;
        CurrentStreakCount = attendanceDto.CurrentStreakCount;
        MaxStreakCount = attendanceDto.MaxStreakCount;
        ClaimRewardCount = attendanceDto.ClaimRewardCount;
        ClaimStreakRewardCount = attendanceDto.ClaimStreakRewardCount;
    }
}
