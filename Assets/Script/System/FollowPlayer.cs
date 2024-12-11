using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] float Speed;
    [SerializeField] float SmoothTime;
    Rigidbody2D rb;
    Vector2 vel = Vector2.zero;
    Vector2 target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        target = (Player.position - transform.position).normalized * Speed;
        rb.linearVelocity = Vector2.SmoothDamp(rb.linearVelocity, target, ref vel, SmoothTime);
    }
}