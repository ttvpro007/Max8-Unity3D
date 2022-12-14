using UnityEngine;

namespace OSC_1
{
    public class NoiseDeform : MonoBehaviour
    {
        [SerializeField] private float NoiseScale = 0.5f;
        [SerializeField] private float speed = 0.8f;
        [SerializeField] private bool recalculateNormals = false;

        private Vector3[] baseVertices;
        private Perlin noise;
        private GameObject oscReceiverGO;

        private void Awake()
        {
            oscReceiverGO = GameObject.Find("OSC Receiver");
        }

        private void Start()
        {
            noise = new Perlin();
        }

        private void Update()
        {
            var mesh = GetComponent<MeshFilter>().mesh;

            var intensity = oscReceiverGO.GetComponent<OSCReceiver>().NoiseIntensity;

            var scale = NoiseScale * intensity;

            if (baseVertices == null)
                baseVertices = mesh.vertices;

            var vertices = new Vector3[baseVertices.Length];

            var timex = Time.time * speed + 0.1365143f;
            var timey = Time.time * speed + 1.21688f;
            var timez = Time.time * speed + 2.5564f;
            for (var i = 0; i < vertices.Length; i++)
            {
                var vertex = baseVertices[i];

                vertex.x += noise.Noise(timex + vertex.x, timex + vertex.y, timex + vertex.z) * scale;
                vertex.y += noise.Noise(timey + vertex.x, timey + vertex.y, timey + vertex.z) * scale;
                vertex.z += noise.Noise(timez + vertex.x, timez + vertex.y, timez + vertex.z) * scale;

                vertices[i] = vertex;
            }

            mesh.vertices = vertices;

            if (recalculateNormals)
                mesh.RecalculateNormals();
            mesh.RecalculateBounds();
        }
    }
}