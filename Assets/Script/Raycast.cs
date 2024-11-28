using Unity.Loading;
using UnityEngine;
using UnityEngine.LowLevel;

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

        if (hits.Length == 0)
        {
            player.isAccelerating = true;
        }

        foreach (RaycastHit hit in hits)
        {

            float distance = hit.distance;

            int hitLayer = hit.collider.gameObject.layer;

            GameObject go = hit.collider.gameObject;

            LightSystemSC lamba = go.GetComponent<LightSystemSC>();

            crosswalksc script = go.GetComponent<crosswalksc>();
            
    

            if (lamba != null)
            {
                if (lamba.red == false)
                {
                    Debug.Log(" yesil");

                }
                else
                {
                    Debug.Log("kirmizi");
                    if (distance < 8.25f)
                    {
                        player.isSlowingDown = true;

                    }
                    break;
                }

            }
            if (script != null)
            {
                if (script.x == 0)
                {
                    Debug.Log("yaya yok ");
                }
                else
                {
                    if (distance < 8.25f)
                    {
                        Debug.Log("yaya var");
                        player.isSlowingDown = true;

                    }
                    break;
                }

            }
            else if (script == null)
            {
                Debug.Log("patladi");
                continue;
            }

            Debug.Log("hizlandim");
            if (player.currentSpeed < player.maxSpeed)
            {
                player.isAccelerating = true;
            }

            //if (hitLayer == 7 || hitLayer == 8)
            //{
            //if (distance < 8.25f)
            //{
            //    player.isSlowingDown = true;

            //}
            //}
            //else
            //{

            //    if (player.currentSpeed < player.maxSpeed && yolBos1 && yolBos2 )
            //    {
            //        player.isAccelerating = true;
            //    }

            //}

            Debug.DrawRay(rayOrigin.position, rayOrigin.forward * maxDistance, Color.green);

            #region Eski raycast
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
            #endregion

        }
        
    }
}
