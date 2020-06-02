#MSFusion.NET
## Introduction

MSFusion.NET is composed of two sub project including Context manager can be considered as the operator of the system; it is the central part of communication between configurations created by di↵erent authors at ARLEM panel, the learner (the user who is using the system for learning), the sensor interface, other components of the system and hosting application. Context manager dictates all the components about their respective next steps and provides feedback to the hosting application.

FusionFramework is a framework to provide required functionalities to perform multisensor data fusion. The process of combining observations from many different sensors to provide a robust and complete description of an environment is called Multisensor data fusion.

### Configuration
A user can set MQTT Broker for multisensor data fusion framework in the file ./FusionFramework/Data/Readers/MQTTReader.cs

Copyright (c) 2017-present, ACIS RWTH Aachen.
