using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSizeControl : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("LocalScale: " + player.transform.localScale);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CharacterBigger()
    {
        player.transform.localScale = new Vector3((float)(player.transform.localScale.x + 1.0), (float)(player.transform.localScale.y + 1.0), (float)(player.transform.localScale.z + 1.0));
        Debug.Log("LocalScale: " + player.transform.localScale);

    }

    public void CharacterSmaller()
    {
        player.transform.localScale = new Vector3((float)(player.transform.localScale.x - 1.0), (float)(player.transform.localScale.y - 1.0), (float)(player.transform.localScale.z - 1.0));
        Debug.Log("LocalScale: " + player.transform.localScale);
    }
}
