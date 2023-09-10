using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UIElements;

namespace PhysicsLab
{
    public class SolarSystemController : MonoBehaviour
    {
        [SerializeField] private float sizeScale = 100000;
        [SerializeField] private float distanceScale = 2;
        [SerializeField] private float speedScale = 100;
        [SerializeField] private ParticleSystem asteroids;
        [SerializeField] private Planet[] planets;
        [SerializeField] private Planet moon;

        [Tooltip("\"Revolution\" refers the object's orbital motion around another object")]
        [SerializeField] private bool planetsCanRevolute;
        
        [Tooltip("\"Rotation\" refers to an object's spinning motion about its own axis.")]
        [SerializeField] private bool planetsCanRotate;

        private const int CHANGE_SPEED_MULTIPLIER = 200;
        private void Start()
        {
            SetPlanets();
        }
        
        private void Update()
        {
            for (int i = 1; i < planets.Length; i++)
            {
                if(planetsCanRotate)planets[i].RotatePlanet(speedScale);
                if(planetsCanRevolute)planets[i].RevolutePlanet(speedScale);
            }
            if (planetsCanRotate) moon.RevolutePlanet(speedScale);
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

                planets[i].planetNameText.text = planets[i].planetName;
                planets[i].planetNameText.transform.position = planets[i].planetObject.transform.position;
            }

            moon.SetObject(sizeScale, distanceScale, planets[3].planetObject.transform);
            moon.planetObject.transform.parent = planets[3].planetObject.transform;
        }

        public void ChangeSpeed(float newValue)
        {
            speedScale = newValue * CHANGE_SPEED_MULTIPLIER;
        }

        [ContextMenu("Stop All The Planets")]
        public void StopsAllThePlanets() {
            speedScale = 0;
        }

        [ContextMenu("Start All The Planets")]
        public void StartAllThePlanets() {
            speedScale = 100;
        }

        [ContextMenu("Planets Names On")]
        public void PlanetsNamesOn()
        {
            foreach (Planet planet in planets)
            {
                planet.planetNameText.gameObject.SetActive(true);
            }
        }

        [ContextMenu("Planets Names Off")]
        public void PlanetsNamesOff()
        {
            foreach (Planet planet in planets)
            {
                planet.planetNameText.gameObject.SetActive(false);
            }

        }

        [ContextMenu("Planets Rotates Off")]
        public void PlanetsRotationOff()
        {
            planetsCanRotate = false;

        }

        [ContextMenu("Planets Rotates On")]
        public void PlanetsRotationOn()
        {
            planetsCanRotate = true;

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
        public TextMesh planetNameText;

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