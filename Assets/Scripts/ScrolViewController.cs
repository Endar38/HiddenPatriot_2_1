using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrolViewController : MonoBehaviour
{

    public GameObject imageObject;
    public GameObject scrollViewObject;
    public RectTransform buttonRot;
    private Vector3 setButtonRotPos;
    private Vector3 setButtonRotScal;
    public float rotationSpeed = 45f; // Kecepatan rotasi image bagi 2
    public float initialImageTransitionSpeed = 180f; // Kecepatan awal transisi untuk image
    public float finalImageTransitionSpeed = 360f; // Kecepatan akhir transisi untuk image
    public float initialScrollViewTransitionSpeed = 90f; // Kecepatan awal transisi untuk ScrollView
    public float finalScrollViewTransitionSpeed = 180f; // Kecepatan akhir transisi untuk ScrollView
    public float delayBetweenTransitions = 0.25f; // Delay antara transisi image dan ScrollView

    private bool isImageActive = true;
    private bool isRotatingImage = false;
    private bool isRotatingScrollView = false;
    private bool putarBalik = false;


    private Quaternion imageStartRotation;
    private Quaternion scrollViewStartRotation;

    void Start()
    {
        setButtonRotPos = buttonRot.localPosition;
        setButtonRotScal = buttonRot.localScale;
        imageStartRotation = imageObject.transform.rotation;
        scrollViewStartRotation = Quaternion.Euler(0f, -90f, 0f);
        scrollViewObject.transform.rotation = scrollViewStartRotation;
    }

    public void FlipImageAndScrollOnClick()
    {
        if (!isRotatingImage && !isRotatingScrollView)
        {
            StartCoroutine(FlipCoroutine());
        }
    }

    IEnumerator FlipCoroutine()
    {
        if (!isImageActive)
        {
            buttonRot.localPosition = setButtonRotPos;
            buttonRot.localScale = setButtonRotScal;
            putarBalik = true;
            yield return StartCoroutine(FlipScrollViewCoroutine());
        }
        else
        {
            buttonRot.localPosition = new Vector3(-235f , buttonRot.localPosition.y, buttonRot.localPosition.z);
            buttonRot.localScale = new Vector3(-0.9f, buttonRot.localScale.y, buttonRot.localScale.z);
            yield return StartCoroutine(FlipImageCoroutine());
        }
    }

    IEnumerator FlipImageCoroutine()
    {
        isRotatingImage = true;

        Quaternion targetImageRotation;
        if (putarBalik)
        {
            targetImageRotation = isImageActive ? Quaternion.Euler(0f, 90f, 0f) : imageStartRotation;
        }
        else
        {
            targetImageRotation = isImageActive ? Quaternion.Euler(0f, 90f, 0f) : imageStartRotation;
        }

        

        float elapsedTime = 0f;
        float currentImageTransitionSpeed = initialImageTransitionSpeed / 2f; // Kecepatan rotasi image dibagi 2

        while (elapsedTime < 0.5f)
        {
            float step = currentImageTransitionSpeed * Time.deltaTime;
            imageObject.transform.rotation = Quaternion.RotateTowards(imageObject.transform.rotation, targetImageRotation, step);

            // Perlahan tambahkan kecepatan rotasi
            currentImageTransitionSpeed = Mathf.Lerp(initialImageTransitionSpeed / 2f, finalImageTransitionSpeed / 2f, elapsedTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isImageActive = !isImageActive;
        imageObject.SetActive(isImageActive);
        isRotatingImage = false;

        yield return new WaitForSeconds(delayBetweenTransitions); // Menunggu sebelum memulai transisi ScrollView

        if (!isImageActive && !putarBalik)
        {
            scrollViewStartRotation = Quaternion.Euler(0f, -90f, 0f);
            StartCoroutine(FlipScrollViewCoroutine());
        }
        else
        {
            putarBalik = false;
        }
    }

    IEnumerator FlipScrollViewCoroutine()
    {
        isRotatingScrollView = true;

        scrollViewObject.SetActive(true);

        Quaternion targetScrollViewRotation;

        if (!putarBalik)
        {
            targetScrollViewRotation = Quaternion.Euler(0f, 0f, 0f);
        }else
        {
            targetScrollViewRotation = Quaternion.Euler(0f, -90f, 0f);
        }

        float elapsedTime = 0f;
        float currentScrollViewTransitionSpeed = initialScrollViewTransitionSpeed;

        while (elapsedTime < 0.5f)
        {
            float step = currentScrollViewTransitionSpeed * Time.deltaTime;
            scrollViewObject.transform.rotation = Quaternion.RotateTowards(scrollViewObject.transform.rotation, targetScrollViewRotation, step);

            // Perlahan tambahkan kecepatan rotasi
            currentScrollViewTransitionSpeed = Mathf.Lerp(initialScrollViewTransitionSpeed, finalScrollViewTransitionSpeed, elapsedTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (putarBalik)
        {
            scrollViewObject.SetActive(false);
            imageObject.SetActive(true);
            imageObject.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            StartCoroutine(FlipImageCoroutine());
        }

        isRotatingScrollView = false;
    }
}



