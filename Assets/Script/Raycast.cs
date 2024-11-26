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
    public bool yolBos1 = true;
    public bool yolBos2 = true;
    public bool yolBos3 = true;

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

            GameObject go = hit.collider.gameObject;

            LightSystemSC lamba = go.GetComponent<LightSystemSC>();

            crosswalksc script = go.GetComponent<crosswalksc>();

            if (lamba != null)
            {
                if (lamba.red == false)
                {
                    yolBos1 = true;
                    Debug.Log("menzilde yesil");
                }
                else
                {
                    yolBos1 = false;
                }

            }


            if (script != null)
            {
                if (script.x == 0)
                {
                    yolBos2 = true;
                    Debug.Log("yaya yok bir");
                }
                else
                {
                    yolBos2 = false;
                }

            }
            else
            {
                //yolBos2 = true;

            }
            if (hitLayer == 7 || hitLayer == 8)
            {
                if (distance < 8.25f)
                {
                        player.isSlowingDown = true;
                        yolBos3 = false;
                }

            }
            else
            {


                if (player.currentSpeed < player.maxSpeed && yolBos1 && yolBos2 && yolBos3)
                {
                    player.isAccelerating = true;
                }

               
            }

            yolBos1 = true;
            yolBos2 = true;
            yolBos3 = true;

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
