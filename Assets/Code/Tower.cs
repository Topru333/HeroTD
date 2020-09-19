using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [field: SerializeField] public int Radius { get; private set; } = 5;
    [field: SerializeField] public int Damage { get; private set; } = 5;
    [field: SerializeField] public float AttackRate { get; private set; } = 5f;
    [field: SerializeField] public int Price { get; private set; } = 5;
    [field: SerializeField] public Vector2Int TileRadius { get; private set; } = Vector2Int.one;
    private float nextAttack = 0f;

    private Collider2D[] hitColliders = { };

    public void FixedUpdate()
    {
        hitColliders = Physics2D.OverlapCircleAll(transform.position, Radius, 1 << 8);

        if (hitColliders.Length > 0 && Time.time > nextAttack && hitColliders[0].gameObject != null)
        {
            hitColliders[0].GetComponent<Mob>().Hit(Damage);
            nextAttack = Time.time + AttackRate;
        }
    }

#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Radius);
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
