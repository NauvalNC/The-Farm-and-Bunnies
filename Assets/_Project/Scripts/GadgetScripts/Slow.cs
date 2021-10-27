using UnityEngine;
[CreateAssetMenu(fileName = "Explosive", menuName = "Farm and Bunnies/New Gadget/Slow", order = 2)]
public class Slow : Gadget
{
    [Header("Stats")]
    public float AOE;
    public int slowSpeed;
    public float slowDuration;
}
