using UnityEngine;

public class Week8 : MonoBehaviour
{
    public GameObject cube;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (cube == null)
            Debug.Log("No GameObject");
        else
        {
            for(int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    for(int k =  0; k < 10; k++)
                    {
                        var go = Instantiate(cube, new Vector3(i * 2, j * 2, k * 2), Quaternion.identity);
                        go.AddComponent<Rigidbody>();
                        float temp = Random.Range(1f, 4f);
                        Destroy(go, temp);
                    }
                }
            }
            
        }   
    }

    // Update is called once per frame
    void Update()
    {
    }
}
