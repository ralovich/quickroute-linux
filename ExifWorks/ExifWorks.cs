// http://code.google.com/p/sporttracks-activitypicture/source/browse/trunk/ActivityPicturePlugin/Helper/ExifWorks.cs?r=37
/*
Copyright (C) 2008 Dominik Laufer

This library is free software; you can redistribute it and/or
modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation; either
version 3 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public
License along with this library. If not, see <http://www.gnu.org/licenses/>.
 */

using System.IO;
using System;
//using Microsoft.VisualBasic;
using System.Drawing;

namespace ExifWorks
    {
    public class ExifWorks : System.IDisposable
        {
        private System.Drawing.Bitmap _Image;
        private System.Text.Encoding _Encoding = System.Text.Encoding.Default;
        private const string vbNullChar = "\0";

        public ExifWorks(string FileName)
        {
            try
            {
                //the file is not locked and may be modified/deleted later
                System.IO.FileStream ImageFile = new System.IO.FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader Reader = new BinaryReader(ImageFile);
                MemoryStream ImageStream = new MemoryStream(Reader.ReadBytes((int)ImageFile.Length));
                Reader.Close();
                _Image = (System.Drawing.Bitmap)System.Drawing.Image.FromStream(ImageStream);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public ExifWorks(ref Bitmap image)
        {
           _Image = image;
        }
        public System.Drawing.Bitmap GetBitmap()
        {
            if (this._Image != null)
            {
                return (System.Drawing.Bitmap)this._Image.Clone();
            }
            else
            { return null; }
        }

        #region " Type declarations "
        public enum TagNames : int
            {
            ExifIFD = 34665,
            GpsIFD = 34853,
            NewSubfileType = 254,
            SubfileType = 255,
            ImageWidth = 256,
            ImageHeight = 257,
            BitsPerSample = 258,
            Compression = 259,
            PhotometricInterp = 262,
            ThreshHolding = 263,
            CellWidth = 264,
            CellHeight = 265,
            FillOrder = 266,
            DocumentName = 269,
            ImageDescription = 270,
            EquipMake = 271,
            EquipModel = 272,
            StripOffsets = 273,
            Orientation = 274,
            SamplesPerPixel = 277,
            RowsPerStrip = 278,
            StripBytesCount = 279,
            MinSampleValue = 280,
            MaxSampleValue = 281,
            XResolution = 282,
            YResolution = 283,
            PlanarConfig = 284,
            PageName = 285,
            XPosition = 286,
            YPosition = 287,
            FreeOffset = 288,
            FreeByteCounts = 289,
            GrayResponseUnit = 290,
            GrayResponseCurve = 291,
            T4Option = 292,
            T6Option = 293,
            ResolutionUnit = 296,
            PageNumber = 297,
            TransferFuncition = 301,
            SoftwareUsed = 305,
            DateTime = 306,
            Artist = 315,
            HostComputer = 316,
            Predictor = 317,
            WhitePoint = 318,
            PrimaryChromaticities = 319,
            ColorMap = 320,
            HalftoneHints = 321,
            TileWidth = 322,
            TileLength = 323,
            TileOffset = 324,
            TileByteCounts = 325,
            InkSet = 332,
            InkNames = 333,
            NumberOfInks = 334,
            DotRange = 336,
            TargetPrinter = 337,
            ExtraSamples = 338,
            SampleFormat = 339,
            SMinSampleValue = 340,
            SMaxSampleValue = 341,
            TransferRange = 342,
            JPEGProc = 512,
            JPEGInterFormat = 513,
            JPEGInterLength = 514,
            JPEGRestartInterval = 515,
            JPEGLosslessPredictors = 517,
            JPEGPointTransforms = 518,
            JPEGQTables = 519,
            JPEGDCTables = 520,
            JPEGACTables = 521,
            YCbCrCoefficients = 529,
            YCbCrSubsampling = 530,
            YCbCrPositioning = 531,
            REFBlackWhite = 532,
            ICCProfile = 34675,
            Gamma = 769,
            ICCProfileDescriptor = 770,
            SRGBRenderingIntent = 771,
            ImageTitle = 800,
            Copyright = 33432,
            ResolutionXUnit = 20481,
            ResolutionYUnit = 20482,
            ResolutionXLengthUnit = 20483,
            ResolutionYLengthUnit = 20484,
            PrintFlags = 20485,
            PrintFlagsVersion = 20486,
            PrintFlagsCrop = 20487,
            PrintFlagsBleedWidth = 20488,
            PrintFlagsBleedWidthScale = 20489,
            HalftoneLPI = 20490,
            HalftoneLPIUnit = 20491,
            HalftoneDegree = 20492,
            HalftoneShape = 20493,
            HalftoneMisc = 20494,
            HalftoneScreen = 20495,
            JPEGQuality = 20496,
            GridSize = 20497,
            ThumbnailFormat = 20498,
            ThumbnailWidth = 20499,
            ThumbnailHeight = 20500,
            ThumbnailColorDepth = 20501,
            ThumbnailPlanes = 20502,
            ThumbnailRawBytes = 20503,
            ThumbnailSize = 20504,
            ThumbnailCompressedSize = 20505,
            ColorTransferFunction = 20506,
            ThumbnailData = 20507,
            ThumbnailImageWidth = 20512,
            ThumbnailImageHeight = 1282,
            ThumbnailBitsPerSample = 20514,
            ThumbnailCompression = 20515,
            ThumbnailPhotometricInterp = 20516,
            ThumbnailImageDescription = 20517,
            ThumbnailEquipMake = 20518,
            ThumbnailEquipModel = 20519,
            ThumbnailStripOffsets = 20520,
            ThumbnailOrientation = 20521,
            ThumbnailSamplesPerPixel = 20522,
            ThumbnailRowsPerStrip = 20523,
            ThumbnailStripBytesCount = 20524,
            ThumbnailResolutionX = 20525,
            ThumbnailResolutionY = 20526,
            ThumbnailPlanarConfig = 20527,
            ThumbnailResolutionUnit = 20528,
            ThumbnailTransferFunction = 20529,
            ThumbnailSoftwareUsed = 20530,
            ThumbnailDateTime = 20531,
            ThumbnailArtist = 20532,
            ThumbnailWhitePoint = 20533,
            ThumbnailPrimaryChromaticities = 20534,
            ThumbnailYCbCrCoefficients = 20535,
            ThumbnailYCbCrSubsampling = 20536,
            ThumbnailYCbCrPositioning = 20537,
            ThumbnailRefBlackWhite = 20538,
            ThumbnailCopyRight = 20539,
            LuminanceTable = 20624,
            ChrominanceTable = 20625,
            FrameDelay = 20736,
            LoopCount = 20737,
            PixelUnit = 20752,
            PixelPerUnitX = 20753,
            PixelPerUnitY = 20754,
            PaletteHistogram = 20755,
            ExifExposureTime = 33434,
            ExifFNumber = 33437,
            ExifExposureProg = 34850,
            ExifSpectralSense = 34852,
            ExifISOSpeed = 34855,
            ExifOECF = 34856,
            ExifVer = 36864,
            ExifDTOrig = 36867,
            ExifDTDigitized = 36868,
            ExifCompConfig = 37121,
            ExifCompBPP = 37122,
            ExifShutterSpeed = 37377,
            ExifAperture = 37378,
            ExifBrightness = 37379,
            ExifExposureBias = 37380,
            ExifMaxAperture = 37381,
            ExifSubjectDist = 37382,
            ExifMeteringMode = 37383,
            ExifLightSource = 37384,
            ExifFlash = 37385,
            ExifFocalLength = 37386,
            ExifMakerNote = 37500,
            ExifUserComment = 37510,
            ExifDTSubsec = 37520,
            ExifDTOrigSS = 37521,
            ExifDTDigSS = 37522,
            ExifFPXVer = 40960,
            ExifColorSpace = 40961,
            ExifPixXDim = 40962,
            ExifPixYDim = 40963,
            ExifRelatedWav = 40964,
            ExifInterop = 40965,
            ExifFlashEnergy = 41483,
            ExifSpatialFR = 41484,
            ExifFocalXRes = 41486,
            ExifFocalYRes = 41487,
            ExifFocalResUnit = 41488,
            ExifSubjectLoc = 41492,
            ExifExposureIndex = 41493,
            ExifSensingMethod = 41495,
            ExifFileSource = 41728,
            ExifSceneType = 41729,
            ExifCfaPattern = 41730,
            GpsVer = 0,
            GpsLatitudeRef = 1,
            GpsLatitude = 2,
            GpsLongitudeRef = 3,
            GpsLongitude = 4,
            GpsAltitudeRef = 5,
            GpsAltitude = 6,
            GpsGpsTime = 7,
            GpsGpsSatellites = 8,
            GpsGpsStatus = 9,
            GpsGpsMeasureMode = 10,
            GpsGpsDop = 11,
            GpsSpeedRef = 12,
            GpsSpeed = 13,
            GpsTrackRef = 14,
            GpsTrack = 15,
            GpsImgDirRef = 16,
            GpsImgDir = 17,
            GpsMapDatum = 18,
            GpsDestLatRef = 19,
            GpsDestLat = 20,
            GpsDestLongRef = 21,
            GpsDestLong = 22,
            GpsDestBearRef = 23,
            GpsDestBear = 24,
            GpsDestDistRef = 25,
            GpsDestDist = 26,
            FileExplorerTitle = 40091,
            FileExplorerAuthor = 40093,
            FileExplorerKeywords = 40094,
            FileExplorerSubject = 40095,
            FileExplorerComments = 40092
            }
        public enum ExifDataTypes : short
            {
            UnsignedByte = 1,
            AsciiString = 2,
            UnsignedShort = 3,
            UnsignedLong = 4,
            UnsignedRational = 5,
            SignedByte = 6,
            Undefined = 7,
            SignedShort = 8,
            SignedLong = 9,
            SignedRational = 10,
            SingleFloat = 11,
            DoubleFloat = 12
            }


        /// <summary> 
        /// Real position of 0th row and column of picture 
        /// </summary> 
        /// <remarks></remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// </history> 

        public enum Orientations
            {
            TopLeft = 1,
            TopRight = 2,
            BottomRight = 3,
            BottomLeft = 4,
            LeftTop = 5,
            RightTop = 6,
            RightBottom = 7,
            LftBottom = 8
            }


        /// <summary> 
        /// Exposure programs 
        /// </summary> 
        /// <remarks></remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// </history> 

        public enum ExposurePrograms
            {
            Manual = 1,
            Normal = 2,
            AperturePriority = 3,
            ShutterPriority = 4,
            Creative = 5,
            Action = 6,
            Portrait = 7,
            Landscape = 8
            }


        /// <summary> 
        /// Exposure metering modes 
        /// </summary> 
        /// <remarks></remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// </history> 

        public enum ExposureMeteringModes
            {
            Unknown = 0,
            Average = 1,
            CenterWeightedAverage = 2,
            Spot = 3,
            MultiSpot = 4,
            MultiSegment = 5,
            Partial = 6,
            Other = 255
            }


        /// <summary> 
        /// Flash activity modes 
        /// </summary> 
        /// <remarks></remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// </history> 

        public enum FlashModes
            {
            NotFired = 0,
            Fired = 1,
            FiredButNoStrobeReturned = 5,
            FiredAndStrobeReturned = 7
            }


        /// <summary> 
        /// Possible light sources (white balance) 
        /// </summary> 
        /// <remarks></remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// </history> 

        public enum LightSources
            {
            Unknown = 0,
            Daylight = 1,
            Fluorescent = 2,
            Tungsten = 3,
            Flash = 10,
            StandardLightA = 17,
            StandardLightB = 18,
            StandardLightC = 19,
            D55 = 20,
            D65 = 21,
            D75 = 22,
            Other = 255
            }

        /// <summary> 
        /// Represents rational which is type of some Exif properties 
        /// </summary> 
        /// <remarks></remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// </history> 

        public struct Rational
            {
            public Int32 Numerator;
            public Int32 Denominator;


            /// <summary> 
            /// Converts rational to string representation 
            /// </summary> 
            /// <returns>String representation of the rational.</returns> 
            /// <remarks></remarks> 
            /// <history> 
            /// [altair] 10.09.2003 Created 
            /// </history> 

            public new string ToString() // ERROR: Unsupported modifier : In, Optional string Delimiter) 
                {
                return Numerator + "/" + Denominator;
                }


            /// <summary> 
            /// Converts rational to double precision real number 
            /// </summary> 
            /// <returns>The rational as double precision real number.</returns> 
            /// <remarks></remarks> 
            /// <history> 
            /// [altair] 10.09.2003 Created 
            /// </history> 

            public double ToDouble()
                {
                return (double)(Numerator) / (double)(Denominator);
                }
            }

        #endregion

        #region " Nicely formatted well-known properties "
        /// <summary> 
        /// Brand of equipment (EXIF EquipMake) 
        /// </summary> 
        /// <value></value> 
        /// <remarks></remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// </history> 
        public string EquipmentMaker
            {
            get { return this.GetPropertyString((int)(TagNames.EquipMake)); }
            }
        /// <summary> 
        /// Model of equipment (EXIF EquipModel) 
        /// </summary> 
        /// <value></value> 
        /// <remarks></remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// </history> 
        public string EquipmentModel
            {
            get { return this.GetPropertyString((int)(TagNames.EquipModel)); }
            set
                {
                try
                    {
                    this.SetPropertyString((int)(TagNames.EquipModel), value);
                    }
                catch (Exception)
                    {

                    throw;
                    }
                }
            }
        /// <summary> 
        /// Software used for processing (EXIF Software) 
        /// </summary> 
        /// <value></value> 
        /// <remarks></remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// </history> 
        public string Software
            {
            get { return this.GetPropertyString((int)(TagNames.SoftwareUsed)); }
            }
        /// <summary> 
        /// Orientation of image (position of row 0, column 0) (EXIF Orientation) 
        /// </summary> 
        /// <value></value> 
        /// <remarks></remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// </history> 
        public Orientations Orientation
            {
            get
                {
                Int32 X = this.GetPropertyInt16((int)(TagNames.Orientation));

                if (!Enum.IsDefined(typeof(Orientations), X))
                    {
                    return Orientations.TopLeft;
                    }
                else
                    {
                    return (Orientations)Enum.Parse(typeof(Orientations), Enum.GetName(typeof(Orientations), X));
                    }
                }
            }

        /// <summary> 
        /// Time when image was last modified (EXIF DateTime). 
        /// </summary> 
        /// <value></value> 
        /// <remarks></remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// </history> 
        public DateTime DateTimeLastModified
            {


            get
                {
                try
                    {
                    return DateTime.ParseExact(this.GetPropertyString((int)(TagNames.DateTime)), "yyyy\\:MM\\:dd HH\\:mm\\:ss", null);
                    }
                catch
                    {
                    return DateTime.MinValue;
                    }
                }
            set
                {
                try
                    {
                    this.SetPropertyString((int)(TagNames.DateTime), value.ToString("yyyy\\:MM\\:dd HH\\:mm\\:ss"));
                    }
                catch
                    {
                    }
                }
            }
        /// <summary> 
        /// Time when image was digitized (EXIF DateTimeDigitized). 
        /// </summary> 
        /// <value></value> 
        /// <remarks></remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// </history> 
        public DateTime DateTimeDigitized
            {


            get
                {
                try
                    {
                    return DateTime.ParseExact(this.GetPropertyString((int)(TagNames.ExifDTDigitized)), "yyyy\\:MM\\:dd HH\\:mm\\:ss", null);
                    }
                catch
                    {
                    return DateTime.MinValue;
                    }
                }
            set
                {
                try
                    {
                    this.SetPropertyString((int)(TagNames.ExifDTDigitized), value.ToString("yyyy\\:MM\\:dd HH\\:mm\\:ss"));
                    }
                catch
                    {
                    }
                }
            }
        /// <summary> 
        /// Image width 
        /// </summary> 
        /// <value></value> 
        /// <remarks></remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// [altair] 04.09.2005 Changed output to Int32, load from image instead of EXIF 
        /// </history> 
        public Int32 Width
            {
            get { return this._Image.Width; }
            }
        /// <summary> 
        /// Image height 
        /// </summary> 
        /// <value></value> 
        /// <remarks></remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// [altair] 04.09.2005 Changed output to Int32, load from image instead of EXIF 
        /// </history> 
        public Int32 Height
            {


            get { return this._Image.Height; }
            }
        /// <summary> 
        /// X resolution in dpi (EXIF XResolution/ResolutionUnit) 
        /// </summary> 
        /// <value></value> 
        /// <remarks></remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// </history> 
        public double ResolutionX
            {
            get
                {
                double R = this.GetPropertyRational((int)(TagNames.XResolution)).ToDouble();

                if (this.GetPropertyInt16((int)(TagNames.ResolutionUnit)) == 3)
                    {
                    //-- resolution is in points/cm 
                    return R * 2.54;
                    }
                else
                    {
                    //-- resolution is in points/inch 
                    return R;
                    }
                }
            }
        /// <summary> 
        /// Y resolution in dpi (EXIF YResolution/ResolutionUnit) 
        /// </summary> 
        /// <value></value> 
        /// <remarks></remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// </history> 
        public double ResolutionY
            {


            get
                {
                double R = this.GetPropertyRational((int)(TagNames.YResolution)).ToDouble();

                if (this.GetPropertyInt16((int)(TagNames.ResolutionUnit)) == 3)
                    {
                    //-- resolution is in points/cm 
                    return R * 2.54;
                    }
                else
                    {
                    //-- resolution is in points/inch 
                    return R;
                    }
                }
            }
        /// <summary> 
        /// Image title (EXIF ImageTitle) 
        /// </summary> 
        /// <value></value> 
        /// <remarks></remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// </history> 
        public string Title
            {
            get { return this.GetPropertyString((int)(TagNames.ImageTitle)); }
            set
                {
                try
                    {
                    this.SetPropertyString((int)(TagNames.ImageTitle), value);
                    }
                catch
                    {
                    }
                }
            }

        public string FileExplorerTitle
            {
            get { return this.GetPropertyString((int)(TagNames.FileExplorerTitle)); }
            set
                {
                try
                    {
                    this.SetPropertyString((int)(TagNames.FileExplorerTitle), value);
                    }
                catch
                    {
                    }
                }
            }

        public string FileExplorerAuthor
            {
            get { return this.GetPropertyString((int)(TagNames.FileExplorerAuthor)); }
            set
                {
                try
                    {
                    this.SetPropertyString((int)(TagNames.FileExplorerAuthor), value);
                    }
                catch
                    {
                    }
                }
            }

        public string FileExplorerComments
            {
            get { return this.GetPropertyString((int)(TagNames.FileExplorerComments)); }
            set
                {
                try
                    {
                    this.SetPropertyString((int)(TagNames.FileExplorerComments), value);
                    }
                catch
                    {
                    }
                }
            }

        /// <summary> 
        /// User comment (EXIF UserComment) 
        /// </summary> 
        /// <value></value> 
        /// <remarks></remarks> 
        /// <history> 
        /// [altair] 13.06.2004 Created 
        /// </history>
        public string UserComment
            {
            get { return this.GetPropertyString((int)(TagNames.ExifUserComment)); }
            set
                {
                try
                    {
                    this.SetPropertyString((int)(TagNames.ExifUserComment), value);
                    }
                catch
                    {
                    }
                }
            }
        /// <summary> 
        /// Artist name (EXIF Artist) 
        /// </summary> 
        /// <value></value> 
        /// <remarks></remarks> 
        /// <history> 
        /// [altair] 13.06.2004 Created 
        /// </history> 
        public string Artist
            {
            get { return this.GetPropertyString((int)(TagNames.Artist)); }
            set
                {
                try
                    {
                    this.SetPropertyString((int)(TagNames.Artist), value);
                    }
                catch
                    {
                    }
                }
            }
        /// <summary> 
        /// Image description (EXIF ImageDescription) 
        /// </summary> 
        /// <value></value> 
        /// <remarks></remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// </history> 
        public string Description
            {
            get { return this.GetPropertyString((int)(TagNames.ImageDescription)); }
            set
                {
                try
                    {
                    this.SetPropertyString((int)(TagNames.ImageDescription), value);
                    }
                catch
                    {
                    }
                }
            }

        /// <summary> 
        /// Image copyright (EXIF Copyright) 
        /// </summary> 
        /// <value></value> 
        /// <remarks></remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// </history> 
        public string Copyright
            {


            get { return this.GetPropertyString((int)(TagNames.Copyright)); }
            set
                {
                try
                    {
                    this.SetPropertyString((int)(TagNames.Copyright), value.ToString());
                    }
                catch
                    {
                    }
                }
            }
        /// <summary> 
        /// Exposure time in seconds (EXIF ExifExposureTime/ExifShutterSpeed) 
        /// </summary> 
        /// <value></value> 
        /// <remarks></remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// </history> 
        public double ExposureTime
            {
            get
                {
                if (this.IsPropertyDefined((int)(TagNames.ExifExposureTime)))
                    {
                    //-- Exposure time is explicitly specified 
                    return this.GetPropertyRational((int)(TagNames.ExifExposureTime)).ToDouble();
                    }
                else if (this.IsPropertyDefined((int)(TagNames.ExifShutterSpeed)))
                    {
                    //-- Compute exposure time from shutter speed 
                    return 1 / (Math.Pow(2, this.GetPropertyRational((int)(TagNames.ExifShutterSpeed)).ToDouble()));
                    }
                else
                    {
                    //-- Can't figure out 
                    return 0;
                    }
                }
            }

        /// <summary> 
        /// Aperture value as F number (EXIF ExifFNumber/ExifApertureValue) 
        /// </summary> 
        /// <value></value> 
        /// <remarks></remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// </history> 
        public double Aperture
            {


            get
                {
                if (this.IsPropertyDefined((int)(TagNames.ExifFNumber)))
                    {
                    return this.GetPropertyRational((int)(TagNames.ExifFNumber)).ToDouble();
                    }
                else if (this.IsPropertyDefined((int)(TagNames.ExifAperture)))
                    {
                    return Math.Pow(System.Math.Sqrt(2), this.GetPropertyRational((int)(TagNames.ExifAperture)).ToDouble());
                    }
                else
                    {
                    return 0;
                    }
                }
            }

        /// <summary> 
        /// Exposure program used (EXIF ExifExposureProg) 
        /// </summary> 
        /// <value></value> 
        /// <remarks>If not specified, returns Normal (2)</remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// </history> 
        public ExposurePrograms ExposureProgram
            {

            get
                {
                Int32 X = this.GetPropertyInt16((int)(TagNames.ExifExposureProg));

                if (Enum.IsDefined(typeof(ExposurePrograms), X))
                    {
                    return (ExposurePrograms)Enum.Parse(typeof(ExposurePrograms), Enum.GetName(typeof(ExposurePrograms), X));
                    }
                else
                    {
                    return ExposurePrograms.Normal;
                    }
                }
            }
        /// <summary> 
        /// ISO sensitivity 
        /// </summary> 
        /// <value></value> 
        /// <remarks></remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// </history> 
        public Int16 ISO
            {


            get { return this.GetPropertyInt16((int)(TagNames.ExifISOSpeed)); }
            }
        /// <summary> 
        /// Subject distance in meters (EXIF SubjectDistance) 
        /// </summary> 
        /// <value></value> 
        /// <remarks></remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// </history> 
        public double SubjectDistance
            {


            get { return this.GetPropertyRational((int)(TagNames.ExifSubjectDist)).ToDouble(); }
            }
        /// <summary> 
        /// Exposure method metering mode used (EXIF MeteringMode) 
        /// </summary> 
        /// <value></value> 
        /// <remarks>If not specified, returns Unknown (0)</remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// </history> 
        public ExposureMeteringModes ExposureMeteringMode
            {


            get
                {
                Int32 X = this.GetPropertyInt16((int)(TagNames.ExifMeteringMode));

                if (Enum.IsDefined(typeof(ExposureMeteringModes), X))
                    {
                    return (ExposureMeteringModes)Enum.Parse(typeof(ExposureMeteringModes), Enum.GetName(typeof(ExposureMeteringModes), X));
                    }
                else
                    {
                    return ExposureMeteringModes.Unknown;
                    }
                }
            }

        /// <summary> 
        /// Focal length of lenses in mm (EXIF FocalLength) 
        /// </summary> 
        /// <value></value> 
        /// <remarks></remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// </history> 
        public double FocalLength
            {
            get { return this.GetPropertyRational((int)(TagNames.ExifFocalLength)).ToDouble(); }
            }
        /// <summary> 
        /// Flash mode (EXIF Flash) 
        /// </summary> 
        /// <value></value> 
        /// <remarks>If not present, value NotFired (0) is returned</remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// </history> 
        public FlashModes FlashMode
            {


            get
                {
                Int32 X = this.GetPropertyInt16((int)(TagNames.ExifFlash));

                if (Enum.IsDefined(typeof(FlashModes), X))
                    {
                    return (FlashModes)Enum.Parse(typeof(FlashModes), Enum.GetName(typeof(FlashModes), X));
                    }
                else
                    {
                    return FlashModes.NotFired;
                    }
                }
            }
        /// <summary> 
        /// Light source / white balance (EXIF LightSource) 
        /// </summary> 
        /// <value></value> 
        /// <remarks>If not specified, returns Unknown (0).</remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// </history> 
        public LightSources LightSource
            {


            get
                {
                Int32 X = this.GetPropertyInt16((int)(TagNames.ExifLightSource));

                if (Enum.IsDefined(typeof(LightSources), X))
                    {
                    return (LightSources)Enum.Parse(typeof(LightSources), Enum.GetName(typeof(LightSources), X));
                    }
                else
                    {
                    return LightSources.Unknown;
                    }
                }
            }


        public DateTime DateTimeOriginal
            {
            get
                {
                try
                    {
                    return DateTime.ParseExact(this.GetPropertyString((int)TagNames.ExifDTOrig), "yyyy\\:MM\\:dd HH\\:mm\\:ss", null);
                    }
                catch
                    {
                    return DateTime.MinValue;
                    }
                }
            set
                {
                try
                    {
                    this.SetPropertyString((int)TagNames.ExifDTOrig, value.ToString("yyyy\\:MM\\:dd HH\\:mm\\:ss"));
                    }
                catch
                    {
                    }
                }
            }

        public String GPSLatitudeReference
        {
            get
            {
                return this.GetPropertyString((int)(TagNames.GpsLatitudeRef));
            }
        }
        public String GPSLongitudeReference
        {
            get
            {
                return this.GetPropertyString((int)(TagNames.GpsLongitudeRef));
            }
        }
        public double GPSLatitude
        {
            get
            {
                double result = 0;
                try
                {
                    byte[] val = this.GetProperty((int)(TagNames.GpsLatitude), new byte[0]);
                    byte[] coordRefByte = this.GetProperty((int)(TagNames.GpsLatitudeRef), new byte[0]);
                    if (val.Length != 0)
                    {
                        //result = Functions.GetGPSDoubleValue(val);
                        if (coordRefByte.Length > 0 && coordRefByte[0] == Convert.ToByte('S') &&
                            //Compatibility with pre svn 35
                            result > 0)
                        {
                            result *= -1;
                        }
                    }
                }
                catch (Exception)
                {
                }
                return result;
            }
            set
            {
                //Set Latitude
                byte[] val = new byte[24];
                //val = Functions.GetGPSByteValue(Math.Abs(value));
                SetProperty((int)(TagNames.GpsLatitude), val, ExifDataTypes.SignedRational);

                //Set coordinate reference (North - South)
                byte[] coordRefByte = new byte[2];
                if (value >= 0)
                    coordRefByte[0] = Convert.ToByte('N');
                else
                    coordRefByte[0] = Convert.ToByte('S');
                SetProperty((int)TagNames.GpsLatitudeRef, coordRefByte, ExifDataTypes.AsciiString);
            }
        }

        public double GPSLongitude
        {
            get
            {
                double result = 0;
                try
                {
                    byte[] val = this.GetProperty((int)(TagNames.GpsLongitude), new byte[0]);
                    byte[] coordRefByte = this.GetProperty((int)(TagNames.GpsLongitudeRef), new byte[0]);
                    if (val.Length != 0)
                    {
                        //result = Functions.GetGPSDoubleValue(val);
                        if (coordRefByte.Length > 0 && coordRefByte[0] == Convert.ToByte('W') &&
                            //Compatibility with pre svn 35
                            result > 0)
                        {
                            result *= -1;
                        }
                    }
                }
                catch (Exception)
                {
                }
                return result;
            }
            set
            {
                //Set Longitude
                byte[] val = new byte[24];
                //val = Functions.GetGPSByteValue(Math.Abs(value));
                SetProperty((int)(TagNames.GpsLongitude), val, ExifDataTypes.SignedRational);

                //Set coordinate reference (East - West)
                byte[] coordRefByte = new byte[2];
                if (value >= 0)
                    coordRefByte[0] = Convert.ToByte('E');
                else
                    coordRefByte[0] = Convert.ToByte('W');
                SetProperty((int)TagNames.GpsLongitudeRef, coordRefByte, ExifDataTypes.AsciiString);
            }
        }

        public double GPSAltitude
            {
            get
                {
                try
                    {
                    double d = this.GetPropertyRational((int)(TagNames.GpsAltitude)).ToDouble();
                    return d;
                    }
                catch (Exception)
                    {
                    return 0;
                    //throw;
                    }
                }
            set
                {
                byte[] val = new byte[8];
                BitConverter.GetBytes(Convert.ToInt32(value * 100)).CopyTo(val, 0);
                BitConverter.GetBytes(Convert.ToInt32(100)).CopyTo(val, 4);
                SetProperty((int)(TagNames.GpsAltitude), val, ExifDataTypes.SignedRational);

                //0=above sea level, 1=below sea level (not supported)
                byte[] b = new byte[1];
                b[0] = Convert.ToByte(0);
                SetProperty((int)TagNames.GpsAltitudeRef, b, ExifDataTypes.UnsignedByte);
                }
            }
        #endregion

        #region " Support methods for working with EXIF properties "
        public string GetPropertyString(Int32 PID)
            {
            if (this.IsPropertyDefined(PID))
                {
                return GetString(this._Image.GetPropertyItem(PID).Value);
                }
            else
                {
                return null;
                }
            }

        /// <summary> 
        /// Checks if current image has specified certain property 
        /// </summary> 
        /// <param name="PID">Property ID</param> 
        /// <returns>True if image has specified property, False otherwise.</returns> 
        /// <remarks></remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// </history> 
        public bool IsPropertyDefined(Int32 PID)
            {
            return (bool)(Array.IndexOf(this._Image.PropertyIdList, PID) > -1);
            }

        /// <summary> 
        /// Gets specified Int32 property 
        /// </summary> 
        /// <param name="PID">Property ID</param> 
        /// <param name="DefaultValue">Optional, default 0. Default value returned if property is not present.</param> 
        /// <remarks>Value of property or DefaultValue if property is not present.</remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// </history> 
        public Int32 GetPropertyInt32(Int32 PID, Int32 DefaultValue)
            {
            if (this.IsPropertyDefined(PID))
                {
                return GetInt32(this._Image.GetPropertyItem(PID).Value);
                }
            else
                {
                return DefaultValue;
                }
            }

        /// <summary> 
        /// Gets specified Int16 property 
        /// </summary> 
        /// <param name="PID">Property ID</param> 
        /// <remarks>Value of property or DefaultValue if property is not present.</remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// </history> 
        public Int16 GetPropertyInt16(Int32 PID)
            {
            if (this.IsPropertyDefined(PID))
                {
                return GetInt16(this._Image.GetPropertyItem(PID).Value);
                }
            else
                {
                return 0;// DefaultValue; 
                }
            }

        /// <summary> 
        /// Gets specified string property 
        /// </summary> 
        /// <param name="PID">Property ID</param> 
        /// <param name="DefaultValue">Optional, default String.Empty. Default value returned if property is not present.</param> 
        /// <returns></returns> 
        /// <remarks>Value of property or DefaultValue if property is not present.</remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// </history> 
        public string GetPropertyString(Int32 PID, string DefaultValue)
            {
            if (this.IsPropertyDefined(PID))
                {
                return GetString(this._Image.GetPropertyItem(PID).Value);
                }
            else
                {
                return DefaultValue;
                }
            }

        /// <summary> 
        /// Gets specified property in raw form 
        /// </summary> 
        /// <param name="PID">Property ID</param> 
        /// <param name="DefaultValue">Optional, default Nothing. Default value returned if property is not present.</param> 
        /// <returns></returns> 
        /// <remarks>Is recommended to use typed methods (like <see cref="GetPropertyString(int,String)" /> etc.) instead, when possible.</remarks> 
        /// <history> 
        /// [altair] 05.09.2005 Created 
        /// </history> 
        public byte[] GetProperty(Int32 PID, byte[] DefaultValue)
            {
            if (this.IsPropertyDefined(PID))
                {
                return this._Image.GetPropertyItem(PID).Value;
                }
            else
                {
                return DefaultValue;
                }
            }

        /// <summary> 
        /// Gets specified rational property 
        /// </summary> 
        /// <param name="PID">Property ID</param> 
        /// <returns></returns> 
        /// <remarks>Value of property or 0/1 if not present.</remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// </history> 
        public Rational GetPropertyRational(Int32 PID)
            {
            if (this.IsPropertyDefined(PID))
                {
                return GetRational(this._Image.GetPropertyItem(PID).Value);
                }
            else
                {
                Rational R;
                R.Numerator = 0;
                R.Denominator = 1;
                return R;
                }
            }

        /// <summary> 
        /// Sets specified string property 
        /// </summary> 
        /// <param name="PID">Property ID</param> 
        /// <param name="Value">Value to be set</param> 
        /// <remarks></remarks> 
        /// <history> 
        /// [altair] 12.6.2004 Created 
        /// </history> 
        public void SetPropertyString(Int32 PID, string Value)
            {
            byte[] Data = this._Encoding.GetBytes(Value + /*Microsoft.VisualBasic.Constants.*/vbNullChar);
            SetProperty(PID, Data, ExifDataTypes.AsciiString);
            }

        public void SetPropertyGPSLoc(Int32 PID, Double Value)
            {
            double d1 = Math.Floor(Value);
            double d2 = Math.Round((Value - Math.Floor(Value)) * 10000 * 0.6);
            byte[] val = new byte[24];
            BitConverter.GetBytes(Convert.ToInt32(d1)).CopyTo(val, 0);
            BitConverter.GetBytes(Convert.ToInt32(1)).CopyTo(val, 4);
            BitConverter.GetBytes(Convert.ToInt32(d2)).CopyTo(val, 8);
            BitConverter.GetBytes(Convert.ToInt32(100)).CopyTo(val, 12);
            BitConverter.GetBytes(Convert.ToInt32(0)).CopyTo(val, 16);
            BitConverter.GetBytes(Convert.ToInt32(1)).CopyTo(val, 20);
            SetProperty(PID, val, ExifDataTypes.SignedRational);


            char coordRef;
            if (PID == (int)TagNames.GpsLongitude)
                {
                if (Value >= 0)
                    coordRef = 'E';
                else
                    coordRef = 'W';

                byte[] coordRefByte = new byte[2];
                coordRefByte[0] = Convert.ToByte(coordRef);
                SetProperty((int)TagNames.GpsLongitudeRef, coordRefByte, ExifDataTypes.AsciiString);
                }
            else
                {
                if (PID == (int)TagNames.GpsLatitude)
                    {
                    if (Value >= 0)
                        coordRef = 'N';
                    else
                        coordRef = 'S';
                    byte[] coordRefByte = new byte[2];
                    coordRefByte[0] = Convert.ToByte(coordRef);
                    SetProperty((int)TagNames.GpsLatitudeRef, coordRefByte, ExifDataTypes.AsciiString);
                    }
                }

            }

        /// <summary> 
        /// Sets specified Int16 property 
        /// </summary> 
        /// <param name="PID">Property ID</param> 
        /// <param name="Value">Value to be set</param> 
        /// <remarks></remarks> 
        /// <history> 
        /// [altair] 12.6.2004 Created 
        /// </history> 
        public void SetPropertyInt16(Int32 PID, Int16 Value)
            {
            byte[] Data = new byte[2];
            Data[0] = (byte)(Value & 255);
            Data[1] = (byte)((Value & 65280) >> 8);
            SetProperty(PID, Data, ExifDataTypes.SignedShort);
            }

        /// <summary> 
        /// Sets specified Int32 property 
        /// </summary> 
        /// <param name="PID">Property ID</param> 
        /// <param name="Value">Value to be set</param> 
        /// <remarks></remarks> 
        /// <history> 
        /// [altair] 13.06.2004 Created 
        /// </history> 
        public void SetPropertyInt32(Int32 PID, Int32 Value)
            {
            byte[] Data = new byte[4];
            for (Int32 I = 0; I <= 3; I++)
                {
                Data[I] = (byte)(Value & 255);
                Value >>= 8;
                }
            SetProperty(PID, Data, ExifDataTypes.SignedLong);
            }

        /// <summary> 
        /// Sets specified propery in raw form 
        /// </summary> 
        /// <param name="PID">Property ID</param> 
        /// <param name="Data">Raw data</param> 
        /// <param name="Type">EXIF data type</param> 
        /// <remarks>Is recommended to use typed methods (like <see cref="SetPropertyString" /> etc.) instead, when possible.</remarks> 
        /// <history> 
        /// [altair] 12.6.2004 Created 
        /// </history> 
        public void SetProperty(Int32 PID, byte[] Data, ExifDataTypes Type)
        {
            System.Drawing.Imaging.PropertyItem P = PropertyItem2.GetPropertyItem();
            //System.Drawing.Imaging.PropertyItem P = this._Image.PropertyItems[0];
            P.Id = PID;
            P.Value = Data;
            P.Type = (short)(Type);
            P.Len = Data.Length;
            //try
            //{
                //Mono TODO: NotImplemented
                this._Image.SetPropertyItem(P);
            //}
            //catch(Exception e) {
            //    System.Console.WriteLine(e.ToString());
            //}
        }

        /// <summary> 
        /// Reads Int32 from EXIF bytearray. 
        /// </summary> 
        /// <param name="B">EXIF bytearray to process</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// [altair] 05.09.2005 Changed from public shared to private instance method 
        /// </history> 
        private Int32 GetInt32(byte[] B)
            {
            if (B.Length < 4)
                throw new ArgumentException("Data too short (4 bytes expected)", "B");
            return B[3] << 24 | B[2] << 16 | B[1] << 8 | B[0];
            }

        /// <summary> 
        /// Reads Int16 from EXIF bytearray. 
        /// </summary> 
        /// <param name="B">EXIF bytearray to process</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// [altair] 05.09.2005 Changed from public shared to private instance method 
        /// </history> 
        private Int16 GetInt16(byte[] B)
            {
            if (B.Length < 2)
                throw new ArgumentException("Data too short (2 bytes expected)", "B");
            return (short)(B[1] << 8 | B[0]);
            }

        /// <summary> 
        /// Reads string from EXIF bytearray. 
        /// </summary> 
        /// <param name="B">EXIF bytearray to process</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// [altair] 05.09.2005 Changed from public shared to private instance method 
        /// </history> 
        private string GetString(byte[] B)
            {
            string R = this._Encoding.GetString(B);
            if (R.EndsWith(/*Constants.*/vbNullChar))
                R = R.Substring(0, R.Length - 1);
            return R;
            }

        /// <summary> 
        /// Reads rational from EXIF bytearray. 
        /// </summary> 
        /// <param name="B">EXIF bytearray to process</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        /// <history> 
        /// [altair] 10.09.2003 Created 
        /// [altair] 05.09.2005 Changed from public shared to private instance method 
        /// </history> 
        private Rational GetRational(byte[] B)
            {
            Rational R = new Rational();
            byte[] N = new byte[4];
            byte[] D = new byte[4];
            Array.Copy(B, 0, N, 0, 4);
            Array.Copy(B, 4, D, 0, 4);
            R.Denominator = this.GetInt32(D);
            R.Numerator = this.GetInt32(N);
            return R;
            }

        #endregion

        #region " IDisposable implementation "
        public void Dispose()
            {
            if (this._Image != null)
                {
                this._Image.Dispose();
                }
            }
        #endregion

        }
    }
