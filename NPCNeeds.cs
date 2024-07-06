using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum NeedType { EAT, HYGIENE, FUN, SLEEP }

[System.Serializable]
public class NPCNeeds {

    private NeedType need = NeedType.EAT;
    private float amount;

    public NeedType Need {
        get => need;
        set => need = value;
    }
    public float Amount {
        get => amount;
        set => amount = Mathf.Clamp(value, 0, 100);
    }

    public NPCNeeds(NeedType needType, float amt) {
        need = needType;
        amount = amt;
    }

    public override string ToString() {
        return need.ToString() + ": " + amount;
    }

}
