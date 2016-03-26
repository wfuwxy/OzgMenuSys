package com.kobo3.menusys;

import java.util.Timer;
import java.util.TimerTask;

import org.json.JSONException;
import org.json.JSONObject;

import android.content.Context;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import de.tavendo.autobahn.WebSocketConnection;
import de.tavendo.autobahn.WebSocketException;
import de.tavendo.autobahn.WebSocketOptions;

public class ConnHelper {
	
	public static WebSocketConnection connection = null;
	public static AppConnHandler handler = null;
	
	private static Handler mReconnectHandler = new Handler() {
		public void handleMessage(Message msg) {
			
			if(msg.what == 1) {
				if(!connection.isConnected()) {
					
					try {						
						connection.connect(AppConfig.SERV, AppConfig.PROTOCOLS, handler, new WebSocketOptions(), null);
					} catch (WebSocketException e) {						
						Log.d("ozgtest", "echo: " + e.toString());
					}
				}
			}
			
			super.handleMessage(msg);
		}
	};
	
	public static WebSocketConnection getConnInstance(Context context) {
		
		if(connection == null)
			connection = new WebSocketConnection();
		
		if(handler == null)
			handler = new AppConnHandler();	
		handler.mContext = context;
		
		if(!connection.isConnected()) {
			try {
				
				connection.connect(AppConfig.SERV, AppConfig.PROTOCOLS, handler, new WebSocketOptions(), null);
			} catch (WebSocketException e) {
				
				SharedPreferences sp = context.getSharedPreferences(AppConfig.APP_DATA, Context.MODE_PRIVATE);
				if(sp.contains(AppConfig.CLIENT_DATA)) {
					Editor editor = sp.edit();
					editor.remove(AppConfig.CLIENT_DATA);
					editor.commit();
				}
				
				Log.d("ozgtest", "echo: " + e.toString());
			}
		}
						
		return connection;
	}
	
	public static void reconnect() {
		TimerTask task = new TimerTask() {
			@Override
			public void run() {
				// TODO Auto-generated method stub
				Message message = new Message();
				message.what = 1;
				mReconnectHandler.sendMessage(message);
			}
		};
		Timer timer = new Timer();
		timer.schedule(task, AppConfig.RECONNECT_TIME);
	}
	
}
