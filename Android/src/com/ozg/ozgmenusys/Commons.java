package com.ozg.ozgmenusys;

import java.io.ByteArrayOutputStream;

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
		
}
