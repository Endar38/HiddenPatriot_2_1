using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObjDrag : MonoBehaviour, IPointerDownHandler, IDragHandler
{

    public RectTransform pivot; // Tentukan pivot untuk rotasi
    public float rotationSpeed = 1f; // Kecepatan rotasi
    public float maxRotationAngle = 45f; // Sudut rotasi maksimum
    public float maxRotationAngle2 = -45f;
    public int arahObj = -1;

    private bool isDragging = false;
    private Vector2 startVector;

    public Animator anim;
    public GameObject penutup;
    public AudioSource sourcef;

    public GameObject canv;
    public Camera uiCam;

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        startVector = ConvertToCanvasLocalPoint(eventData.position) - (Vector2)pivot.localPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            Vector2 currentVector = ConvertToCanvasLocalPoint(eventData.position) - (Vector2)pivot.localPosition;
            float angle = Vector2.SignedAngle(startVector, currentVector);

            float newAngle = transform.rotation.eulerAngles.z + angle * rotationSpeed;
            newAngle = (newAngle > 180) ? newAngle - 360 : newAngle;

            // Limit rotation based on maxRotationAngle and maxRotationAngle2
            if (Mathf.Abs(newAngle) >= maxRotationAngle && Mathf.Abs(newAngle) < 180f)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Sign(newAngle) * maxRotationAngle);
                isDragging = false;
                if (arahObj == -1)
                {
                    if (!sourcef.isPlaying)
                    {
                        sourcef.Play();
                    }
                    penutup.SetActive(true);
                    anim.SetBool("endObj", true);
                }
            }
            else if (Mathf.Abs(newAngle) <= maxRotationAngle2 && Mathf.Abs(newAngle) > 180f)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Sign(newAngle) * maxRotationAngle2);
                isDragging = false;
                if (arahObj == 1)
                {
                    if (sourcef.isPlaying)
                    {
                        sourcef.Stop();
                    }
                    penutup.SetActive(true);
                    anim.SetBool("endObj", true);
                }
            }
            else
            {
                transform.Rotate(Vector3.forward, angle * rotationSpeed);
                startVector = currentVector;
            }
        }
    }

    private Vector2 ConvertToCanvasLocalPoint(Vector2 screenPosition)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canv.transform as RectTransform, screenPosition, uiCam, out localPoint);
        return localPoint;
    }
}