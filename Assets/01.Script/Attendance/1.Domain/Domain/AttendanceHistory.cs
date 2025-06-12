using System;
using UnityEngine;

public struct AttendanceHistory
{
    public DateTime LastAttendanceDate;
    public int AttendanceCount;
    public int MaxStreakAttendanceCount;
    public int CurrentStreakAttendanceCount;

    public AttendanceHistory(AttendanceHistoryDTO attendanceHistoryDTO)
    {
        LastAttendanceDate = attendanceHistoryDTO.LastAttendanceDate;
        AttendanceCount = attendanceHistoryDTO.AttendanceCount;
        MaxStreakAttendanceCount = attendanceHistoryDTO.MaxStreakAttendanceCount;
        CurrentStreakAttendanceCount = attendanceHistoryDTO.CurrentStreakAttendanceCount;
    }
}

