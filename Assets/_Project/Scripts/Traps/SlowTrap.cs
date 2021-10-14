using UnityEngine;

public class SlowTrap : AOETrap
{
    public Slow slow;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().DecreaseSpeed(slow.slowSpeed);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<Enemy>().ResetSpeed();
            }
        }
    }
}
