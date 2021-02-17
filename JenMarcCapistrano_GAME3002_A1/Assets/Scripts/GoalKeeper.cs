using UnityEngine.Assertions;
using UnityEngine;

public class GoalKeeper : MonoBehaviour
{
    public float begin;
    public float dist = 5;
    public float speed = 5;
    public int dir;
    
    // Start is called before the first frame update
    void Start()
    {
        begin = transform.position.x;
        dir= 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > begin+dist)
        {
            dir = -1;
        }
        else if (transform.position.x < begin-dist)
        {
            dir = 1;
        }

        transform.position = new Vector3(transform.position.x + Time.deltaTime * speed * dir, transform.position.y, transform.position.z);
    }
}
