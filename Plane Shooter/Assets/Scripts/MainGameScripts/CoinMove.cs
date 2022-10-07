using UnityEngine;

public class CoinMove : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    
    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.down * (speed * Time.deltaTime));
    }
}
