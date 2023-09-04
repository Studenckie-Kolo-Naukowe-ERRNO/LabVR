using System.Collections.Generic;
using UnityEngine;

namespace GeoLab {
    public class MineRockOnHit : MonoBehaviour {
        [SerializeField] private Vector3 smallRocksOffest;
        [SerializeField] private Vector3 smallRocksSize;

        [SerializeField] private float health = 1000;
        [SerializeField] private GameObject[] smallRocksPrefabs;
        private List<GameObject> smallRocksGameobject = new List<GameObject>();

        private void Start() {
            LoadSmallRocksOnScene();
        }

        private void OnCollisionEnter(Collision collision) {
            if (collision.gameObject.layer == 8) {
                Vector3 collisionForce = collision.impulse / Time.fixedDeltaTime;
                health -= collisionForce.magnitude;

                if (health <= 0) {
                    DestroyTheRock();
                }
            }
        }

        private void OnDestroy() {
            CreateSmallRocks();
        }

        private void LoadSmallRocksOnScene() {
            foreach (GameObject smallRock in smallRocksPrefabs) {
                GameObject rock = Instantiate(smallRock);
                rock.SetActive(false);
                rock.transform.localScale = smallRocksSize;
                smallRocksGameobject.Add(rock);
            }
        }

        private void DestroyTheRock() {
            Destroy(gameObject);
            return;
        }

        private void CreateSmallRocks() {
            gameObject.GetComponent<MeshCollider>().enabled = false;
            foreach (GameObject smallRock in smallRocksGameobject) {
                smallRock.transform.position = transform.position + smallRocksOffest;
                smallRock.SetActive(true);
            }
        }
    }
}
