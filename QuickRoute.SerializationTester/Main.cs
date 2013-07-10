using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace QuickRoute.SerializationTester
{
  [Serializable]
  class Document
  {
    public List<int> mIntList;
    static int cnt = 1035;

    public Document()
    {
      mIntList = new List<int>();
      for(int i = 0; i < cnt; i++)
        mIntList.Add(i);
      cnt = cnt -17;
    }
  };

  [Serializable]
  public class SerializableDictionary<TKey, TValue>
  {
      /// <summary>
      /// List of serializable dictionary entries.
      /// </summary>
      [XmlElement("Entry")]
      public List<KeyAndValue<TKey, TValue>> Entries { get; set; }
  
      /// <summary>
      /// Serializable key-value pair.
      /// </summary>
      [Serializable]
      public class KeyAndValue<TKey1, TValue1>
      {
          /// <summary>
          /// Key that is mapped to a value.
          /// </summary>
          public TKey1 Key { get; set; }
  
          /// <summary>
          /// Value the key is mapped to.
          /// </summary>
          public TValue1 Value { get; set; }
      }
  }

  class MainClass
  {
    const string fileName = "test1.bin";

    static void Serialize()
    {
      //using KV = SerializableDictionary<string, Document>.KeyAndValue<string, Document>;
      var myDict = new SerializableDictionary<string, Document>();
      myDict.Entries = new List<SerializableDictionary<string, Document>.KeyAndValue<string, Document>>();
      var myElem = new SerializableDictionary<string, Document>.
                         KeyAndValue<string, Document>();
      myElem.Key = "Hello World!";
      myElem.Value = new Document();
      myDict.Entries.Add(myElem);
//      myDict.Entries.Add(new SerializableDictionary<string, Document>.
//                         KeyAndValue<string, Document>({Key = "Hello World!", ValueType = new Document()}));
      myElem = null;
      myElem = new SerializableDictionary<string, Document>.
                         KeyAndValue<string, Document>();
      myElem.Key = "Second";
      myElem.Value = new Document();
      myDict.Entries.Add(myElem);

      myElem = null;
      myElem = new SerializableDictionary<string, Document>.
                         KeyAndValue<string, Document>();
      myElem.Key = "Third";
      myElem.Value = new Document();
      myDict.Entries.Add(myElem);

      IFormatter formatter = new BinaryFormatter();
      Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
      try
      {
        formatter.Serialize(stream, myDict);
        //FileName = fileName;
      }
      catch (SerializationException e) 
      {
        Console.WriteLine("Failed to serialize. Reason: " + e.Message);
        throw;
      }
      finally
      {
        stream.Close();
      }
      Console.WriteLine ("S: OK");
    }

    static void Deserialize()
    {
      SerializableDictionary<string, Document> myDict = null;

      // Open the file containing the data that you want to deserialize.
      FileStream fs = new FileStream(fileName, FileMode.Open);
      try 
      {
        BinaryFormatter formatter = new BinaryFormatter();
        
        // Deserialize the hashtable from the file and 
        // assign the reference to the local variable.
        myDict = (SerializableDictionary<string, Document>) formatter.Deserialize(fs);
      }
      catch (SerializationException e) 
      {
        Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
        throw;
      }
      finally 
      {
        fs.Close();
      }

      // To prove that the table deserialized correctly, 
      // display the key/value pairs.
      foreach (SerializableDictionary<string, Document>.KeyAndValue<string, Document> kv in myDict.Entries) 
      {
        Console.WriteLine("{0} lives at {1}.", kv.Key, kv.Value.mIntList.Count);
      }

      Console.WriteLine ("D: OK");
    }

    public static void Main (string[] args)
    {
      Console.WriteLine ("==================BinaryFormatter======================");
      Serialize ();
      Deserialize();
      Console.WriteLine ("==================XmlSerializer======================");
      Test.Main1();
      Console.WriteLine ("==================protobuf-net======================");

      // http://code.google.com/p/protobuf-net/source/browse/#svn%2Ftrunk%2FExamples
      Console.WriteLine ("==================ExifWorks======================");
      var a = new ExifTester();

      Console.WriteLine ("==================SerializeFail======================");
      // http://www.mail-archive.com/mono-bugs@lists.ximian.com/msg65268.html
      // https://bugzilla.novell.com/show_bug.cgi?id=527199
      SerializeFail.SerializeFail.Main2(new string[]{"client"});
      SerializeFail.SerializeFail.Main2(new string[]{"server"});
      // http://docs.xamarin.com/guides/ios/advanced_topics/limitations
      // Value types as Dictionary Keys
      // Using a value type as a Dictionary<TKey, TValue> key is problematic, as the default Dictionary
      // constructor attempts to use EqualityComparer<TKey>.Default. EqualityComparer<TKey>.Default, in turn,
      // attempts to use Reflection to instantiate a new type which implements the IEqualityComparer<TKey> interface.
      // This works for reference types (as the reflection+create a new type step is skipped), but for value
      // types it crashes and burns rather quickly once you attempt to use it on the device.
      // Workaround: Manually implement the  IEqualityComparer<TKey>
      // (http://www.go-mono.com/docs/index.aspx?link=T%3aSystem.Collections.Generic.IEqualityComparer%601)
      // interface in a new type and provide an instance of that type to the
      // Dictionary<TKey, TValue>(IEqualityComparer<TKey>) constructor
      // (http://www.go-mono.com/docs/monodoc.ashx?link=C%3aSystem.Collections.Generic.Dictionary%602(System.Collections.Generic.IEqualityComparer%7b%600%7d)).
    }
  }
}
