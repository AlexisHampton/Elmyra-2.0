using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public enum PersonalityType { BRAVE, LOVING, KIND, CURIOUS, ALL }

public class NPC : MonoBehaviour {

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private NPCNeeds[] npcNeeds = new NPCNeeds[4];
    [SerializeField] private ActionSO action;

    private Task currTask;
    private bool hasStartedTask;
    NeedType lowestNeed;

    private float needsTimer = 0;

    void Start() {
        string msg = name + " ";
        //init all the npc needs
        for (int i = 0; i < npcNeeds.Length; i++) {
            npcNeeds[i] = new NPCNeeds((NeedType)i, Random.Range(50, 90));
            msg += npcNeeds[i].ToString() + " ";
        }
        Debug.Log(msg);
        NextTask();
        needsTimer = NPCManager.Instance.GetTimeUntilNextNPCNeedDecrease;
    }

    void Update() {
        needsTimer -= Time.deltaTime;

        if (!hasStartedTask && agent.remainingDistance <= .5f && currTask != null) {
            hasStartedTask = true;
            npcNeeds[(int)lowestNeed].Amount += currTask.GetEnergyGained;
            DecreaseAffectedNeeds(currTask.GetTaskNeeds);
            currTask.DoTask(this);
            //debug printing needs
            string msg = name + " ";
            for (int i = 0; i < npcNeeds.Length; i++) {
                msg += npcNeeds[i].ToString() + " ";
            }
            Debug.Log(msg);
        }
        if (needsTimer <= 0) {
            DecreaseAllNeeds(3);
            needsTimer = NPCManager.Instance.GetTimeUntilNextNPCNeedDecrease;
        }

    }

    public void NextTask() {
        currTask = null;
        hasStartedTask = false;

        //get a needs task
        if (!NeedsAreAlright()) {
            lowestNeed = FindLowestNPCNeed();
            Debug.Log(name + " needs task, low: " + lowestNeed.ToString());
            currTask = NPCManager.Instance.GetNextTask(this, lowestNeed);
        } else {
            Debug.Log(name + " action task: ");
            currTask = NPCManager.Instance.GetNextTask(this, action);
        }

        //go to task area
        if (currTask != null) {
            Debug.Log(name + " going to " + currTask.GetTaskName);
            Move(currTask.transform.position);
        } else {
            Debug.Log("no tasks to do");
        }

    }


    private bool NeedsAreAlright() {
        foreach (NPCNeeds npcNeed in npcNeeds)
            if (npcNeed.Amount < 70)
                return false;
        return true;

    }

    private NeedType FindLowestNPCNeed() {
        NeedType need = NeedType.EAT;
        float best = 101;
        foreach (NPCNeeds npcNeed in npcNeeds) {
            if (best > npcNeed.Amount) {
                need = npcNeed.Need;
                best = npcNeed.Amount;
            }
        }
        return need;
    }

    private void DecreaseAffectedNeeds(List<TaskNeed> taskNeeds) {
        foreach (TaskNeed taskNeed in taskNeeds)
            npcNeeds[(int)taskNeed.needType].Amount -= taskNeed.reductionAmount;
    }

    private void DecreaseAllNeeds(int amount) {
        for (int i = 0; i < npcNeeds.Length; i++)
            npcNeeds[i].Amount -= amount;
    }

    private void Move(Vector3 pos) {
        agent.destination = pos;
    }
}
