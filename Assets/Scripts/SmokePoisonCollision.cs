using UnityEngine;

public class SmokePoisonCollision : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Particle hit: " + other.name);

        BossAIController boss = other.GetComponent<BossAIController>();
        if (boss != null)
        {
            Debug.Log("Boss bị trúng độc!");
            boss.SetPoisoned();
        }
    }
}