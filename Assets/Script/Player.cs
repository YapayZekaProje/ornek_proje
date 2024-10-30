using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    private float speed = 2;
    private float moveSpeed = 5;

    public void GidilcekYer(Vector3 hedefNoktasi)
    {
        ++hedefNoktasi.y;
        transform.position = Vector3.MoveTowards(transform.position, hedefNoktasi, speed * Time.deltaTime);

    }

    public void LookToTarget(Vector3 target)
    {
        Vector3 direction = target - transform.position;

        // Eğer hedefe çok yakın değilse
        if (direction.magnitude > 0.1f)
        {
            direction.y = 0;  // y ekseninde dönmeyi engelliyoruz

            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Mevcut rotasyonu al ve yalnızca Y eksenini ayarla
            Vector3 currentRotation = transform.rotation.eulerAngles;
            targetRotation = Quaternion.Euler(currentRotation.x, targetRotation.eulerAngles.y, currentRotation.z);

            // Arabayı sadece Y ekseninde hedefe bakacak şekilde yumuşakça döndür
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);
        }
    }

    private void GameKontrol()
    {
        Vector3 newPosition = transform.position;

        // Yukarı ok tuşu z ekseninde artırma yapar
        if (Input.GetKey(KeyCode.UpArrow))
        {
            newPosition.z += moveSpeed * Time.deltaTime;
        }

        // Aşağı ok tuşu z ekseninde azaltma yapar
        if (Input.GetKey(KeyCode.DownArrow))
        {
            newPosition.z -= moveSpeed * Time.deltaTime;
        }

        // Sağ ok tuşu x ekseninde artırma yapar
        if (Input.GetKey(KeyCode.RightArrow))
        {
            newPosition.x += moveSpeed * Time.deltaTime;
        }

        // Sol ok tuşu x ekseninde azaltma yapar
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition.x -= moveSpeed * Time.deltaTime;
        }

        // Yeni pozisyonu güncelle
        transform.position = newPosition;

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
            ++speed;
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
            --speed;
    }

    private void Update()
    {
        GameKontrol();
    }
}
