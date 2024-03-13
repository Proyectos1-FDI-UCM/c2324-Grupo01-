using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowComponent : MonoBehaviour
{
    #region paramaters
    private float perfectRadius = 0.2f;
    private float goodRadius = 0.35f;
    private float badRadius = 0.5f;
    #endregion

    #region refrences
    // [SerializeField]
    // private Material grayShader;
    private Animator _myAnimator;
    private SpriteRenderer _myRenderer;
    [SerializeField]
    private Sprite graySprite;
    #endregion

    private void Start()
    {
        _myAnimator = GetComponent<Animator>();
        _myRenderer = GetComponent<SpriteRenderer>();
    }

    #region methods
    public void Deactivate()
    {
        _myAnimator.enabled = false;
        _myRenderer.sprite = graySprite;
        //_myRenderer.material = grayShader;
    }
    #endregion

    void OnDrawGizmos()
    {
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        if (collider != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + (Vector3)collider.offset, perfectRadius);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position + (Vector3)collider.offset, goodRadius);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position + (Vector3)collider.offset, badRadius);
        }
    }
}