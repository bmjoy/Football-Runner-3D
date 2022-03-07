using UnityEngine;
using System.Collections;

public class PoolObject : MonoBehaviour
{
    private bool destroyActivated;
    private Transform poolParent;

    protected virtual void Awake()
    {

    }

    public void SetPoolParent(Transform parent)
    {
        poolParent = parent;
    }

    public virtual void OnObjectReuse()
    {
        destroyActivated = false;
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
        ReturnToPool();
    }

    public void Destroy(float t)
    {
        if (!destroyActivated)
        {
            destroyActivated = true;
            StartCoroutine(DestroyRoutine(t));
        }
    }

    public void ReturnToPool()
    {
        transform.SetParent(poolParent);
    }

    private IEnumerator DestroyRoutine(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy();
    }
}
