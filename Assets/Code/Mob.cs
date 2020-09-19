using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class Mob : MonoBehaviour
{
    private List<Vector2> path;
    private Mob status;
    private Rigidbody2D rigidbdy;
    private Animator animator;

    public void Start()
    {
        status = gameObject.GetComponent<Mob>();
        rigidbdy = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    public void FixedUpdate()
    {
        if (HP < 1)
        {
            Debug.Log(HP);
            Kill();
        }

        animator.SetFloat("Speed", status.Speed);
        if (path != null)
        {
            if (rigidbdy.position == path[0] && path.Count > 1)
            {
                path.RemoveAt(0);
            }
            rigidbdy.MovePosition(Vector2.MoveTowards(transform.position, path[0], status.Speed * Time.deltaTime));
            TurnSprite(path[0]);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        PlayerControll.instance.HP.Value -= status.DMG;
        Destroy(gameObject);
    }

    private void TurnSprite(Vector2 turnPoint)
    {
        Vector2 direction = (new Vector2(transform.position.x, transform.position.y) - turnPoint).normalized;
        direction.x = Mathf.Round(direction.x);
        direction.y = Mathf.Round(direction.y);
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
    }

    private void Kill()
    {
        PlayerControll.instance.Tokens.Value += Bounty;
        Destroy(gameObject);
    }

    public void Hit(int dmg)
    {
        HP -= dmg;
    }

    [field: SerializeField] public float Speed { get; set; } = 4f;
    [field: SerializeField] public int HP { get; set; } = 100;
    [field: SerializeField] public int DEF { get; set; } = 0;
    [field: SerializeField] public int DMG { get; set; } = 1;
    [field: SerializeField] public int Bounty { get; set; } = 1;

    public List<Vector2> Path
    {
        set
        {
            path = new List<Vector2>(value);
        }
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(Mob))]
public class MobEditor : Editor
{
    public Mob mob { get { return (target as Mob); } }

    public override void OnInspectorGUI()
    {
        mob.HP = EditorGUILayout.IntField("HP:", mob.HP);
        mob.DEF = EditorGUILayout.IntField("Defense:", mob.DEF);
        mob.DMG = EditorGUILayout.IntField("Damage:", mob.DMG);
        mob.Bounty = EditorGUILayout.IntField("Bounty:", mob.Bounty);
        mob.Speed = EditorGUILayout.Slider("Speed:", mob.Speed, 1f, 20f);

        if (EditorApplication.isPlaying)
        {
            if (GUILayout.Button("Kill"))
            {
                mob.HP = 0;
            }
        }

    }
}
#endif
