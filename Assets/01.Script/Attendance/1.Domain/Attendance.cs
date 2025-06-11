using System;
using UnityEngine;

public class Attendance
{
    public readonly string Email; // 이메일
    public int TotalAttendanceCount; // 누적 출석 일수
    public DateTime LastAttendanceDate; // 마지막 출석 날짜
    public int CurrentStreakCount; // 연속 출석 카운트
    public int MaxStreakCount; // 최대 연속 출석 카운트
    public int ClaimRewardCount; // 획득한 일반 보상 수
    public int ClaimStreakRewardCount; // 획득한 연속 보상 수

    public Attendance(
    string email,
    int totalAttendanceCount,
    DateTime lastAttendanceDate,
    int currentStreakCount,
    int maxStreakCount,
    int claimRewardCount,
    int claimStreakRewardCount)
    {
        Email = email;
        TotalAttendanceCount = totalAttendanceCount;
        LastAttendanceDate = lastAttendanceDate;
        CurrentStreakCount = currentStreakCount;
        MaxStreakCount = maxStreakCount;
        ClaimRewardCount = claimRewardCount;
        ClaimStreakRewardCount = claimStreakRewardCount;
    }

    public Attendance(AttendanceDTO attendanceDto)
    {
        Email = attendanceDto.Email;
        TotalAttendanceCount = attendanceDto.TotalAttendanceCount;
        LastAttendanceDate = attendanceDto.LastAttendanceDate;
        CurrentStreakCount = attendanceDto.CurrentStreakCount;
        MaxStreakCount = attendanceDto.MaxStreakCount;
        ClaimRewardCount = attendanceDto.ClaimRewardCount;
    }

    public bool CanGetReward(int day)
    {
        return false;
    }
    public AttendanceDTO ToDTO()
    {
        return new AttendanceDTO(this);
    }
}
