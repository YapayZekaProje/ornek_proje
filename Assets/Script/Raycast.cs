using UnityEngine;

public class RaycastDistance : MonoBehaviour
{
    public float maxDistance = 100f;             
    public Transform rayOrigin;                  
    RaycastHit hit;
    Pathfinding pathfinding;

    private void Start()
    {
        pathfinding = FindObjectOfType<Pathfinding>();
    }
    private void Update()
    {

        StartRaycast();

    }

    private void StartRaycast()
    {
        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            float distance = hit.distance;

            Collider hitCollider = hit.collider;

            if (hitCollider != null) 
            {
                int hitLayer = hitCollider.gameObject.layer;

                if (hitLayer == 7 || hitLayer==8 ) 
                {
                    if (distance < 1.25f) 
                    pathfinding.driveable = false; 
                }
                else
                {
                    pathfinding.driveable = true;   
                }
          
            }
        }
        else
        {
            pathfinding.driveable = true;

            Debug.DrawRay(rayOrigin.position, rayOrigin.forward * maxDistance, Color.green);
        }
       


    }
}
