using System;
using UnityEngine;

[Serializable]
public class AttendanceDTO
{
    public string Email; // 이메일
    public int TotalAttendanceCount; // 누적 출석 일수
    public DateTime LastAttendanceDate; // 마지막 출석 날짜
    public int CurrentStreakCount; // 연속 출석 카운트
    public int MaxStreakCount; // 최대 연속 출석 카운트
    public int ClaimRewardCount; // 획득한 일반 보상 수
    public int ClaimStreakRewardCount; // 획득한 연속 보상 수

    public AttendanceDTO(Attendance attendance)
    {
        Email = attendance.Email;
        TotalAttendanceCount = attendance.TotalAttendanceCount;
        LastAttendanceDate = attendance.LastAttendanceDate;
        CurrentStreakCount = attendance.CurrentStreakCount;
        MaxStreakCount = attendance.MaxStreakCount;
        ClaimRewardCount = attendance.ClaimRewardCount;
        ClaimStreakRewardCount = attendance.ClaimStreakRewardCount;
    }

    public AttendanceDTO(AttendanceSaveData attendanceSaveData)
    {
        Email = attendanceSaveData.Email;
        TotalAttendanceCount = attendanceSaveData.TotalAttendanceCount;
        LastAttendanceDate = attendanceSaveData.LastAttendanceDate;
        CurrentStreakCount = attendanceSaveData.CurrentStreakCount;
        MaxStreakCount = attendanceSaveData.MaxStreakCount;
        ClaimRewardCount = attendanceSaveData.ClaimRewardCount;
        ClaimStreakRewardCount = attendanceSaveData.ClaimStreakRewardCount;
    }
}
