// using System;
// using System.Text;
// using System.Speech;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;
// using UnityEngine.Windows.Speech;
// public class PalyerMovement : MonoBehaviour
// {
//     // Create the keywords and the recognizer
//     private KeywordRecognizer keywordRecognizer;
//     private Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
//     public GameObject player;
//     public float speed = 5.0f;
//     public float rotationSpeed = 200.0f;
//     // Start is called before the first frame update
//     void Start()
//     {
//         // Add the keywords and the functions to the dictionary
//         keywords.Add("forward", MoveForward);
//         keywords.Add("backward", MoveBackward);
//         keywords.Add("left", MoveLeft);
//         keywords.Add("right", MoveRight);
//         keywords.Add("stop", Stop);
//         keywords.Add("turn left", TurnLeft);
//         keywords.Add("turn right", TurnRight);
//         // Create the KeywordRecognizer and pass the dictionary to it
//         keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
//         // Register the OnPhraseRecognized function
//         keywordRecognizer.OnPhraseRecognized += OnPhraseRecognized;
//         // Start the recognizer
//         keywordRecognizer.Start();
//     }
//     private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
//     {
//         System.Action keywordAction;
//         if (keywords.TryGetValue(args.text, out keywordAction))
//         {
//             if (args.confidence >= ConfidenceLevel.Medium)
//             {
//                 Debug.Log("Recognized phrase: " + args.text + " with confidence: " + args.confidence);
//                 keywordAction.Invoke();
//             }
//         }
//     }
//     private void MoveForward()
//     {
//         player.transform.Translate(Vector3.forward * Time.deltaTime * speed);
//     }
//     private void MoveBackward()
//     {
//         player.transform.Translate(-Vector3.forward * Time.deltaTime * speed);
//     }
//     private void MoveLeft()
//     {
//         player.transform.Translate(-Vector3.right * Time.deltaTime * speed);
//     }
//     private void MoveRight()
//     {
//         player.transform.Translate(Vector3.right * Time.deltaTime * speed);
//     }
//     private void Stop()
//     {
//         player.transform.Translate(Vector3.zero);
//     }
//     private void TurnLeft()
//     {
//         player.transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
//     }
//     private void TurnRight()
//     {
//         player.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
//     }
//     // Update is called once per frame
//     void Update()
//     {
        
//     }

// }
