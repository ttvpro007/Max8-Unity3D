using UnityEngine;

namespace OSC_1
{
    public class OSCReceiver : MonoBehaviour
    {
        public string RemoteIP = "127.0.0.1"; //127.0.0.1 signifies a local host (if testing locally
        public int SendToPort = 5555; //the port you will be sending from
        public int ListenerPort = 2222; //the port you will be listening on
        public Transform controller;
        public float NoiseIntensity => val;

        private float val;

        // the OSC object
        private Osc osc;
        private Udp udp;

        private void Start()
        {
            udp = GetComponent<Udp>();
            udp.init(RemoteIP, SendToPort, ListenerPort);

            osc = GetComponent<Osc>();
            osc.init(udp);
            osc.SetAllMessageHandler(AllMessageHandler);
        }

        private void AllMessageHandler(OscMessage msg)
        {
            // log the OSC message
            Debug.Log(Osc.OscMessageToString(msg));

            // message parameters
            var address = msg.Address;
            var values = msg.Values;

            // different actions, based on the address pattern
            switch (address)
            {
                // FORMAT:  /cursor id group_id x y z x_world y_world z_world 
                case "/test":
                    // extract the data
                    val = (float)values[0];

                    // log the data
                    Debug.Log("Noise Intensity: " + val);
                    break;
            }
        }
    }
}