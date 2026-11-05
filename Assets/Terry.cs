using UnityEngine;

public class Terry : MonoBehaviour
{
    public GameObject deathParticles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        var go = Instantiate(deathParticles, this.transform.position, Quaternion.identity);
        Destroy(go, 1f);
    }
}
