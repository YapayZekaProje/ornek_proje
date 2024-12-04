using Unity.Loading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.LowLevel;

public class RaycastDistance : MonoBehaviour
{
    public float maxDistance;
    public Transform rayOrigin;
    RaycastHit hit;
    Player player;

    float nullTimer = 0f; // null olan script için bir zamanlayıcı
    float nullThreshold = 8f; // "null" süresi  saniye olarak ayarlanir


    private void Start()
    {
        player = FindAnyObjectByType<Player>();
    }

    private void Update()
    {

        StartRaycast();

    }

    private void StartRaycast()
    {
        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);   //rayi firlat
        Debug.DrawRay(rayOrigin.position, rayOrigin.forward * maxDistance, Color.green);

        RaycastHit[] hits = Physics.RaycastAll(ray, maxDistance, ~0, QueryTriggerInteraction.Collide); //rayin carptigi seyleri tut

        if (hits.Length==0)
        {
             
                nullTimer += Time.deltaTime; // null olduğunda zamanlayıcıyı artır

                if (nullTimer >= nullThreshold)
                {

                    Debug.LogWarning("yaya yok (null bekleme süresi doldu)");
                    player.isAccelerating = true; // hızlan
                    nullTimer = 0f; // zamanlayıcıyı sıfırla

                }
           
            
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
                    if (distance < 10f)
                    {
                        player.isAccelerating = false;
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
                    if (distance < 10f)
                    {
                        Debug.Log("yaya var");
                        player.isAccelerating = false;
                        player.isSlowingDown = true;
                        break;
                    }
                }

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
