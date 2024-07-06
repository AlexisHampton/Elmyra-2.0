using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour {
    private static NPCManager instance;
    public static NPCManager Instance { get => instance; }


    [SerializeField] private float taskRadius = 100f;
    [SerializeField] private LayerMask taskMask;
    [SerializeField] private float timeUntilNextNPCNeedDecrease = 34;

    public float GetTimeUntilNextNPCNeedDecrease { get => timeUntilNextNPCNeedDecrease; }

    private void Awake() {
        instance = this;
    }

    public Task GetNextTask(NPC npc, NeedType lowest) {
        List<Task> possibleTasks = new List<Task>();


        //find needs within a range that match the need
        RaycastHit[] hits = Physics.SphereCastAll(npc.transform.position, taskRadius, Vector3.forward, 0);

        Debug.Log("hits: " + hits.Length);

        foreach (RaycastHit hit in hits) {
            Task newTask = hit.collider.GetComponentInParent<Task>();
            if (newTask != null && newTask.GetTaskSO.GetNeedFulfilled == lowest) {
                Debug.Log(npc.name + " found task: " + newTask);
                possibleTasks.Add(newTask);
            }

        }

        //find the best one 
        return FindBestTask(possibleTasks, npc);
    }


    //goal tasks
    public Task GetNextTask(NPC npc, ActionSO action) {

        List<Task> possibleTasks = new List<Task>();

        //find needs within a range
        RaycastHit[] hits = Physics.SphereCastAll(npc.transform.position, taskRadius, Vector3.forward, 0);

        foreach (RaycastHit hit in hits) {
            Task newTask = hit.collider.GetComponentInParent<Task>();
            if (newTask != null) {
                foreach (TaskSO taskSO in action.GetTaskSOs) {
                    if (taskSO.Equals(newTask.GetTaskSO)) {
                        possibleTasks.Add(newTask);
                        break;
                    }
                }
            }
        }

        return FindBestTask(possibleTasks, npc);
        //needs to match task signature in Action
    }

    private Task FindBestTask(List<Task> possibleTasks, NPC npc) {
        if (possibleTasks.Count == 0) return null;
        float bestScore = possibleTasks[0].GetTaskScore(npc);
        Task bestTask = possibleTasks[0];

        foreach (Task task in possibleTasks) {
            float taskScore = task.GetTaskScore(npc);
            if (taskScore > bestScore) {
                bestScore = taskScore;
                bestTask = task;
            }
        }

        if (!bestTask.CheckIfCanDoTask(npc)) {
            possibleTasks.Remove(bestTask);
            return FindBestTask(possibleTasks, npc);
        }
        return bestTask;
    }

}
