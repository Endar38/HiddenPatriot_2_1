using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KameraKontrol : MonoBehaviour
{

    public Transform player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private bool isFollowingPlayer = true; // Defaultnya kamera mengikuti pemain

    private void FixedUpdate()
    {
        if (isFollowingPlayer)
        {
            // Tetapkan nilai z kamera agar tetap sama
            Vector3 desiredPosition = player.position + offset;
            desiredPosition.z = transform.position.z;

            // Mengikuti pemain dengan efek smoothing
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }

    public void SetFollowPlayer(bool follow)
    {
        isFollowingPlayer = follow;
    }
}
