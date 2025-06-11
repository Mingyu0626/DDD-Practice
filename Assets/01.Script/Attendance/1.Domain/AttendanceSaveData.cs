using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttendanceSaveData
{
    public string ID;
    public bool RewardClaimed;

    public AttendanceSaveData()
    {
        ID = string.Empty;
        RewardClaimed = false;
    }
    public AttendanceSaveData(Attendance attendance)
    {
        ID = attendance.ID;
        RewardClaimed = attendance.RewardClaimed;
    }
    public AttendanceSaveData(AttendanceDTO attendanceDto)
    {
        ID = attendanceDto.ID;
        RewardClaimed = attendanceDto.RewardClaimed;
    }
}

[Serializable]
public class AttendanceSaveDataList
{
    public List<AttendanceSaveData> DataList;
    public AttendanceSaveDataList()
    {
        DataList = new List<AttendanceSaveData>();
    }
    public AttendanceSaveDataList(List<AttendanceDTO> attendances)
    {
        DataList = new List<AttendanceSaveData>();
        foreach (var attendance in attendances)
        {
            DataList.Add(new AttendanceSaveData(attendance));
        }
    }
}
