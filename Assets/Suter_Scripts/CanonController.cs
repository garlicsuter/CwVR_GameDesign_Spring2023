using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CanonController : MonoBehaviour
{
    [SerializeField] private float minPower = 10f;
    [SerializeField] private float maxPower = 100f;
    [SerializeField] private float powerStep = 5f;
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject pineapplePrefab;
    [SerializeField] private XRGrabInteractable dial;

    private float currentPower = 0f;
    private float nextFireTime = 0f;

    void Start()
    {
        if (dial)
        {
            dial.onSelectEntered.AddListener(StartPower);
            dial.onSelectExited.AddListener(Fire);
        }
    }

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 1f / fireRate;

            if (currentPower > 0f)
            {
                FirePineapple();
            }
        }
    }

    void StartPower(XRBaseInteractor interactor)
    {
        currentPower = 0f;
    }

    void Fire(XRBaseInteractor interactor)
    {
        currentPower = Mathf.Clamp(currentPower, minPower, maxPower);
    }

    void FirePineapple()
    {
        GameObject pineapple = Instantiate(pineapplePrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = pineapple.GetComponent<Rigidbody>();
        rb.AddForce(shootPoint.forward * currentPower, ForceMode.Impulse);
        currentPower = 0f;
    }

    void AdjustPower(float amount)
    {
        currentPower += amount * powerStep;
    }
}

