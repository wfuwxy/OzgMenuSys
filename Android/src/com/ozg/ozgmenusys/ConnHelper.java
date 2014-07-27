package com.ozg.ozgmenusys;

import org.json.JSONException;
import org.json.JSONObject;

import android.content.Context;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.util.Log;
import de.tavendo.autobahn.WebSocketConnection;
import de.tavendo.autobahn.WebSocketException;
import de.tavendo.autobahn.WebSocketOptions;

public class ConnHelper {
	
	public static WebSocketConnection connection = null;
	public static AppConnHandler handler = null;
	
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
				
				Log.d("ozgtest", e.toString());
			}
		}
						
		return connection;
	}
	
}
