package com.ozg.menusys;

import org.json.JSONException;
import org.json.JSONObject;

import de.tavendo.autobahn.WebSocketConnection;
import de.tavendo.autobahn.WebSocketException;
import de.tavendo.autobahn.WebSocketOptions;
import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.content.res.Configuration;
import android.os.Bundle;
import android.util.DisplayMetrics;
import android.util.Log;
import android.view.Display;
import android.view.Menu;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.TextView;

public class MainActivity extends BaseActivity { 
    	
	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		// TODO Auto-generated method stub
		super.onActivityResult(requestCode, resultCode, data);

		if(ConnHelper.getConnInstance(this).isConnected()) {
			JSONObject jsonData = new JSONObject();		
			try {
				jsonData.put("cmd", AppConfig.SERV_CHK_CLIENT);				
				ConnHelper.getConnInstance(this).sendTextMessage(jsonData.toString());
	        	
			} catch (JSONException e) {
				e.printStackTrace();			
			}
		}
		else {
			//重新链接
			ConnHelper.reconnect();
		}
		
	}
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);				
		setContentView(R.layout.activity_main);
						
	}
		
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.main, menu);
		return true;
	}

	@Override
	public void onConfigurationChanged(Configuration newConfig) {
		super.onConfigurationChanged(newConfig);
		
	    if(this.getResources().getConfiguration().orientation == Configuration.ORIENTATION_LANDSCAPE) {
	        //当前为横屏， 在此处添加额外的处理代码
	    	
	    	//test
//	    	DisplayMetrics mDisplayMetrics = new DisplayMetrics();
//	    	getWindowManager().getDefaultDisplay().getMetrics(mDisplayMetrics);
//	    	int w = mDisplayMetrics.widthPixels;
//	    	int h = mDisplayMetrics.heightPixels;
	    	//Log.d("ozg test", "echo " + String.valueOf(w));
	    	//Log.d("ozg test", "echo " + String.valueOf(h));
	    	//Commons.alertErrMsg(this, "width " + String.valueOf(w) + " height " + String.valueOf(h));
	    	
			//socket相关
			ConnHelper.getConnInstance(this);
	    }
	    else if(this.getResources().getConfiguration().orientation == Configuration.ORIENTATION_PORTRAIT) {
	        //当前为竖屏， 在此处添加额外的处理代码

	    }

	    //检测实体键盘的状态：推出或者合上
	    if(newConfig.hardKeyboardHidden == Configuration.HARDKEYBOARDHIDDEN_NO) { 
	        //实体键盘处于推出状态，在此处添加额外的处理代码

	    }
	    else if (newConfig.hardKeyboardHidden == Configuration.HARDKEYBOARDHIDDEN_YES) { 
	        //实体键盘处于合上状态，在此处添加额外的处理代码

	    }
	    
	}
	
}
