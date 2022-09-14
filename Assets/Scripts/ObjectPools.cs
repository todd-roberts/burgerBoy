using UnityEngine;

public class ObjectPools : MonoBehaviour
{
    [SerializeField]
    GameObject[] prefabs;

    private void Start()
    {
        PrewarmPrefabs();
    }

    private void PrewarmPrefabs()
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            GameObject obj = Instantiate(prefabs[i], transform.position, Quaternion.identity);
            Object.Destroy(obj);
        }
    }

}
