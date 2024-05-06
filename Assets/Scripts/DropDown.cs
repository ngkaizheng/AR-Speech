using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AI;

public class DropDown : MonoBehaviour
{
    public TMP_Dropdown dropDown;
    public GameObject player;

    void Start()
    {
        // dropDown.onValueChanged.AddListener(HandleInputData);
    }

    public void HandleInputData()
    {
        NavMeshAgent agent = player.GetComponent<NavMeshAgent>();
        Animator animator = player.GetComponent<Animator>();


        animator.SetBool("Flair", dropDown.value == 1);
        animator.SetBool("Breakdance", dropDown.value == 2);
        animator.SetBool("Locking", dropDown.value == 3);
        Debug.Log("Value: " + dropDown.value);
    }
}
