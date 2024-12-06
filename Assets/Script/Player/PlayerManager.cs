using System.Threading;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PlayerFight PF;
    GameObject i;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        Destroy(i);
    }
    void Start()
    {
        PF = GetComponent<PlayerFight>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MonsterScript MS = collision.GetComponent<MonsterScript>();
        if (MS)
        {
            PF.Monster = MS;
        }
        PF.enabled = true;
        i = Instantiate(new GameObject(), (transform.position - collision.transform.position)/2, Quaternion.identity);
        Transform t = i.transform;
        transform.position = (transform.position - t.position).normalized * 5;
        collision.transform.position = (t.position - collision.transform.position).normalized * 5;
        enabled = false;
    }
}
