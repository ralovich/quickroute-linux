using System;
using Microsoft.VisualBasic;
using System.Drawing;

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
  }
}

