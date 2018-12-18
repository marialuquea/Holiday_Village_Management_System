using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using Business;

namespace Data
{
    /*
     * Author: Maria Luque Anguita 40280156
     * Description of class: Serialise lists to xml files and deserialise
     * Date last modified: 3/12/2017
     */
    [Serializable()]
    public class Serializer
    {   
        //Access the singleton instance of the lists
        ObjectsList dataLayerSingleton = ObjectsList.Instance;
        
        //serialise lists (save informatin to xml file)
        public void bookingsSerializer()
        {
            // Create a new XmlSerializer instance with the type of the test class
            XmlSerializer SerializerObj = new XmlSerializer(typeof(List<Booking>));

            // Create a new file stream to write the serialized object to a file
            TextWriter WriteFileStream = new StreamWriter(@"../../../Docs/bookings.xml");

            //write object into file
            SerializerObj.Serialize(WriteFileStream, dataLayerSingleton.allBookings);

            // Cleanup
            WriteFileStream.Close();
        }

        public void customerSerializer()
        {
            // Create a new XmlSerializer instance with the type of the test class
            XmlSerializer SerializerObj = new XmlSerializer(typeof(List<Customer>));

            // Create a new file stream to write the serialized object to a file
            TextWriter WriteFileStream = new StreamWriter(@"../../../Docs/customerS.xml");
                
            //write object into file
            SerializerObj.Serialize(WriteFileStream, dataLayerSingleton.allCustomers);

            // Cleanup
            WriteFileStream.Close();
            
        }

        //deserialize lists (read information from xml file)
        public void bookingDeserializer()
        {
            //Load the object

            //Create a new XmlSerializer with the type of the test class
            XmlSerializer SerializerObj = new XmlSerializer(typeof(List<Booking>));

            // Create a new file stream for reading the XML file
            FileStream ReadFileStream = new FileStream(@"../../../Docs/bookings.xml", FileMode.Open, FileAccess.Read, FileShare.Read);

            // Load the object saved above by using the Deserialize function
            List<Booking> loadedObj = (List<Booking>)SerializerObj.Deserialize(ReadFileStream);

            dataLayerSingleton.allBookings = loadedObj;

            // Cleanup
            ReadFileStream.Close();
        }

        public void customerDeserializer()
        {
            //Load the object

            //Create a new XmlSerializer with the type of the test class
            XmlSerializer SerializerObj = new XmlSerializer(typeof(List<Customer>));

            // Create a new file stream for reading the XML file
            FileStream ReadFileStream = new FileStream(@"../../../Docs/customerS.xml", FileMode.Open, FileAccess.Read, FileShare.Read);

            // Load the object saved above by using the Deserialize function
            List<Customer> loadedObj = (List<Customer>)SerializerObj.Deserialize(ReadFileStream);

            dataLayerSingleton.allCustomers = loadedObj;

            // Cleanup
            ReadFileStream.Close();

            
        }
    
    }
}
