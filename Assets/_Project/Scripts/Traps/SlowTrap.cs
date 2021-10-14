using UnityEngine;

public class SlowTrap : AOETrap
{
    public Slow slow;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !isDragging)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.DecreaseSpeed(slow.slowSpeed);

           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && !isDragging)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.ResetSpeed();
        }
    }
}
