using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DailyAttendanceSaveData
{
    public string ID;
    public bool RewardClaimed;

    public DailyAttendanceSaveData()
    {
        ID = string.Empty;
        RewardClaimed = false;
    }
    public DailyAttendanceSaveData(Attendance attendance)
    {
        ID = attendance.ID;
        RewardClaimed = attendance.RewardClaimed;
    }
    public DailyAttendanceSaveData(DailyAttendanceDTO attendanceDto)
    {
        ID = attendanceDto.ID;
        RewardClaimed = attendanceDto.RewardClaimed;
    }
}

[Serializable]
public class AttendanceSaveData
{
    public List<DailyAttendanceSaveData> DailyAttendances;
    public List<DailyAttendanceSaveData> StreakAttendances;
    public AttendanceHistory AttendanceInfo;
    public AttendanceSaveData()
    {
        DailyAttendances = new List<DailyAttendanceSaveData>();
        StreakAttendances = new List<DailyAttendanceSaveData>();
    }
    public AttendanceSaveData(
        List<DailyAttendanceDTO> dailyAttendances,
        List<DailyAttendanceDTO> streakAttendances,
        AttendanceHistory history)
    {
        DailyAttendances = new List<DailyAttendanceSaveData>();
        foreach (var attendance in dailyAttendances)
        {
            DailyAttendances.Add(new DailyAttendanceSaveData(attendance));
        }

        StreakAttendances = new List<DailyAttendanceSaveData>();
        foreach (var streak in streakAttendances)
        {
            StreakAttendances.Add(new DailyAttendanceSaveData(streak));
        }

        AttendanceInfo = history;
    }
}
