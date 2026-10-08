using UnityEngine;

public class SmallGhost : EnemyBaseScript
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CheckBeforeMove();
    }

    // Update is called once per frame
    void Update()
    {
        Move(2.5f);
    }
}
