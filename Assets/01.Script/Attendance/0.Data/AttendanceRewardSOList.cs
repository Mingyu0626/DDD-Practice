using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttendanceSOList", menuName = "Scriptable Objects/AttendanceSOList")]
public class AttendanceRewardSOList : ScriptableObject
{
    [SerializeField]
    private List<AttendanceRewardSO> _attendances = new List<AttendanceRewardSO>();
    public List<AttendanceRewardSO> Attendances => _attendances;
}
