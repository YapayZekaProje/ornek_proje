using UnityEngine;

public class RaycastDistance : MonoBehaviour
{
    public float maxDistance;
    public Transform rayOrigin;
    RaycastHit hit;
    Pathfinding pathfinding;
    Player player;


    private void Start()
    {
        pathfinding = FindObjectOfType<Pathfinding>();
        player = FindAnyObjectByType<Player>();
    }

    private void Update()
    {

        StartRaycast();

    }

    private void StartRaycast()
    {
        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);   //rayi firlat

        RaycastHit[] hits = Physics.RaycastAll(ray, maxDistance, ~0, QueryTriggerInteraction.Collide); //rayin carptigi seyleri tut


        foreach (RaycastHit hit in hits)
        {

            float distance = hit.distance;

            int hitLayer = hit.collider.gameObject.layer;

            if (hitLayer == 7 || hitLayer == 8)
            {

                if (distance < 8.25f)
                {
                    //  pathfinding.driveable = false;
                    player.isSlowingDown = true;
                    break;

                }

            }
            else
            {
                player.isAccelerating = true;
            }

            if (player.maxSpeed != player.currentSpeed)
            {
                player.isAccelerating = true;
            }
        }
        
        Debug.DrawRay(rayOrigin.position, rayOrigin.forward * maxDistance, Color.green);


        //if (Physics.Raycast(ray, out hit, maxDistance))
        //{
        //    float distance = hit.distance;

        //    Collider hitCollider = hit.collider;

        //    if (hitCollider != null)
        //    {
        //        int hitLayer = hitCollider.gameObject.layer;

        //        if (hitLayer == 7 || hitLayer == 8)
        //      {
        //    if (distance < 8.25f)
        //    {
        //        //  pathfinding.driveable = false;
        //        player.isSlowingDown = true;

        //    }

        //}
        //        else
        //        {
        //          //  pathfinding.driveable = true;
        //            player.isAccelerating = true;
        //        }

        //    }
        //}
        //else
        //{
        //    if (player.maxSpeed != player.currentSpeed)
        //    {
        //        player.isAccelerating = true;
        //    }
        //    //pathfinding.driveable = true;
        //    Debug.DrawRay(rayOrigin.position, rayOrigin.forward * maxDistance, Color.green);
        //}



    }
}
