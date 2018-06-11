using System;
using System.Collections.Generic;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Exceptions;
using FusionFramework.Core.Utilities;
using static uPLibrary.Networking.M2Mqtt.MqttClient;
using FusionFramework.Transformer.Data;
using FusionFramework.Data.Segmentators;

namespace FusionFramework.Core.Data.Reader
{
    class MQTTReader : IReader
    {
        private MqttClient Client;
        private String UUID = "cc36fa72-2af8-4161-9004-a73a0d69aed7";
        

        public MQTTReader(string subscriberURL, ReadFinished onReadFinished)
        {
            Client = new MqttClient("cloud11.dbis.rwth-aachen.de");
            Client.Connect(Guid.NewGuid().ToString());

            Client.MqttMsgPublishReceived += MessageArrivedCallback;
            OnReadFinished = onReadFinished;
            Path = subscriberURL;
        }

        public MQTTReader(string subscriberURL, ReadFinished onReadFinished, ISegmentator segmentator)
        {
            Client = new MqttClient("cloud11.dbis.rwth-aachen.de");
            Client.Connect(Guid.NewGuid().ToString());

            Client.MqttMsgPublishReceived += MessageArrivedSegmentedCallback;
            Segmentator = segmentator;
            Path = subscriberURL;
        }

        public override void Start()
        {
            Client.Subscribe(new string[] { Path }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }

        private void MessageArrivedSegmentedCallback(object sender, MqttMsgPublishEventArgs e)
        {
            if (Segmentator.Push(Array.ConvertAll(e.Message.ToString().Split(','), double.Parse)) == true)
            {
                OnReadFinished(Segmentator.Window);
            }
        }

        private void MessageArrivedCallback(object sender, MqttMsgPublishEventArgs e)
        {
            OnReadFinished(Array.ConvertAll(e.Message.ToString().Split(','), double.Parse));
        }

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
