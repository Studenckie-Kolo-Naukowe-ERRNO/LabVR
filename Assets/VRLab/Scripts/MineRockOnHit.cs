using System.Collections.Generic;
using UnityEngine;


namespace GeoLab {
    [RequireComponent(typeof(AudioSource))]
    public class MineRockOnHit : MonoBehaviour {
        [SerializeField] private Vector3 smallRocksOffest;
        [SerializeField] private Vector3 smallRocksSize;
        [SerializeField] private float getMineralsChance;

        [SerializeField] private float health = 1000;
        [SerializeField] private GameObject[] smallRocksPrefabs;
        [SerializeField] private GameObject[] mineralsPrefabs;
        [SerializeField] private ParticleSystem particleSystem;

        private List<GameObject> smallRocksGameobject = new List<GameObject>();
        private GameObject mineralGameobject;
        private int toolsLayer;

        [SerializeField] private AudioClip[] hitSounds;
        private AudioSource thisAudioSource;
        private void Start() {
            toolsLayer = LayerMask.NameToLayer("Tools");
            mineralGameobject = null;

            float randomValue = Random.Range(0f, 1f);

            LoadSmallRocksOnScene();

            if (randomValue < getMineralsChance) {
                LoadMineralsOnScene();
            }

            thisAudioSource = GetComponent<AudioSource>();
        }

        private void OnCollisionEnter(Collision collision) 
        {
            if (collision.gameObject.layer == toolsLayer) 
            {
                Vector3 collisionForce = collision.impulse / Time.fixedDeltaTime;
                health -= collisionForce.magnitude;

                thisAudioSource.PlayOneShot(RandomHitClip());

                if (health <= 0) 
                {
                    DestroyTheRock();
                }
            }
        }

        private void LoadSmallRocksOnScene() {
            foreach (GameObject smallRock in smallRocksPrefabs) {
                GameObject rock = Instantiate(smallRock);
                rock.SetActive(false);
                rock.transform.localScale = smallRocksSize;
                smallRocksGameobject.Add(rock);
            }
        }

        private void LoadMineralsOnScene() {
            int randomMineral = Random.Range(0, mineralsPrefabs.Length);

            mineralGameobject = Instantiate(mineralsPrefabs[randomMineral]);
        }

        private void DestroyTheRock() {
            particleSystem.gameObject.transform.SetParent(null, false);
            particleSystem.gameObject.transform.position = transform.gameObject.transform.position;
            particleSystem.Play();

            CreateSmallRocks();

            if (mineralGameobject != null) {
                CreateMineral();
            }

            Destroy(gameObject);
            return;
        }

        private void CreateSmallRocks() {
            foreach (GameObject smallRock in smallRocksGameobject) {
                smallRock.transform.position = transform.position + smallRocksOffest;
                smallRock.SetActive(true);
            }
        }

        private void CreateMineral() {
            mineralGameobject.transform.position = transform.position + smallRocksOffest;
            mineralGameobject.SetActive(true);
        }

        private AudioClip RandomHitClip()
        {
            return hitSounds[Random.Range(0, hitSounds.Length)];
        }
    }
}
