using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool IsStop { get; set; }
    public float Speed { get; set; }

    private void Update()
    {
        transform.Translate(Vector3.down * (Speed * Time.deltaTime), Space.World);
    }
}
