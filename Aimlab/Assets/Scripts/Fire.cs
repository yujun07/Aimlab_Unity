using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float range = 100f;
    public Camera cam;

    public LayerMask shot;
    private int totalShots = 0;
    private int successfulHits = 0;
    private float accuracy = 0;

    void Update()
    {
        if (!GameManager.instance.isGameOver && !MenuManager.instance.menuEnabled && GameManager.instance.isGameStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
                totalShots++;
            }
            CaculateAccuracy();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range, shot))
        {
            Debug.Log("hit");
            GameManager.instance.Score++;
            successfulHits++;
            GetComponent<AudioSource>().Play();
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.OnHit();
            }
        }
        else
        {
            Debug.Log("miss");
        }
    }
    private void CaculateAccuracy()
    {
        if (totalShots > 0)
        {
            accuracy = (successfulHits / (float)totalShots) * 100f;
        }
        else
        {
            accuracy = 0f;
        }
        GameManager.instance.Accuracy = accuracy;
    }
}
