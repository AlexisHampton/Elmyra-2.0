using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Goal", menuName = "Goal", order = 2)]
public class GoalSO : ScriptableObject {
    [SerializeField] private List<ActionSO> actions;

    public List<ActionSO> GetActionSOs { get => actions; }
}
