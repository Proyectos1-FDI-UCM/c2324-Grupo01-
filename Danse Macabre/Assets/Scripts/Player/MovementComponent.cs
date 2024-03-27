using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementComponent : MonoBehaviour
{
    #region references
    [SerializeField]
    private Transform myTransform;
    private Rigidbody2D myRigidBody;
    [SerializeField]
    private TempoManager TempoManager;

    // DO NOT DELETE
    // [SerializeField]
    // private GameObject MusicManager;
    // private AudioSource music;
    #endregion

    #region properties
    public float speed;
    public bool canMove = true;
    //private bool canCallMethod = true;
    #endregion

    void Start()
    {
        myTransform = transform;
        myRigidBody = GetComponent<Rigidbody2D>();
        
        speed = TempoManager.PlayerSpeed;
        GameManager.Instance.PlayerSpeed = speed;
        //Debug.Log("Movement: Speed" +  speed); 
        
        Autoscroll();
    }
    
    void Update()
    {

        // NO QUITAR: SIRVE PARA LEVEL BUILDING
        // if (canCallMethod && Time.time > 2)
        // {
        //     Autoscroll();
        //     canCallMethod = false; 
        // }
    }

    #region methods
    public void Autoscroll()
    {
        myRigidBody.velocity = Vector2.right * speed;
    }
    public void InitialPosition(Vector3 position)
    {
        myTransform.position = position;
    }
    #endregion
}
