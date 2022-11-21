using UnityEngine;
using OscJack;

public class OSCReceiver : MonoBehaviour
{
    UnityEngine.Object sync = new UnityEngine.Object();

    [SerializeField] int port = 12345;
    [SerializeField, Range(1f, 5f)] float scale = 1f;
    [SerializeField] Transform transform1;
    [SerializeField] Transform transform2;

    OscServer server;
    int index = 0;
    Vector3 pos = Vector3.zero;

    private void OnEnable()
    {
        server = new OscServer(port);
        server.MessageDispatcher.AddCallback(
            "/index-xyz", // OSC address
            (string address, OscDataHandle data) =>
            {
                index = data.GetElementAsInt(0);
                pos.x = data.GetElementAsFloat(1) * scale;
                pos.y = data.GetElementAsFloat(2) * scale;
                pos.z = data.GetElementAsFloat(3) * scale;
            }
        );

        if (transform1) transform1.position = pos;
        if (transform2) transform1.position = pos;
    }

    private void Update()
    {
        switch (index)
        {
            case 1:
                if (transform1) transform1.position = pos;
                break;
            case 2:
                if (transform2) transform2.position = pos;
                break;
            default:
                break;
        }
    }

    private void LateUpdate()
    {
        index = 0;
    }

    private void OnDisable()
    {
        server.Dispose();
        server = null;
    }
}
