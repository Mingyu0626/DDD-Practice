using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttendanceSOList", menuName = "Scriptable Objects/AttendanceSOList")]
public class AttendanceSOList : ScriptableObject
{
    [SerializeField]
    private List<AttendanceSO> _attendances = new List<AttendanceSO>();
    public List<AttendanceSO> Attendances => _attendances;
}
