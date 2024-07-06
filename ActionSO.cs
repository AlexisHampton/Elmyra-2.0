using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAction", menuName = "Action", order = 1)]
public class ActionSO : ScriptableObject {
    [SerializeField] private List<TaskSO> taskSOs;

    public List<TaskSO> GetTaskSOs { get => taskSOs; }

}
