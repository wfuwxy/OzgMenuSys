package com.ozg.ozgmenusys;

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
import android.content.pm.ActivityInfo;
import android.os.Bundle;
import android.util.Log;
import android.view.Window;
import android.view.WindowManager;

public class BaseActivity extends Activity {
	
	public WebSocketConnection mConnection = null;
	public AppConnHandler mHandler = null;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		this.requestWindowFeature(Window.FEATURE_NO_TITLE);
        this.getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN);
		
        this.setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_LANDSCAPE);
                
	}
	
	protected void connect() {
		if(this.mConnection == null)
			this.mConnection = new WebSocketConnection();
				
		if(!this.mConnection.isConnected()) {
			try {
				
				if(this.mHandler == null) {
					this.mHandler = new AppConnHandler();
					this.mHandler.mContext = this;
				}
				
				this.mConnection.connect(AppConfig.SERV, AppConfig.PROTOCOLS, this.mHandler, new WebSocketOptions(), null);
			} catch (WebSocketException e) {
				
				SharedPreferences sp = this.getSharedPreferences(AppConfig.APP_DATA, Context.MODE_PRIVATE);
				if(sp.contains(AppConfig.CLIENT_DATA)) {
					Editor editor = sp.edit();
					editor.remove(AppConfig.CLIENT_DATA);
					editor.commit();
				}
				
				Log.d("ozgtest", e.toString());
			}
		}
		else {
			SharedPreferences sp = this.getSharedPreferences(AppConfig.APP_DATA, Context.MODE_PRIVATE);
			
			if(sp.contains(AppConfig.CLIENT_DATA)) {
				
				this.toMainActivity();
			}
			else {
				JSONObject data = new JSONObject();
				
				try {
					data.put("cmd", AppConfig.SERV_CHK_CLIENT);
					this.mConnection.sendTextMessage(data.toString());			
				} catch (JSONException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
	        		
			}
			
		}		
	}
	
	protected void toMainActivity() {
		this.mConnection.disconnect();
		this.mConnection = null;
		this.mHandler = null;
		
		Intent intent = new Intent();					
        intent.setClass(this, MenuActivity.class);
        this.startActivityForResult(intent, 1);
	}
	
}
