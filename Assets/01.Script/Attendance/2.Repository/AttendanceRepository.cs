using System;
using System.Collections.Generic;
using UnityEngine;

public class AttendanceRepository
{
    private const string SAVE_PREFIX = "ATTENDANCE_";
    private const string SAVE_STREAKPREFIX = "STREAK_ATTENDANCE_";

    public void Save(string email, List<AttendanceDTO> attendanceList, List<AttendanceDTO> streakAttendanceList)
    {
        AttendanceSaveDataList attendanceSaveDataList = new AttendanceSaveDataList(attendanceList);
        string json = JsonUtility.ToJson(attendanceSaveDataList);
        PlayerPrefs.SetString(SAVE_PREFIX + email, json);

        AttendanceSaveDataList streakAttendanceSaveDataList = new AttendanceSaveDataList(streakAttendanceList);
        string json2 = JsonUtility.ToJson(streakAttendanceSaveDataList);
        PlayerPrefs.SetString(SAVE_STREAKPREFIX + email, json2);
    }

    public (List<AttendanceSaveData>, List<AttendanceSaveData>) Load(string email)
    {
        if (!PlayerPrefs.HasKey(SAVE_PREFIX + email) || !PlayerPrefs.HasKey(SAVE_STREAKPREFIX + email))
        {
            return (null, null);
        }
        string json = PlayerPrefs.GetString(SAVE_PREFIX + email);
        AttendanceSaveDataList attendanceSaveDataList = JsonUtility.FromJson<AttendanceSaveDataList>(json);

        string json2 = PlayerPrefs.GetString(SAVE_STREAKPREFIX + email);
        AttendanceSaveDataList streakAttendanceSaveDataList = JsonUtility.FromJson<AttendanceSaveDataList>(json2);

        return (attendanceSaveDataList.DataList, streakAttendanceSaveDataList.DataList);
    }
}
