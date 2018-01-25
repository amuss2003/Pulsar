using System;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace GdiPlusLib
{


public class Gdip
	{
	private static ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

	private static bool GetCodecClsid( string filename, out Guid clsid )
		{
		clsid = Guid.Empty;
		string ext = Path.GetExtension( filename );
		if( ext == null )
			return false;
		ext = "*" + ext.ToUpper();
		foreach( ImageCodecInfo codec in codecs )
			{
			if( codec.FilenameExtension.IndexOf( ext ) >= 0 )
				{
				clsid = codec.Clsid;
				return true;
				}
			}
		return false;
		}

    public static bool AutoSaveDIBAs(string PathName,string picname, IntPtr bminfo, IntPtr pixdat)
    {
        //SaveFileDialog sd = new SaveFileDialog();

        //sd.FileName = picname;
        //sd.Title = "Save bitmap as...";
        //sd.Filter = "Bitmap file (*.bmp)|*.bmp|TIFF file (*.tif)|*.tif|JPEG file (*.jpg)|*.jpg|PNG file (*.png)|*.png|GIF file (*.gif)|*.gif|All files (*.*)|*.*";
        ////sd.Filter = "JPEG file (*.jpg)|*.jpg";
        //sd.FilterIndex = 1;
        //if (sd.ShowDialog() != DialogResult.OK)
        //    return false;
        string FileName = "";

        //Need to file name hashing => picname
        //snif + first letter to create folder hash
        //e.g:
        //78822-1 file name
        //pick the first letter 7
        //pick the last letter snif 1
        //create or check for existing folder to save to
        //S:\Archive\Contract\1\7\

        //////////////////////////////////////
        //  File Hashing Working Algorithm  //
        //////////////////////////////////////

        if (picname.IndexOf("-") > -1)
        {
            String FirstLetter = picname.Substring(0, 1);
            String LastLetter = picname.Substring(picname.Length - 1, 1);

            if (!Directory.Exists(PathName + LastLetter))
            {
                Directory.CreateDirectory(PathName + LastLetter);
            }

            if (!Directory.Exists(PathName + LastLetter + @"\" + FirstLetter))
            {
                Directory.CreateDirectory(PathName + LastLetter + @"\" + FirstLetter);
            }

            FileName = PathName + LastLetter + @"\" + FirstLetter + @"\" + picname + ".jpg";
        }

        FileName = PathName + picname + ".jpg";

        if (File.Exists(FileName))
        {
            if (MessageBox.Show("äàí áøöåðê ìëúåá òì äקובץ ä÷ééí?", "ùîéøú קובץ", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return false;
            }
            else
            {
                try
                {
                    File.Delete(FileName);
                }
                catch (Exception)
                {
                    
                }                
            }
        }

        Guid clsid;
        if (!GetCodecClsid(FileName, out clsid))
        {
            MessageBox.Show("Unknown picture format for extension " + Path.GetExtension(FileName),
                            "Image Codec", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }

        IntPtr img = IntPtr.Zero;
        int st = GdipCreateBitmapFromGdiDib(bminfo, pixdat, ref img);
        if ((st != 0) || (img == IntPtr.Zero))
            return false;

        st = GdipSaveImageToFile(img, FileName, ref clsid, IntPtr.Zero);
        GdipDisposeImage(img);
        return st == 0;
    }


	public static bool SaveDIBAs( string picname, IntPtr bminfo, IntPtr pixdat )
	{
		SaveFileDialog sd = new SaveFileDialog();

		sd.FileName = picname;
		sd.Title = "Save bitmap as...";
		sd.Filter = "Bitmap file (*.bmp)|*.bmp|TIFF file (*.tif)|*.tif|JPEG file (*.jpg)|*.jpg|PNG file (*.png)|*.png|GIF file (*.gif)|*.gif|All files (*.*)|*.*";
        //sd.Filter = "JPEG file (*.jpg)|*.jpg";
        sd.FilterIndex = 1;
		if( sd.ShowDialog() != DialogResult.OK )
			return false;

		Guid clsid;
		if( ! GetCodecClsid( sd.FileName, out clsid ) )
		{
			MessageBox.Show( "Unknown picture format for extension " + Path.GetExtension( sd.FileName ),
							"Image Codec", MessageBoxButtons.OK, MessageBoxIcon.Information );
			return false;
		}
		
		IntPtr img = IntPtr.Zero;
		int st = GdipCreateBitmapFromGdiDib( bminfo, pixdat, ref img );
		if( (st != 0) || (img == IntPtr.Zero) )
			return false;

		st = GdipSaveImageToFile( img, sd.FileName, ref clsid, IntPtr.Zero );
		GdipDisposeImage( img );
		return st == 0;
	}


    public static bool SaveDIBAsTemp(String pFileName,IntPtr bminfo, IntPtr pixdat)
    {
        if (!Directory.Exists(@"c:\temp"))
        {
            Directory.CreateDirectory(@"c:\temp");
        }

        String path = @"c:\temp\";
        String FileName = "temp_ocr_" + pFileName + ".jpg";
        Guid clsid;
        if (!GetCodecClsid(FileName, out clsid))
        {
            MessageBox.Show("Unknown picture format for extension " + Path.GetExtension(FileName),
                            "Image Codec", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }

        IntPtr img = IntPtr.Zero;
        int st = GdipCreateBitmapFromGdiDib(bminfo, pixdat, ref img);
        if ((st != 0) || (img == IntPtr.Zero))
            return false;

        st = GdipSaveImageToFile(img, path + FileName, ref clsid, IntPtr.Zero);
        GdipDisposeImage(img);
        return st == 0;
    }


	[DllImport("gdiplus.dll", ExactSpelling=true)]
	internal static extern int GdipCreateBitmapFromGdiDib( IntPtr bminfo, IntPtr pixdat, ref IntPtr image );

    [DllImport("gdiplus.dll", ExactSpelling=true, CharSet=CharSet.Unicode)]
	internal static extern int GdipSaveImageToFile( IntPtr image, string filename, [In] ref Guid clsid, IntPtr encparams );

    [DllImport("gdiplus.dll", ExactSpelling=true)]
	internal static extern int GdipDisposeImage( IntPtr image );

	}
	
} // namespace GdiPlusLib
