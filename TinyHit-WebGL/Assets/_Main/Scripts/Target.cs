using UnityEngine;

public class Target : MonoBehaviour
{
    public float KnifeDepth => _knifeDepth;

    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _knifeDepth;

    private void Update()
    {
        transform.Rotate(0, 0, _rotateSpeed * Time.deltaTime);
    }
}
