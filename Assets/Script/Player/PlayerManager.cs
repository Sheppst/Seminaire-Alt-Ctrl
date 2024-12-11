using Cainos.PixelArtTopDown_Basic;
using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject VIDE;
    TopDownCharacterController PM;
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
        PM = GetComponent<TopDownCharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MonsterScript MS = collision.GetComponent<MonsterScript>();
        if (MS && collision.name != name && collision.CompareTag("Monster"))
        {
            PF.Monster = MS;
            PF.enabled = true;
            i = Instantiate(VIDE, transform.position + (collision.transform.position - transform.position)/2, Quaternion.identity);
            Transform t = i.transform;
            Vector3 target1 = (transform.position - t.position).normalized * 5;
            Vector3 target2 = (collision.transform.position - t.position).normalized * 5;
                target1.y = 0;
                target2.y = 0;
            
            target1.z = 0;
            target2.z = 0;
            transform.position += target1;
            collision.transform.position += target2;
            StartCoroutine(Inutile());
            enabled = false;
        }
        IEnumerator Inutile()
        {
            yield return new WaitForEndOfFrame();
            PM.enabled = false;
        }
    }
}
