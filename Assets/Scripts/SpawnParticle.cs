using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParticle : MonoBehaviour
{
    [SerializeField]
    private ParticleConfig[] _particleConfig;


    public void SpawnParticles(ParticleType type)
    {
        for (int i = 0; i < _particleConfig.Length; i++)
        {
            if (type == _particleConfig[i].Type)
            {
                Spawn(_particleConfig[i]);

                break;
            }
        }
    }


    public void SpawnParticles()
    {
        if (_particleConfig.Length > 0)
            Spawn(_particleConfig[0]);
        else
            Debug.LogWarning("No particles");
    }


    private void Spawn(ParticleConfig config)
    {
        var randParticleIndex = Random.Range(0, config.Particle.Length);
        var randParticle = config.Particle[randParticleIndex];

        var spawnedParticle = new List<GameObject>();

        for (int i = 0; i < config.SpawnPlace.Length; i++)
        {
            var pos = config.SpawnPlace[i].position;

            spawnedParticle.Add(Instantiate(randParticle, pos, Quaternion.identity));
        }

        DestroyParticles(spawnedParticle.ToArray(), config.LifeTime);
    }


    private void DestroyParticles(GameObject[] particle, float lifeTime)
    {
        for (int i = 0; i < particle.Length; i++)
        {
            Destroy(particle[i], lifeTime);
        }
    }
}