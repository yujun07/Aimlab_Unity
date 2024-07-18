using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab;       // 과녁 프리팹
    public int initialPoolSize = 20;      // 초기 풀 크기
    public int numberOfTargets = 10;      // 생성할 과녁의 수
    public float targetLifetime = 5f;     // 과녁의 생명 주기 (초 단위)
    public float minDistance = 1.5f;      // 과녁 간 최소 거리
    public Transform spawnAreaMin;
    public Transform spawnAreaMax;

    private List<GameObject> pool;
    private List<GameObject> activeTargets;

    void Awake()
    {
        pool = new List<GameObject>();
        activeTargets = new List<GameObject>();

        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = Instantiate(targetPrefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public void StartSpawning()
    {
        SpawnTargets();
    }

    void SpawnTargets()
    {
        for (int i = 0; i < numberOfTargets; i++)
        {
            SpawnTarget();
        }
    }

    void SpawnTarget()
    {
        Vector3 randomPosition;
        int attempts = 0;
        bool validPosition = false;

        do
        {
            randomPosition = new Vector3(
                Random.Range(spawnAreaMin.position.x, spawnAreaMax.position.x),
                Random.Range(spawnAreaMin.position.y, spawnAreaMax.position.y),
                Random.Range(spawnAreaMin.position.z, spawnAreaMax.position.z)
            );

            validPosition = true;

            foreach (GameObject target in activeTargets)
            {
                if (Vector3.Distance(randomPosition, target.transform.position) < minDistance)
                {
                    validPosition = false;
                    break;
                }
            }

            attempts++;
        } while (!validPosition && attempts < 100);

        if (validPosition)
        {
            GameObject target = GetObject();
            target.transform.position = randomPosition;
            target.GetComponent<Target>().Activate(targetLifetime);
            activeTargets.Add(target);
        }
    }

    public GameObject GetObject()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // If no inactive objects are available, create a new one
        GameObject newObj = Instantiate(targetPrefab);
        newObj.SetActive(true);
        pool.Add(newObj);
        return newObj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        activeTargets.Remove(obj);
        SpawnTarget(); // ReturnObject 호출 시 새로운 과녁 생성
    }
}
