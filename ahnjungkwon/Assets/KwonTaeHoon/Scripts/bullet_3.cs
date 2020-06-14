using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_3 : MonoBehaviour
{
    public float DestroyTime = 2.0f;
    public float bulletSpeed = 1.0f;

    [SerializeField]
    private ParticleSystem hitParticle;

    void Start()
    {
        Destroy(gameObject, DestroyTime);
    }

    void Update()
    {
        //프레임마다 오브젝트를 로컬좌표상에서 앞으로 1의 힘만큼 날아가라
        transform.Translate(Vector3.forward * bulletSpeed);


    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Particle Played");
        other.GetComponent<KE_Dragon>().OnDamageProcess();
        hitParticle.Play();

        Destroy(gameObject, 0.3f);

        //if (other.tag == "Enemy")
        //{
        //    Debug.Log("Particle Played");
        //    other.GetComponent<KE_Dragon>().OnDamageProcess();
        //    hitParticle.Play();
            
        //    Destroy(gameObject, 0.3f);
        //}
        //else if(other.tag == "Walls")
        //{
        //    hitParticle.Play();
        //}

        
    }


}
