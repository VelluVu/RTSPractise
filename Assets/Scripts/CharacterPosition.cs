using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterPosition : MonoBehaviour {

    private Vector3 newPosition;

    public GameObject[] playerPositions;
    public GameObject[] players;

    public Animator animator;

    public float rotateSpeed;
    public float moveSpeed;

	// Use this for initialization
	void Start () {

        animator = GetComponent<Animator>();
        animator.SetInteger("Formations", 0);
        newPosition = transform.position;

        int playerAmount = players.Length;
        for (int i = 0; i < playerAmount; i++)
        {

            players[i].GetComponent<NavMeshAgent>().destination = playerPositions[i].transform.position;
            players[i].GetComponent<NavMeshAgent>().isStopped = false;
        }

	}
	
	// Update is called once per frame
	void Update () {

        transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 
            0, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);

        if (Input.GetMouseButtonUp(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray,out hit))
            {
                if (hit.collider.gameObject.CompareTag("Ground"))
                {
                    transform.position = hit.point;
                }
            }
 
        }

        RefreshTargets();

	}

    public void RefreshTargets()
    {

        int playerAmount = players.Length;
        for (int i = 0; i < playerAmount; i++)
        {

            players[i].GetComponent<NavMeshAgent>().destination = playerPositions[i].transform.position;
            players[i].GetComponent<NavMeshAgent>().isStopped = false;
        }

    }

    public void ChangeFormation(int formationNumber)
    {

        animator.SetInteger("Formations", formationNumber);

    }

    public void UpdatePlayerArray(Transform playerList)
    {
        for (int i = 0; i < playerList.gameObject.transform.childCount; i++)
        {
            players[i] = playerList.gameObject.transform.GetChild(i).gameObject.GetComponent<IconToPlayer>().myPlayer;
        }
    }
}
