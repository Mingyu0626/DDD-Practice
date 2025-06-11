using System;
using UnityEngine;

[Serializable]
public class AttendanceDTO
{
    public string Email; // �̸���
    public int TotalAttendanceCount; // ���� �⼮ �ϼ�
    public DateTime LastAttendanceDate; // ������ �⼮ ��¥
    public int CurrentStreakCount; // ���� �⼮ ī��Ʈ
    public int MaxStreakCount; // �ִ� ���� �⼮ ī��Ʈ
    public int ClaimRewardCount; // ȹ���� �Ϲ� ���� ��
    public int ClaimStreakRewardCount; // ȹ���� ���� ���� ��

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
