using System;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Exceptions;
using FusionFramework.Core.Utilities;
using FusionFramework.Data.Segmentators;

namespace FusionFramework.Core.Data.Reader
{
    /// <summary>
    /// Reads stream using MQTT protocol.
    /// </summary>
    public class MQTTReader<T> : IReader
    {
        private MqttClient Client;

        /// <summary>
        /// Segmentation if required by client for the incoming data.
        /// </summary>
        public SlidingWindow<T> Segmentator;


        /// <summary>
        /// Instantiate the MQTT reader without segmentator.
        /// </summary>
        /// <param name="subscriberURL">MQTT topic address</param>
        public MQTTReader(string subscriberURL)
        {
            Client = new MqttClient("cloud11.dbis.rwth-aachen.de");
            Client.Connect(Guid.NewGuid().ToString());

            Client.MqttMsgPublishReceived += MessageArrivedCallback;
            Path = subscriberURL;
        }

        /// <summary>
        /// Instantiate the MQTT reader without segmentator.
        /// </summary>
        /// <param name="subscriberURL">MQTT topic address</param>
        /// <param name="onReadFinished">Trigger when reading finished.</param>
        public MQTTReader(string subscriberURL, ReadFinished onReadFinished)
        {
            Client = new MqttClient("cloud11.dbis.rwth-aachen.de");
            Client.Connect(Guid.NewGuid().ToString());

            Client.MqttMsgPublishReceived += MessageArrivedCallback;
            OnReadFinished = onReadFinished;
            Path = subscriberURL;
        }

        /// <summary>
        /// Instantiate the MQTT reader with segmentator.
        /// </summary>
        /// <param name="subscriberURL">MQTT topic address</param>
        /// <param name="segmentator">Segmentation class that breaks the file in segementation / windows</param>
        public MQTTReader(string subscriberURL, SlidingWindow<T> segmentator)
        {
            Client = new MqttClient("cloud11.dbis.rwth-aachen.de");
            Client.Connect(Guid.NewGuid().ToString());

            Client.MqttMsgPublishReceived += MessageArrivedSegmentedCallback;

            Segmentator = segmentator;
            Path = subscriberURL;
        }

        /// <summary>
        /// Start Reading
        /// </summary>
        public override void Start()
        {
            Client.Subscribe(new string[] { Path }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }

        /// <summary>
        /// Stop Reading
        /// </summary>
        public override void Stop()
        {
            Client.Unsubscribe(new string[] { Path });
        }

        /// <summary>
        /// Triggers when message is recieved from MQTT server and perform segmentation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageArrivedSegmentedCallback(object sender, MqttMsgPublishEventArgs e)
        {
            try
            {
                if (Segmentator.Push((T)Convert.ChangeType(Array.ConvertAll(System.Text.Encoding.UTF8.GetString(e.Message).Split(','), double.Parse), typeof(T))) == true)
                {
                    OnReadFinished(Segmentator.Window);
                }

            }
            catch (System.FormatException)
            {

            }
        }

        /// <summary>
        /// Triggers when message is recieved from MQTT server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageArrivedCallback(object sender, MqttMsgPublishEventArgs e)
        {
            OnReadFinished(Array.ConvertAll(System.Text.Encoding.UTF8.GetString(e.Message).Split(','), double.Parse));
        }

        /// <summary>
        /// Test connection with server.
        /// </summary>
        public static void TestConnection()
        {
            try
            {
                MqttClient mqttClient = new MqttClient("cloud11.dbis.rwth-aachen.de");
                mqttClient.Connect(Guid.NewGuid().ToString());
                mqttClient.Disconnect();
            }
            catch (MqttConnectionException)
            {
                Logger.Log("You forgot it again don't ya? Start the damn MQTT Server first.", "Wait Sparky!!!");
                // TODO: Terminate app
            }

        }        
    }
}
