using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using DG.Tweening;

public class Boss : MonoBehaviour, Damageable
{
    //-----------------DropStats----------------
    public GameObject BossKey;

    public int randomNumber;
    //------------BattleStats-------------------
    public int maxHealth = 500;
    public int currentHealth;
    public float Skill01Range = 15f;
    public float lookRadius = 15f;
    public float stoppingDistance = 3f;

    //--------------MoveStats-------------------
    Vector3 right = new Vector3(0.126f, -0.093f, 0f);
    Vector3 left = new Vector3(-0.126f, -0.093f, 0f);
    Transform target;
    //--------------AnimatorControl-------------------
    public Animator animator;
    //----------------Skills--------------------------    
    public Transform attackPoint;
    public GameObject bullet;
    public GameObject BeingSummoned;
    private bool CanSummonStage1 = true;
    private bool CanSummonStage2 = true;
    private bool Intro1 = true;
    private bool Intro2 = true;
    private bool NeedHeal = false;
    //----------------NavMeshAgent--------------------
    NavMeshAgent agent;
    //-------------------Partical System--------------
    public ParticleSystem Intro;
    public GameObject Stage2;
    public ParticleSystem P_Summon;
    public ParticleSystem Death1;
    public ParticleSystem Death2;
    public ParticleSystem Death3;
    public ParticleSystem Death4;
    //--------------HealthBar--------------------------
    public Slider healthbar;




    void Start()
    {
        target = Game.GetPlayerTransform();

        attackPoint.LookAt(target);

        currentHealth = maxHealth;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.stoppingDistance = 4f;

        randomNumber = Random.Range(0, 101);
        
    }


    void Update()
    {
        healthbar.value = currentHealth;

        if (Intro1 == true)
        {
            Intro.Play();
            GetComponent<Animator>().Play("Boss01-idle");
            StartCoroutine(BossIntro());
            
        }
        else
        {
            agent.SetDestination(target.position);
            float distance = Vector3.Distance(target.position, transform.position);
            if (transform.position.x < target.position.x)//Face to Player
            {
                GetComponent<SpriteRenderer>().flipX = false;

            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            //---------------------------   Skill Start --------------------

            if (currentHealth >= 500)//Attack01
            {
                GetComponent<Animator>().Play("Boss01_Attack1");
            }

            if (currentHealth < 500 && currentHealth >= 400)
            {
                if (Intro2 == true)
                {
                    transform.DOShakePosition(1f, Vector3.one * 0.1f, 30);
                    GetComponent<Animator>().Play("Boss01-idle");
                    StartCoroutine(BossIntro2());
                }
                else
                {
                    Stage2.GetComponent<ParticleSystem>().Play();
                    GetComponent<Animator>().Play("Boss01_Attack2");
                }
            }

            if (currentHealth < 400 && currentHealth >= 300 && NeedHeal == false)
            {
                if (CanSummonStage1 == true)
                {
                    GetComponent<Animator>().Play("Summon01");
                    StartCoroutine(Summon01());
                }
                else
                {
                    GetComponent<Animator>().Play("Boss01_Attack1");
                }

            }

            if (currentHealth < 300 && currentHealth >= 100 && NeedHeal == false)
            {

                    GetComponent<Animator>().Play("Boss01_Attack2");

            }

            if (currentHealth < 100 && NeedHeal == false)
            {
                if (CanSummonStage2 == true)
                {
                    NeedHeal = true;
                }
                else
                {
                    GetComponent<Animator>().Play("Boss01_Attack1");
                }
            }
            
            if (NeedHeal == true)
            {
                GetComponent<Animator>().Play("Summon02");
                StartCoroutine(Summon02());
            }
        }  
    }

    public void OnDamaged(int damage)
    {
        currentHealth -= damage;
        transform.DOShakePosition(0.5f, Vector3.one * 0.1f, 20);
        if (currentHealth <= 0)
        {
            Death1.GetComponent<ParticleSystem>().Play();
            Death2.GetComponent<ParticleSystem>().Play();
            Death3.GetComponent<ParticleSystem>().Play();
            Death4.GetComponent<ParticleSystem>().Play();
            GetComponent<BossRoom>().CloseDown();
            Die();
        }

    }

    IEnumerator BossIntro()
    {
        GetComponent<Animator>().Play("Boss01-idle");
        yield return new WaitForSeconds(3f);
        Intro1 = false;
    }
    IEnumerator BossIntro2()
    { 
        yield return new WaitForSeconds(3f);
        Intro2 = false;
       
    }
    IEnumerator Summon01()
    {
        
        yield return new WaitForSeconds(1.5f);
        CanSummonStage1 = false;
    }
    IEnumerator Summon02()
    {
        Debug.Log("Wait 6 sec");
        yield return new WaitForSeconds(6.5f);
        CanSummonStage2 = false;
        NeedHeal = false;
        Debug.Log("Finish Waiting");
    }

    void Die()
    {

        animator.Play("Boss01_Die");
        //Drop();
        GetComponent<Collider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        Destroy(Stage2);
        this.enabled = false;
    }

    void Drop()
    {
        
            Instantiate(BossKey, transform.position, Quaternion.identity);

    }


    //-------------------------Animator Event--------------------------------------------------------
    //---------------------------AttackStage1-----------------------------------
    void ShootType01()
    {       
        Instantiate(bullet, attackPoint.transform.position, Quaternion.identity);
    }

    //---------------------------AttackStage2-----------------------------------
    void ShootType02()
    {
        Vector3 ShootLeft = new Vector3(attackPoint.transform.position.x + 2f, attackPoint.transform.position.y, attackPoint.transform.position.z);
        Vector3 ShootRight = new Vector3(attackPoint.transform.position.x - 2f, attackPoint.transform.position.y, attackPoint.transform.position.z);

        Instantiate(bullet, ShootLeft, Quaternion.identity);
        Instantiate(bullet, attackPoint.transform.position, Quaternion.identity);
        Instantiate(bullet, ShootRight, Quaternion.identity);

    }
    //---------------------------SummonStage1-----------------------------------
    void summonStage01()
    {
        for (int i = 2; i > 0; i--)
        {
           
            Vector3 Sposition = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            Instantiate(BeingSummoned, Sposition, Quaternion.identity);
        }
    }

    //---------------------------Heal---------------------------------------------
        void Heal()
    {
        //VFX
        currentHealth += 20;
    }
    //----------------------------------------------------------------------------
    void StopMoving()
    {
        agent.speed = 0;
    }
    void StartMoving()
    {
        agent.speed = 2;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.DrawWireSphere(transform.position, Skill01Range);
    }

}