using UnityEngine;

public class Player : MonoBehaviour
{

    void AddPoint()
    {
        Debug.Log("Plus one point!!!!");
    }

    void Die()
    {
        Debug.Log("Bleh");
    }

    void OnCollisionEnter(Collision collision)
    {
        Die();
    }

    void OnTriggerEnter(Collider other)
    {
        AddPoint();
    }
}
