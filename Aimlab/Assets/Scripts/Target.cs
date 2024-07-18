using UnityEngine;

public class Target : MonoBehaviour
{
    private TargetSpawner targetSpawner;
    private float lifetime;
    private bool isActive;

    void Start()
    {
        targetSpawner = FindObjectOfType<TargetSpawner>();
    }

    public void Activate(float lifetime)
    {
        this.lifetime = lifetime;
        isActive = true;
        Invoke(nameof(Deactivate), lifetime);
    }

    public void OnHit()
    {
        if (isActive)
        {
            Deactivate();
        }
    }

    void Deactivate()
    {
        isActive = false;
        CancelInvoke();
        targetSpawner.ReturnObject(gameObject);
    }
}
