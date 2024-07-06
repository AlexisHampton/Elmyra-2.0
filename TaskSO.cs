using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A task signature used to find tasks in the world
[CreateAssetMenu(fileName = "NewTaskSO", menuName = "Task", order = 0)]
public class TaskSO : ScriptableObject {

    //the need that is satisfied by the task
    [SerializeField] private NeedType needFulfilled;
    //the personality that would be attracted to these actions, 
    //not implemented yet!
    [SerializeField] private PersonalityType personalityType;

    public NeedType GetNeedFulfilled { get => needFulfilled; }
    public PersonalityType GetPersonalityType { get => personalityType; }



    public override bool Equals(object other) {
        if (other is not TaskSO) return false;
        TaskSO task = (TaskSO)other;
        if (task.needFulfilled != needFulfilled) return false;

        //if either accepts all personalities, we don't need to check
        if (task.personalityType == PersonalityType.ALL || task.personalityType == PersonalityType.ALL) return true;
        if (task.personalityType != personalityType) return false;


        return true;
    }
}
