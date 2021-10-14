using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gadget", menuName = "Farm and Bunnies/New Gadget", order = 1)]
public class Gadget : ScriptableObject
{
    [Header("Details")]
    public string gadgetName;
    [TextArea] public string gadgetDescription;
    public Sprite gadgetIcon;
    public GameObject prefab;

    [Header("Mandatory")]
    public int DPCost;
    public int CD;

    public virtual void ExecGadget()
    {
        Debug.Log("Gadget activated!");
    }
}
