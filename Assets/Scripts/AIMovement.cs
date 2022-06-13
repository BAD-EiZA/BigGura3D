using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : CharacterMovement
{
    private EnemyAI eAI;
    private float currentStoppingTime;
    private float currentTime;
    private bool shouldBeCounting = true;
    private void OnEnable()
    {
        if (eAI == null)
            eAI = FindObjectOfType<EnemyAI>();
        eAI.OnStartCount += OnStartCount;
        eAI.OnStopCount += OnStopCount;
        currentStoppingTime = Random.Range(3, 6);
    }
    private void Update()
    {
        if (shouldBeCounting)
            currentTime += Time.deltaTime;

        if (currentTime >= currentStoppingTime)
        {
            verDirect = 0;
            shouldBeCounting = false;
        }
        anim.SetFloat("Speed", rb.velocity.normalized.magnitude);
    }
    private void OnStartCount()
    {
        verDirect = 1;
        currentTime = 0;
        shouldBeCounting = true;
        currentStoppingTime = Random.Range(3, 6);
    }
    private void OnStopCount()
    {
        verDirect = 0;
    }
}
