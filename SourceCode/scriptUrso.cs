using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class scriptUrso : MonoBehaviour
{
    public GameObject pc;
    private NavMeshAgent agnt;
    public GameObject[] waypoints = new GameObject[4];
    public float distMin = 5;
    public float grabDist = 3f;
    private Animator anim;
    private int i = 0;
    private GameObject destino;
    private bool isFollowing = false;
    private bool isDead = false;
    private bool isAttacking = false;
    public TextMeshProUGUI vidasText;

    private LifesManager lifesManager;
    private AudioSource audioSource;
    public AudioClip attackSound;

    void Start()
    {
        agnt = GetComponent<NavMeshAgent>();
        agnt.speed = 4.5f;
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        lifesManager = FindObjectOfType<LifesManager>();
        if (lifesManager == null)
        {
            Debug.LogError("LifesManager não encontrado!");
        }
        prox();
    }

    void Update()
    {
    float distanceToPlayer = Vector3.Distance(transform.position, pc.transform.position);

    if (lifesManager == null)
    {
        Debug.LogError("LifesManager ainda não está configurado!");
        return;
    }

    if (!isAttacking && distanceToPlayer < grabDist)
    {
        agnt.isStopped = true;
        StartCoroutine(AttackCoroutine());
    }
    else if (!isAttacking && (isFollowing || distanceToPlayer < 20))
    {
        isFollowing = true;
        agnt.SetDestination(pc.transform.position);
    }
    else if (!isAttacking && Vector3.Distance(transform.position, destino.transform.position) < distMin)
    {
        prox();
    }

    anim.SetBool("WalkForward", !isAttacking);
    }



    private IEnumerator AttackCoroutine()
    {
    isAttacking = true;

    anim.SetTrigger("Attack1");
    audioSource.PlayOneShot(attackSound);

    yield return new WaitForSeconds(1.2f);

    prox();

    agnt.SetDestination(destino.transform.position);

    lifesManager.PerderVida();

    isAttacking = false;
    }


    private void prox()
    {
        if (isDead) return;

        destino = waypoints[i++];
        if (i == waypoints.Length)
            i = 0;
        agnt.SetDestination(destino.transform.position);
        agnt.isStopped = false;
    }
}