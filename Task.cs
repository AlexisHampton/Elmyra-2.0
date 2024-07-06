using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//used for needs that will be impacted by this task
//will likely replace energyGained
[System.Serializable]
public class TaskNeed {
    public NeedType needType;
    public float reductionAmount;
}

public abstract class Task : MonoBehaviour {

    [SerializeField] protected string taskName;
    [SerializeField] protected float energyGained;
    [SerializeField] protected List<TaskNeed> taskNeeds = new List<TaskNeed>();
    [SerializeField] protected TaskSO taskSO;


    public string GetTaskName { get => taskName; }
    public TaskSO GetTaskSO { get => taskSO; }
    public float GetEnergyGained { get => energyGained; }
    public List<TaskNeed> GetTaskNeeds { get => taskNeeds; }

    protected Vector3 location;
    protected NPC npc;

    private void Awake() {
        location = transform.position;
    }

    public Task DoTask(NPC npcIn) {
        npc = npcIn;
        CarryOutTask();
        return this;
    }

    public float GetTaskScore(NPC npc) {
        float distance = (location - npc.transform.position).magnitude;
        return -distance + energyGained;
    }
    public bool IsTaskOccupied() {
        return npc != null;
    }

    public abstract bool CheckIfCanDoTask(NPC npc);
    protected abstract Task GetAdjacentTask();

    protected virtual void CarryOutTask() {

        Debug.Log(npc.name + " finished " + taskName + " ");
        npc.NextTask();
        npc = null;
    }

}
