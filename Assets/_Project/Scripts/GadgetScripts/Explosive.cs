using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Explosive", menuName = "Farm and Bunnies/New Gadget/Explosive", order = 1)]
public class Explosive : Gadget
{
    [Header("Stats")]
    public int DMG;
    public int AOE;

}
