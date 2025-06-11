using System;
using UnityEngine;

public class Attendance
{
    public readonly string Email; // �̸���
    public int TotalAttendanceCount; // ���� �⼮ �ϼ�
    public DateTime LastAttendanceDate; // ������ �⼮ ��¥
    public int CurrentStreakCount; // ���� �⼮ ī��Ʈ
    public int MaxStreakCount; // �ִ� ���� �⼮ ī��Ʈ
    public int ClaimRewardCount; // ȹ���� �Ϲ� ���� ��
    public int ClaimStreakRewardCount; // ȹ���� ���� ���� ��

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
