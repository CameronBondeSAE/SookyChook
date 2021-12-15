using System.Collections;
using System.Collections.Generic;
using Aaron;
using UnityEngine;

public class GhostChicken : MonoBehaviour
{
    public Vector3 playerLocation;
    public Rigidbody rb;
    private TurnTowards turnTowards;
    private CharacterModel characterModel;
    
    // Start is called before the first frame update
    void Start()
    {
        characterModel = FindObjectOfType<CharacterModel>();
        rb = GetComponent<Rigidbody>();
        turnTowards = GetComponent<TurnTowards>();
        playerLocation = characterModel.transform.position;
        turnTowards.target = playerLocation;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
