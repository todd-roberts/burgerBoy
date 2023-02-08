using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    private TextMeshPro _tmp;
    private Animator _animator;

    [SerializeField]
    private float fadeRate = 2.5f;

    private void Awake()
    {
        _tmp = GetComponent<TextMeshPro>();
        _animator = GetComponent<Animator>();
    }

    public void Display(int damage)
    {
        _tmp.text = $"{damage}";
    }

    private void Update()
    {
        Fade();
        FaceCamera();
    }

    private void FaceCamera()
    {
        Vector3 lookPos = _tmp.transform.position - Camera.main.transform.position;

        _tmp.transform.rotation = Quaternion.LookRotation(lookPos);
    }

    private void Fade()
    {
        _tmp.alpha -= Time.deltaTime * fadeRate;

        if (_tmp.alpha <= 0)
        {
            Object.Destroy(gameObject);
        }
    }
}
