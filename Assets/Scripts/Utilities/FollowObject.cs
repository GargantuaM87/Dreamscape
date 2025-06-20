using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private Transform target; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x - 8f, transform.position.y, target.position.z - 8f);
    }
}
