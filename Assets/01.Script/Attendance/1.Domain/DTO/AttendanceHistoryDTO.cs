using System;
using UnityEngine;

[Serializable]
public class AttendanceHistoryDTO
{
    public DateTime LastAttendanceDate;
    public int AttendanceCount;
    public int MaxStreakAttendanceCount;
    public int CurrentStreakAttendanceCount;

    public AttendanceHistoryDTO(AttendanceHistory info)
    {
        LastAttendanceDate = info.LastAttendanceDate;
        AttendanceCount = info.AttendanceCount;
        MaxStreakAttendanceCount = info.MaxStreakAttendanceCount;
        CurrentStreakAttendanceCount = info.CurrentStreakAttendanceCount;
    }
}
