using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class EnemyJumperScript : MonoBehaviour
{
    public int enemyHp = 1;
    private GameObject player;

    public float shotSpan = 1.5f;
    public float bulletSpeed = 10.0f;
    private float currentTime = 0f;

    [SerializeField] private GameObject enemyBullet1;

    [SerializeField]
    private Renderer _renderer;

    public GameObject playerSprite;


    private Sequence _seq;

    float dis = 0.0f;

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            enemyHp -= 1;
            if (enemyHp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }*/

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerSprite = GameObject.FindGameObjectWithTag("PlayerSprite");
        dis = Vector3.Distance(player.transform.position, transform.position);
    }

    private void Update()
    {
        dis = Vector3.Distance(player.transform.position, transform.position);
        /*currentTime += Time.deltaTime;
        if (currentTime > shotSpan)
        {
            if (dis <= 30.0f)
            {
                Shot();
            }
            currentTime = 0f;
        }*/
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            enemyHp -= 1;

            if (enemyHp <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                HitBlink();
            }
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine("Blow");
        }


    }


    //https://sunagitsune.com/unitycollisionvector2d/


    IEnumerator Blow()
    {
        int i = 0;
        while (i < 5)
        {
            yield return new WaitForSeconds(0.03f);
            player.transform.Translate(
                (player.transform.position - this.transform.position).normalized.x / 2,
                0,
                0
            );

            i++;
        }
    }


    private void HitBlink()
    {
        _seq?.Kill();
        _seq = DOTween.Sequence();
        _seq.AppendCallback(() => _renderer.enabled = false);
        _seq.AppendInterval(0.05f);
        _seq.AppendCallback(() => _renderer.enabled = true);
        _seq.AppendInterval(0.05f);
        _seq.SetLoops(2);
        _seq.Play();
    }

    public void Shot()
    {

        var pos = this.gameObject.transform.position;

        if(dis <= 30.0f)
        {
            var t = Instantiate(enemyBullet1) as GameObject;
            t.transform.position = pos;
            Vector3 vec = Vector3.Scale((player.transform.position - pos), new Vector3(1, 1, 0)).normalized;
            t.GetComponent<Rigidbody>().velocity = vec * bulletSpeed;

            /*var t2 = Instantiate(enemyBullet) as GameObject;
            t2.transform.position = pos;
            Vector3 vec2 = Vector3.Scale(((player.transform.position + new Vector3(0, 10, 0)) - pos), new Vector3(1, 1, 0)).normalized;
            t2.GetComponent<Rigidbody>().velocity = vec * bulletSpeed;*/

        }
    }



}