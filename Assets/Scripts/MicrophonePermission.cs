// using System;
// using System.Text;
// using System.Speech;
// using UnityEngine;
// using UnityEngine.Android;
// using UnityEngine.Windows.Speech;

// public class MicrophonePermission : MonoBehaviour
// {
//     void Start()
//     {
//         // Check if microphone access is granted using Unity's Microphone class
//         if (!Microphone.IsRecording(null))
//         {
//             // Start recording to prompt the user for microphone permissions
//             Microphone.Start(null, false, 1, 44100);
//             // Wait for a short time until the microphone initializes
//             while (!(Microphone.GetPosition(null) > 0)) { }
//             // Stop recording to release microphone resources
//             Microphone.End(null);
//         }

//         // Check if microphone permission is granted using Unity's permission system
//         if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
//         {
//             // Request microphone permissions from the user
//             Permission.RequestUserPermission(Permission.Microphone);
//         }
//     }
// }
