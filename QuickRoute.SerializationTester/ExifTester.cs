using System;
using Microsoft.VisualBasic;
using System.Drawing;
using Exiv2;
using GLib;

namespace QuickRoute.SerializationTester
{
  public class ExifTester
  {
    public ExifTester ()
    {
      string fname = "/home/tade/work/maps/2010_08_22_FrenchCreekNorth/2010_08_22_FrenchCreekNorth.jpg";
      var ew = new ExifWorks(fname);
      Console.WriteLine("ew=" + ew.ToString());
      Console.WriteLine("width=" + ew.Width);

//      var ver = new byte[] { 2, 2, 0, 0 };
//      var longitudeRef = new byte[] { Convert.ToByte('W'), 0 };
//      //var longitude = ExifUtil.GetExifGpsCoordinate(center.Longitude);
//      var latitudeRef = new byte[] { Convert.ToByte( 'S'), 0 };
//      //var latitude = ExifUtil.GetExifGpsCoordinate(center.Latitude);
//      exif.SetProperty((int)ExifWorks.ExifWorks.TagNames.GpsVer, ver, ExifWorks.ExifWorks.ExifDataTypes.UnsignedLong);
//      exif.SetProperty((int)ExifWorks.ExifWorks.TagNames.GpsLongitudeRef, longitudeRef, ExifWorks.ExifWorks.ExifDataTypes.AsciiString);
//      //exif.SetProperty((int)ExifWorks.ExifWorks.TagNames.GpsLongitude, longitude, ExifWorks.ExifWorks.ExifDataTypes.UnsignedRational);
//      exif.SetProperty((int)ExifWorks.ExifWorks.TagNames.GpsLatitudeRef, latitudeRef, ExifWorks.ExifWorks.ExifDataTypes.AsciiString);
//      //exif.SetProperty((int)ExifWorks.ExifWorks.TagNames.GpsLatitude, latitude, ExifWorks.ExifWorks.ExifDataTypes.UnsignedRational);
//      //if (Properties.EncodingInfo.Encoder.MimeType == "image/jpeg")
//      //{
//      //  exif.SetProperty((int)ExifWorks.ExifWorks.TagNames.JPEGQuality, new byte[] {(byte)(100 * ((JpegEncodingInfo)Properties.EncodingInfo).Quality)}, ExifWorks.ExifWorks.ExifDataTypes.UnsignedByte);
//      //}

      SetExifData(ew.GetBitmap());
    }

    static public void SetExifData(Bitmap image)
    {
      Console.WriteLine("================================\n================================\n");
      try
      {
      var exif = new ExifWorks(ref image);

      //var g = new System.Drawing.Graphics();
      //var g2 = new PdnGraphics();

      Console.WriteLine("ew=" + exif.ToString());
      Console.WriteLine("width=" + exif.Width);

      var ver = new byte[] { 2, 2, 0, 0 };
      var longitudeRef = new byte[] { Convert.ToByte('W'), 0 };
      //var longitude = ExifUtil.GetExifGpsCoordinate(center.Longitude);
      var latitudeRef = new byte[] { Convert.ToByte( 'S'), 0 };
      //var latitude = ExifUtil.GetExifGpsCoordinate(center.Latitude);
      exif.SetProperty((int)ExifWorks.TagNames.GpsVer, ver, ExifWorks.ExifDataTypes.UnsignedLong);
      exif.SetProperty((int)ExifWorks.TagNames.GpsLongitudeRef, longitudeRef, ExifWorks.ExifDataTypes.AsciiString);
      //exif.SetProperty((int)ExifWorks.ExifWorks.TagNames.GpsLongitude, longitude, ExifWorks.ExifWorks.ExifDataTypes.UnsignedRational);
      exif.SetProperty((int)ExifWorks.TagNames.GpsLatitudeRef, latitudeRef, ExifWorks.ExifDataTypes.AsciiString);
      //exif.SetProperty((int)ExifWorks.ExifWorks.TagNames.GpsLatitude, latitude, ExifWorks.ExifWorks.ExifDataTypes.UnsignedRational);
      //if (Properties.EncodingInfo.Encoder.MimeType == "image/jpeg")
      //{
      //  exif.SetProperty((int)ExifWorks.ExifWorks.TagNames.JPEGQuality, new byte[] {(byte)(100 * ((JpegEncodingInfo)Properties.EncodingInfo).Quality)}, ExifWorks.ExifWorks.ExifDataTypes.UnsignedByte);
      //}
      }
      catch(System.IndexOutOfRangeException e)
      {}
      Console.WriteLine("================================\n================================\n");
    }

    static public void Exiv2Main()
    {
      GLib.GType.Init ();
      try {
        string fname = "/home/tade/work/maps/2010_08_22_FrenchCreekNorth/2010_08_22_FrenchCreekNorth.jpg";
        Exiv2.Image image = Exiv2.ImageFactory.Open (fname);
        if (!image.Good)
          return;
        image.ReadMetadata ();

        Console.WriteLine ("Mime type: {0}", image.MimeType);
        Console.WriteLine ("pixel width: {0}", image.PixelWidth);
        Console.WriteLine ("pixel height: {0}", image.PixelHeight);
        Console.WriteLine ("comment: {0}", image.Comment);

        //image.ExifData.FindKey(

        image.Comment = "new comment";
        //image.ClearMetadata ();

        ExifData exif_data = image.ExifData;
        if (exif_data.IsEmpty) {
          Console.WriteLine ("No Exif data found in file");
          return;
        }

        Console.WriteLine ("Exif count: {0}", exif_data.Count);
        foreach (ExifDatum datum in exif_data)
          Console.WriteLine ("{0} {1} {2} {3} {4}",
              datum.Key,
              datum.Tag, 
              datum.Typename,
              datum.Count,
              datum.ToString ());

#if False
        // This is the quickest way to add (simple) Exif data. If a metadatum for
        // a given key already exists, its value is overwritten. Otherwise a new
        // tag is added.
        exifData["Exif.Image.Model"] = "Test 1";        // AsciiValue
        exifData["Exif.Image.SamplesPerPixel"] = (UInt16)162;     // UShortValue
        exifData["Exif.Image.XResolution"] = (Int32)(-2);     // LongValue
        exifData["Exif.Image.YResolution"] = new Exiv2.Rational (-2, 3);  // RationalValue
        exifData["Exif.Photo.DateTimeOriginal"] = "1999:12:31 23:59:59";  // AsciiValue
        Console.WriteLine ("Added a few tags the quick way.");

        // Modify Exif data

        // Since we know that the metadatum exists (or we don't mind creating a new
        // tag if it doesn't), we can simply do this:
        ExifDatum tag = exifData["Exif.Photo.DateTimeOriginal"] as ExifDatum;
        string date = tag.ToString ();
        date = "2000" + ((date.Length > 4) ? date.Substring (4) : null);
        tag.Value = date;
        Console.WriteLine ("Modified key '{0}', new value '{1}'", tag.Key, tag.ToString ());

//        tag.setValue(date);
//        std::cout << "Modified key \"" << key
//                  << "\", new value \"" << tag.value() << "\"\n";
//        
//        // Alternatively, we can use findKey()
//        key = Exiv2::ExifKey("Exif.Image.PrimaryChromaticities");
//        Exiv2::ExifData::iterator pos = exifData.findKey(key);
//        if (pos == exifData.end()) throw Exiv2::Error(1, "Key not found");
//        // Get a pointer to a copy of the value
//        v = pos->getValue();
//        // Downcast the Value pointer to its actual type
//        Exiv2::URationalValue* prv = dynamic_cast<Exiv2::URationalValue*>(v.release());
//        if (prv == 0) throw Exiv2::Error(1, "Downcast failed");
//        rv = Exiv2::URationalValue::AutoPtr(prv);
//        // Modify the value directly through the interface of URationalValue
//        rv->value_[2] = std::make_pair(88,77);
//        // Copy the modified value back to the metadatum
//        pos->setValue(rv.get());
//        std::cout << "Modified key \"" << key
//                  << "\", new value \"" << pos->value() << "\"\n";

        exifData.Erase (new ExifKey("Exif.Image.XResolution"));
#endif

        image.WriteMetadata ();

      } catch (GLib.GException e) {
        Console.WriteLine ("Exiv2.Exception caught {0}", e);
      }

    }
  }
}

