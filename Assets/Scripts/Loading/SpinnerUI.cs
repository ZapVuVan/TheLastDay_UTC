using UnityEngine;

public class SpinnerUI : MonoBehaviour
{
    [SerializeField] private float speed = 300f;

    private void Update()
    {
        transform.Rotate(0f, 0f, -speed * Time.deltaTime);
    }
}
