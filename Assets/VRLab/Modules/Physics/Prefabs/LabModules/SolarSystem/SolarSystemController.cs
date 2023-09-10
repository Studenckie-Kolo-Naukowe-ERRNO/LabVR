using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UIElements;

namespace PhysicsLab
{
    public class SolarSystemController : MonoBehaviour
    {
        [SerializeField] private GameObject cameraGamobject;
        [SerializeField] private float sizeScale = 100000;
        [SerializeField] private float distanceScale = 2;
        [SerializeField] private float speedScale = 100;
        [SerializeField] private ParticleSystem asteroids;
        [SerializeField] private Planet[] planets;
        [SerializeField] private Planet moon;
        [SerializeField] private bool PlanetNamesTextAllignToPlayer;

        [Tooltip("\"Revolution\" refers the object's orbital motion around another object")]
        [SerializeField] private bool planetsCanRevolute;
        
        [Tooltip("\"Rotation\" refers to an object's spinning motion about its own axis.")]
        public bool planetsCanRotate;

        private const int CHANGE_SPEED_MULTIPLIER = 200;
        private void Start()
        {
            if (cameraGamobject == null) {
                cameraGamobject = GameObject.FindGameObjectWithTag("MainCamera");
            }
            SetPlanets();
        }
        
        private void Update()
        {
            for (int i = 1; i < planets.Length; i++)
            {
                if(planetsCanRotate)planets[i].RotatePlanet(speedScale);
                if(planetsCanRevolute)planets[i].RevolutePlanet(speedScale);
                if (PlanetNamesTextAllignToPlayer && cameraGamobject != null)
                    planets[i].planetNameText.transform.LookAt(cameraGamobject.transform);
            }
            if (planetsCanRotate) moon.RevolutePlanet(speedScale);
        }

        private void OnTriggerEnter(Collider collider) 
        {
            if (collider.CompareTag("Player")) {
                PlanetNamesTextAllignToPlayer = true;
            }
        }

        private void OnTriggerExit(Collider collider) 
        {
            if (collider.CompareTag("Player")) {
                PlanetNamesTextAllignToPlayer = false;
            }
        }

        [ContextMenu("Set planets")]
        public void SetPlanets()
        {
            asteroids.transform.localScale = new Vector3(distanceScale, distanceScale, distanceScale);
            asteroids.Clear();
            asteroids.Play();
            for (int i = 1; i < planets.Length; i++)
            {
                planets[i].SetObject(sizeScale, distanceScale, planets[0].planetObject.transform);

                planets[i].planetNameText.SetText(planets[i].planetName);
                planets[i].planetNameText.transform.localPosition = new Vector3(0, -1, 0);
            }

            moon.SetObject(sizeScale, distanceScale, planets[3].planetObject.transform);
            moon.planetObject.transform.parent = planets[3].planetObject.transform;
        }

        public void ChangeSpeed(float newValue)
        {
            speedScale = newValue * CHANGE_SPEED_MULTIPLIER;
        }

        [ContextMenu("Toggle movement of all The Planets")]
        public void TogglePlanetsMovement() {
            planetsCanRevolute = !planetsCanRevolute;
        }


        [ContextMenu("Toggle Planets Names")]
        public void TogglePlanetsNames()
        {
            foreach (Planet planet in planets)
            {
                planet.planetNameText.gameObject.SetActive(!planet.planetNameText.gameObject.activeSelf);
            }
        }


        [ContextMenu("Toggle Planets Rotation")]
        public void TogglePlanetsRotation()
        {
            planetsCanRotate = !planetsCanRotate;

        }

    }

    [System.Serializable]
    public class Planet
    {
        public string planetName;
        [Tooltip("(km)")]
        public float diameter;
        [Tooltip("(au)")]
        public float distanceFromSun;
        [Tooltip("(au)")]
        public float distanceOffset;
        [Space]
        public float rotationPeriod;
        public float revolutionPeriod;
        public GameObject planetObject;
        public TextMeshPro planetNameText;

        private Transform rotateAround;

        public void SetObject(float scale, float dScale, Transform rotateAnhor)
        {
            if (planetObject == null) return;
            rotateAround = rotateAnhor;

            float size = ((float)Mathp.KmToAu(diameter) / dScale)*scale;
            planetObject.transform.localScale = new Vector3(size, size, size);
            planetObject.transform.localPosition = rotateAround.localPosition + new Vector3((distanceFromSun+ distanceOffset) / dScale, 0, 0);
        }
        public void RevolutePlanet(float speed)
        {
            if (revolutionPeriod != 0)
            {
                float angle = (speed * Time.deltaTime) / (revolutionPeriod / 365.25f);
                planetObject.transform.RotateAround(rotateAround.position, rotateAround.transform.up, angle);
            }
        }

        public void RotatePlanet(float speed)
        {
            if (rotationPeriod!=0)
            {
                planetObject.transform.Rotate((Vector3.up * speed) / (rotationPeriod * Time.deltaTime));
            }
        }
        
    }
}