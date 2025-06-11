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
    public string Email; // �̸���
    public int TotalAttendanceCount; // ���� �⼮ �ϼ�
    public DateTime LastAttendanceDate; // ������ �⼮ ��¥
    public int CurrentStreakCount; // ���� �⼮ ī��Ʈ
    public int MaxStreakCount; // �ִ� ���� �⼮ ī��Ʈ
    public int ClaimRewardCount; // ȹ���� �Ϲ� ���� ��
    public int ClaimStreakRewardCount; // ȹ���� ���� ���� ��

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
