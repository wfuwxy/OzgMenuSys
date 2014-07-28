package com.ozg.menusys;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.UnsupportedEncodingException;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Bitmap.CompressFormat;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.graphics.Matrix;
import android.graphics.PixelFormat;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import android.util.Base64;
import android.util.Log;
import android.widget.Toast;

public class Commons {

	public static Drawable zoomDrawable(Drawable drawable, int w, int h) {  
        int width = drawable.getIntrinsicWidth();  
        int height = drawable.getIntrinsicHeight();  
        Bitmap oldbmp = drawableToBitmap(drawable);  
        Matrix matrix = new Matrix();  
        float scaleWidth = ((float) w / width);  
        float scaleHeight = ((float) h / height);  
        matrix.postScale(scaleWidth, scaleHeight);  
        Bitmap newbmp = Bitmap.createBitmap(oldbmp, 0, 0, width, height, matrix, true);  
        return new BitmapDrawable(null, newbmp);  
    }  
      
    public static Bitmap drawableToBitmap(Drawable drawable) {  
        int width = drawable.getIntrinsicWidth();  
        int height = drawable.getIntrinsicHeight();  
        Bitmap.Config config = drawable.getOpacity() != PixelFormat.OPAQUE ? Bitmap.Config.ARGB_8888 : Bitmap.Config.RGB_565;  
        Bitmap bitmap = Bitmap.createBitmap(width, height, config);  
        Canvas canvas = new Canvas(bitmap);  
        drawable.setBounds(0, 0, width, height);  
        drawable.draw(canvas);  
        return bitmap;  
    }  
	
    public static String bitmapToString(Bitmap bitmap, CompressFormat bmpFormat) {
        
        // 将Bitmap转换成字符串    
        String string = null;
        ByteArrayOutputStream bStream = new ByteArrayOutputStream();  
        bitmap.compress(bmpFormat, 100, bStream);  

        byte[] bytes = bStream.toByteArray();
  
        string = Base64.encodeToString(bytes, Base64.DEFAULT);
        return string;
  
    }
    
    public static Bitmap stringToBitmap(String string) {
    	
        // 将字符串转换成Bitmap类型
        Bitmap bitmap = null;
        try {
        	byte[] bitmapArray;  
        	bitmapArray = Base64.decode(string, Base64.DEFAULT);  
        	bitmap = android.graphics.BitmapFactory.decodeByteArray(bitmapArray, 0, bitmapArray.length);  

        } catch (Exception e) {  
        	e.printStackTrace();  

        }
        return bitmap;
    }
    
    //本项目自有的静态方法
    
	public static void alertErrMsg(Context context, String msg) {
		
		Toast toast = Toast.makeText(context, msg, Toast.LENGTH_SHORT);
		toast.show();
	}
	
	public static int getSamllImgSize(int val) {
		return (int)((float)val * 8.0f);
	}
	
	public static void imgCacheWrite(Context context, String cacheImgFileName, String imgBase64Str) {
		File cacheImgFile = new File(context.getFilesDir() + "/" + cacheImgFileName);
    	if(cacheImgFile.exists()) {
    		cacheImgFile.delete();
    	}
    	
    	FileOutputStream fos;
		try {
			fos = context.openFileOutput(cacheImgFileName, Context.MODE_PRIVATE);
			fos.write(imgBase64Str.getBytes("utf-8"));
	    	fos.flush();
	    	fos.close();
	    	
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
    	
	}
	
	public static String imgCacheRead(Context context, String cacheImgFileName) {
	    int len = 1024;
	    byte[] buffer = new byte[len];
	    try {
	        FileInputStream fis = context.openFileInput(cacheImgFileName);
	        ByteArrayOutputStream baos = new ByteArrayOutputStream();
	        int nrb = fis.read(buffer, 0, len); // read up to len bytes
	        while (nrb != -1) {
	            baos.write(buffer, 0, nrb);
	            nrb = fis.read(buffer, 0, len);
	        }
	        buffer = baos.toByteArray();
	        fis.close();
	    } catch (FileNotFoundException e) {
	        e.printStackTrace();
	    } catch (IOException e) {
	        e.printStackTrace();
	    }
	    
	    try {
			return new String(buffer, "utf-8");
		} catch (UnsupportedEncodingException e) {
			
			return null;
		}
	}
		
}
