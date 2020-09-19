using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    [SerializeField] private int radius = 5;
    [SerializeField] private int damage = 5;
    [SerializeField] private float attackRate = 5f;
    private float nextAttack = 0f;

    private Collider2D[] hitColliders = { };

    public void FixedUpdate()
    {
        hitColliders = Physics2D.OverlapCircleAll(transform.position, radius, 1 << 8);

        if (hitColliders.Length > 0 && Time.time > nextAttack && hitColliders[0].gameObject != null)
        {
            hitColliders[0].GetComponent<Mob>().Hit(damage);
            nextAttack = Time.time + attackRate;
        }
    }

#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i] == null)
            {
                break;
            }
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(hitColliders[i].transform.position, Vector3.one);
        }
    }
#endif
}
