using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadABookTask : Task {

    public override bool CheckIfCanDoTask(NPC npc) {
        return true;
    }

    protected override Task GetAdjacentTask() {
        throw new System.NotImplementedException();
    }

    protected override void CarryOutTask() {
        Debug.Log(npc.name + " is " + taskName);
        base.CarryOutTask();
    }

}