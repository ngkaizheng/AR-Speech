using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AI;

using IBM.Watson.SpeechToText.V1;
using IBM.Cloud.SDK;
using IBM.Cloud.SDK.Authentication;
using IBM.Cloud.SDK.Authentication.Iam;
using IBM.Cloud.SDK.Utilities;
using IBM.Cloud.SDK.DataTypes;

public class AnimationPlayer : MonoBehaviour
{
    // public TMP_Dropdown dropDown;
    public GameObject player;

    // Audio clip to play with the animation
    public AudioClip[] audioClip;

    // AudioSource component to play the audio clip
    private AudioSource audioSource;

    private Animator animator;


    void Start()
    {

    }

    private void Awake()
    {
        NavMeshAgent agent = player.GetComponent<NavMeshAgent>();

        // Get the Animator component attached to the player GameObject
        animator = player.GetComponent<Animator>();

        // Get the AudioSource component attached to the player GameObject
        audioSource = player.GetComponent<AudioSource>();

        // Assign the audio clip to the AudioSource component
        // audioSource.clip = audioClip;
    }

    private float cooldownDuration = 2.0f; // 0.5 seconds cooldown duration
    private Dictionary<string, float> lastAnimationTimes = new Dictionary<string, float>();

    public bool CanTriggerAnimation(string animationName)
    {
        if (!lastAnimationTimes.ContainsKey(animationName) || Time.time - lastAnimationTimes[animationName] >= cooldownDuration)
        {
            lastAnimationTimes[animationName] = Time.time;
            return true;
        }
        return false;
    }

    public void playHello()
    {

        audioSource.Stop();

        animator.SetTrigger("Hello");
        animator.SetBool("Dance", false);
        animator.SetBool("Sleep", false);
        animator.SetBool("Running", false);

        audioSource.clip = audioClip[0];
        audioSource.loop = false;
        audioSource.Play();
    }

    public void playSleep()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Sleep")) { return; }
        audioSource.Stop();

        animator.SetBool("Dance", false);
        animator.SetBool("Sleep", true);
        animator.SetBool("Running", false);

        audioSource.clip = audioClip[1];
        audioSource.loop = true;
        audioSource.Play();
    }

    public void playDance()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dance")) { return; }
        audioSource.Stop();

        animator.SetBool("Dance", true);
        animator.SetBool("Sleep", false);
        animator.SetBool("Running", false);
    }

    public void playRunning()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Running")) { return; }
        audioSource.Stop();


        animator.SetBool("Dance", false);
        animator.SetBool("Sleep", false);
        animator.SetBool("Running", true);

        audioSource.clip = audioClip[2];
        audioSource.loop = true;
        audioSource.Play();

    }

    public void playStop()
    {
        audioSource.Stop();
        elpasedTimeGoAway = 100;
        elpasedTimeComeBack = 100;
        animator.SetBool("Walk", false);
        animator.SetBool("Dance", false);
        animator.SetBool("Sleep", false);
        animator.SetBool("Running", false);

    }

    // public float RotateDegree = 0;
    public Vector3 originLocation = new Vector3(0, 0, 0);

    public Vector3 setOriginLocation()
    {
        Debug.Log("Origin Location: " + player.transform.position);
        return originLocation = player.transform.position;
    }

    [SerializeField] private float moveSpeed = 0.50f; // Movement speed of the player
    [SerializeField] private bool GoAwayAnimation = false;

    public TextMeshProUGUI speedText;

    public void IncreaseSpeed()
    {
        moveSpeed += 0.10f;
        speedText.text = "Current Speed: " + moveSpeed;
    }

    public void DescreaseSpeed()
    {
        if (moveSpeed <= 0.11f) return;
        moveSpeed -= 0.1f;
        speedText.text = "Current Speed: " + moveSpeed;
    }

    // public void playGoAway()
    // {
    //     Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
    //     StartCoroutine(MovePlayerForDuration(randomDirection, 5));
    // }

    // public void playComeBack()
    // {
    //     StartCoroutine(MovePlayerToPosition(new Vector3(0f, 0f, 0.06f), 5));
    // }
    public void playGoAway()
    {
        // Rotate the player in the x direction by 180 degrees
        player.transform.localRotation = Quaternion.Euler(90f, 90f, 90f);

        // Define the fixed movement direction
        Vector3 moveDirection = Vector3.down;

        // Start the coroutine to move the player for 5 seconds
        StartCoroutine(MovePlayerForDuration(moveDirection, 5));
    }
    public void playComeBack()
    {
        // Rotate the player in the x direction by -180 degrees
        player.transform.localRotation = Quaternion.Euler(-90f, 90f, 90f);

        // Start the coroutine to move the player to the specified position for 5 seconds
        StartCoroutine(MovePlayerToPosition(new Vector3(0f, 0f, 0.06f), 5));
    }

    private float elpasedTimeGoAway = 0;
    private float elpasedTimeComeBack = 0;

    //Reset
    public void playReset()
    {
        //Find Tag Respawn
        GameObject respawn = GameObject.FindGameObjectWithTag("Respawn");
        player.transform.localPosition = new Vector3(0f, 0f, 0.06f);
        player.transform.localRotation = Quaternion.Euler(-90f, 90f, 90f);
    }

    IEnumerator MovePlayerToPosition(Vector3 targetPosition, float duration)
    {
        // Get the GameObject with the "Player" tag
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            // Get the Animator component attached to the GameObject with the "Player" tag
            Animator animator = playerObject.GetComponent<Animator>();

            if (animator != null)
            {
                // Set the "Running" parameter of the animator to true
                animator.SetBool("Running", true);

                // Get the initial local position of the player
                Vector3 initialPosition = playerObject.transform.localPosition;
                elpasedTimeComeBack = 0f;

                while (elpasedTimeComeBack < duration)
                {
                    // Calculate the interpolation factor (0 to 1)
                    float t = elpasedTimeComeBack / duration;

                    // Interpolate between initial and target local positions
                    playerObject.transform.localPosition = Vector3.Lerp(initialPosition, targetPosition, t);

                    // Update the elapsed time
                    elpasedTimeComeBack += Time.deltaTime;
                    yield return null; // Wait for the next frame
                }

                // Set the "Running" parameter of the animator to false
                animator.SetBool("Running", false);
            }
            else
            {
                Debug.LogError("Animator component not found on GameObject with tag 'Player'.");
            }
        }
        else
        {
            Debug.LogError("GameObject with tag 'Player' not found.");
        }
    }

    // IEnumerator MovePlayerToPosition(Vector3 targetPosition, float duration)
    // {
    //     // Get the GameObject with the "Player" tag
    //     GameObject playerObject = GameObject.FindWithTag("Player");

    //     if (playerObject != null)
    //     {
    //         // Get the Animator component attached to the GameObject with the "Player" tag
    //         Animator animator = playerObject.GetComponent<Animator>();

    //         if (animator != null)
    //         {
    //             // Set the "Running" parameter of the animator to true
    //             animator.SetBool("Running", true);

    //             Vector3 initialPosition = playerObject.transform.position;
    //             elpasedTimeComeBack = 0f;

    //             while (elpasedTimeComeBack < duration)
    //             {
    //                 // Calculate the interpolation factor (0 to 1)
    //                 float t = elpasedTimeComeBack / duration;

    //                 // Interpolate between initial and target positions
    //                 playerObject.transform.position = Vector3.Lerp(initialPosition, targetPosition, t);

    //                 // Calculate the direction vector from current position to target position
    //                 Vector3 moveDirection = (targetPosition - initialPosition).normalized;

    //                 // // Orient the character to face the movement direction (if not stationary)
    //                 // if (moveDirection != Vector3.zero)
    //                 // {
    //                 //     Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
    //                 //     playerObject.transform.rotation = Quaternion.Lerp(playerObject.transform.rotation, targetRotation, Time.deltaTime * 10f);
    //                 // }

    //                 // Update the elapsed time
    //                 elpasedTimeComeBack += Time.deltaTime;
    //                 yield return null; // Wait for the next frame
    //             }

    //             // Set the "Running" parameter of the animator to false
    //             animator.SetBool("Running", false);
    //         }
    //         else
    //         {
    //             Debug.LogError("Animator component not found on GameObject with tag 'Player'.");
    //         }
    //     }
    //     else
    //     {
    //         Debug.LogError("GameObject with tag 'Player' not found.");
    //     }
    // }

    // IEnumerator MovePlayerForDuration(Vector3 moveDirection, float duration)
    // {
    //     // Get the GameObject with the "Player" tag
    //     GameObject playerObject = GameObject.FindWithTag("Player");

    //     if (playerObject != null)
    //     {
    //         // Get the CharacterController component attached to the GameObject with the "Player" tag
    //         CharacterController controller = playerObject.GetComponent<CharacterController>();

    //         // Get the Animator component attached to the GameObject with the "Player" tag
    //         Animator animator = playerObject.GetComponent<Animator>();

    //         if (controller != null && animator != null)
    //         {
    //             // Set the "Running" parameter of the animator to true
    //             animator.SetBool("Running", true);

    //             elpasedTimeGoAway = 0f;
    //             while (elpasedTimeGoAway < duration)
    //             {
    //                 // Move the character in the specified direction
    //                 controller.Move(moveDirection * moveSpeed * Time.deltaTime);

    //                 // // Orient the character to face the movement direction (if not stationary)
    //                 // if (moveDirection != Vector3.zero)
    //                 // {
    //                 //     Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
    //                 //     playerObject.transform.rotation = Quaternion.Lerp(playerObject.transform.rotation, targetRotation, Time.deltaTime * 10f);
    //                 // }

    //                 // Update the elapsed time
    //                 elpasedTimeGoAway += Time.deltaTime;
    //                 yield return null; // Wait for the next frame
    //             }

    //             // Set the "Running" parameter of the animator to false
    //             animator.SetBool("Running", false);
    //         }
    //         else
    //         {
    //             Debug.LogError("CharacterController or Animator component not found on GameObject with tag 'Player'.");
    //         }
    //     }
    //     else
    //     {
    //         Debug.LogError("GameObject with tag 'Player' not found.");
    //     }
    // }
    IEnumerator MovePlayerForDuration(Vector3 moveDirection, float duration)
    {
        // Get the GameObject with the "Player" tag
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            // Get the Animator component attached to the GameObject with the "Player" tag
            Animator animator = playerObject.GetComponent<Animator>();

            if (animator != null)
            {
                // Set the "Running" parameter of the animator to true
                animator.SetBool("Running", true);

                // Get the initial local position of the player
                Vector3 initialPosition = playerObject.transform.localPosition;
                elpasedTimeGoAway = 0f;

                while (elpasedTimeGoAway < duration)
                {
                    // Calculate the interpolation factor (0 to 1)
                    float t = elpasedTimeGoAway / duration;

                    // Interpolate between initial and target local positions
                    playerObject.transform.localPosition = Vector3.Lerp(initialPosition, initialPosition + moveDirection, t);

                    // Update the elapsed time
                    elpasedTimeGoAway += Time.deltaTime;
                    yield return null; // Wait for the next frame
                }

                // Set the "Running" parameter of the animator to false
                animator.SetBool("Running", false);
            }
            else
            {
                Debug.LogError("Animator component not found on GameObject with tag 'Player'.");
            }
        }
        else
        {
            Debug.LogError("GameObject with tag 'Player' not found.");
        }
    }

}
