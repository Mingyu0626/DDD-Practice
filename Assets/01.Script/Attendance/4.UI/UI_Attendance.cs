using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_Attendance : MonoBehaviour
{
    [SerializeField]
    private List<UI_AttendanceSlot> _slots;
    [SerializeField]
    private GameObject _attendacneSlotUIPrefab;

    private void Start()
    {
        
    }

    private void Init()
    {
        foreach (var slot in _slots)
        {
            slot.
        }
    }

}
