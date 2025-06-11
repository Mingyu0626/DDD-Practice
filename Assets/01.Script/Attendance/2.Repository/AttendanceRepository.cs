using System;
using System.Collections.Generic;
using UnityEngine;

public class AttendanceRepository
{
    private const string SAVE_PREFIX = "ATTENDANCE_";
    public void Save(string email, List<AttendanceDTO> dataList)
    {
        AttendanceSaveDataList attendanceSaveDataList = new AttendanceSaveDataList(dataList);
        string json = JsonUtility.ToJson(attendanceSaveDataList);
        PlayerPrefs.SetString(SAVE_PREFIX + email, json);
    }

    public List<AttendanceDTO> Load(string email)
    {
        if (!PlayerPrefs.HasKey(SAVE_PREFIX + email))
        {
            return null;
        }
        string json = PlayerPrefs.GetString(SAVE_PREFIX + email);
        return JsonUtility.FromJson<List<AttendanceDTO>>(json);
    }
}
