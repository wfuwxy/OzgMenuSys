package com.ozg.menusys;

import java.io.File;

import org.json.JSONException;
import org.json.JSONObject;

import de.tavendo.autobahn.WebSocketConnection;
import de.tavendo.autobahn.WebSocketConnectionHandler;
import de.tavendo.autobahn.WebSocketException;
import de.tavendo.autobahn.WebSocketOptions;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.graphics.drawable.AnimationDrawable;
import android.os.Bundle;
import android.util.DisplayMetrics;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.TextView;

public class MenuActivity extends BaseActivity {
		
	public String mCmd = null;
	
	private ImageView mProgressView = null;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_menu);
		
		//视图相关
		((Button)this.findViewById(R.id.btn_order)).setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {				
				MenuActivity.this.getOrderList();
			}			
		});
		
		((Button)this.findViewById(R.id.btn_payment)).setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				//结账处理
				AlertDialog.Builder builder = new AlertDialog.Builder(MenuActivity.this);
				builder.setMessage(R.string.menu_payment_dialog_msg);
				builder.setTitle(R.string.menu_commons_dialog_title);
								
				builder.setPositiveButton(R.string.menu_commons_dialog_btn_yes, new DialogInterface.OnClickListener() {

					@Override
					public void onClick(DialogInterface dialog, int which) {
						MenuActivity.this.payment();
						dialog.dismiss();
					}
				});
				
				builder.setNegativeButton(R.string.menu_commons_dialog_btn_no, new DialogInterface.OnClickListener() {

					@Override
					public void onClick(DialogInterface dialog, int which) {
						dialog.dismiss();
					}

				});
				
				builder.create().show();	
			}			
		});
		
		TextView labName = (TextView)this.findViewById(R.id.lab_name);
		
		SharedPreferences sp = this.getSharedPreferences(AppConfig.APP_DATA, MODE_PRIVATE);
		try {
			JSONObject clientData = new JSONObject(sp.getString(AppConfig.CLIENT_DATA, ""));
			JSONObject client = clientData.getJSONObject("client");
			
			labName.setText(client.getString("name"));
			
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		//请求数据
		this.getMenuClassList();
		
	}

	public void getMenuClassList() {
		this.showProgress();
		
		this.mCmd = AppConfig.SERV_MENU_CLASS_LIST;
				
		JSONObject sendData = new JSONObject();
		try {
			sendData.put("cmd", this.mCmd);
			ConnHelper.getConnInstance(this).sendTextMessage(sendData.toString());
		} catch (JSONException e) {

			e.printStackTrace();
		}
		
	}
	
	public void getMenuList(int dataId) {
		this.showProgress();
		
		this.mCmd = AppConfig.SERV_MENU_LIST;
		JSONObject sendData = new JSONObject();
		try {
			sendData.put("data", dataId);
			sendData.put("cmd", this.mCmd);
			ConnHelper.getConnInstance(this).sendTextMessage(sendData.toString());
			
		} catch (JSONException e) {

			e.printStackTrace();
		}
		
	}
	
	public void getOrderList() {
		this.showProgress();
		
		this.mCmd = AppConfig.SERV_ORDER_LIST;
		JSONObject sendData = new JSONObject();
		try {
			sendData.put("cmd", this.mCmd);
			ConnHelper.getConnInstance(this).sendTextMessage(sendData.toString());
			
		} catch (JSONException e) {

			e.printStackTrace();
		}
		
	}
	
	private void getImage(int dataId, String cmd) {
		this.mCmd = cmd;
		JSONObject sendData = new JSONObject();
		try {
			sendData.put("data", dataId);
			sendData.put("cmd", this.mCmd);
			ConnHelper.getConnInstance(this).sendTextMessage(sendData.toString());
			
		} catch (JSONException e) {

			e.printStackTrace();
		}
		
	}
	
	public void getBigImage(int dataId) {
		
		String cacheImgFileName = "cache_" + String.valueOf(dataId) + "_bigimg.png";
		File cacheImgFile = new File(this.getFilesDir() + "/" + cacheImgFileName);
    	if(cacheImgFile.exists()) {
    		//缓存文件存在
    		
    		if(System.currentTimeMillis() - cacheImgFile.lastModified() > AppConfig.IMG_CACHE_TIMEOUT) {
    			//缓存过期
    			cacheImgFile.delete();
    			
    			this.showProgress();
    			this.getImage(dataId, AppConfig.SERV_BIG_IMAGE);
    		}
    		else {
    			String imgContent = Commons.imgCacheRead(this, cacheImgFileName);
    			ConnHelper.handler.showBigImage(imgContent);
    		}
    	}
    	else {
    		this.showProgress();
    		this.getImage(dataId, AppConfig.SERV_BIG_IMAGE);
    	}
    	
	}
	
	public void getSmallImage(int dataId, String name, float price, String smallImg) {
		
		String cacheImgFileName = "cache_" + String.valueOf(dataId) + "_smallimg.png";
		File cacheImgFile = new File(this.getFilesDir() + "/" + cacheImgFileName);
    	if(cacheImgFile.exists()) {
    		//缓存文件存在
    		
    		if(System.currentTimeMillis() - cacheImgFile.lastModified() > AppConfig.IMG_CACHE_TIMEOUT) {
    			//缓存过期
    			cacheImgFile.delete();
    			this.getImage(dataId, AppConfig.SERV_SMALL_IMAGE);
    		}
    		else {
    			String imgContent = Commons.imgCacheRead(this, cacheImgFileName);
    			ConnHelper.handler.showSmallImage(imgContent, dataId, name, price, smallImg);
    		}
    	}
    	else
    		this.getImage(dataId, AppConfig.SERV_SMALL_IMAGE);
		
	}
	
	public void addOrder(int menuId, int quantity) {
		this.showProgress();
		
		this.mCmd = AppConfig.SERV_ADD_ORDER;
		JSONObject sendData = new JSONObject();
		try {
			
			JSONObject data = new JSONObject();
			data.put("menu_id", menuId);
			data.put("quantity", quantity);
			
			sendData.put("data", data);
			sendData.put("cmd", this.mCmd);			
			ConnHelper.getConnInstance(this).sendTextMessage(sendData.toString());
			
		} catch (JSONException e) {

			e.printStackTrace();
		}
	}
	
	public void payment() {
		this.mCmd = AppConfig.SERV_PAYMENT;
		JSONObject sendData = new JSONObject();
		try {
			
			sendData.put("cmd", this.mCmd);			
			ConnHelper.getConnInstance(this).sendTextMessage(sendData.toString());
			
		} catch (JSONException e) {

			e.printStackTrace();
		}
		
	}
	
	public void enabledView(Boolean enabled) {
		((Button)this.findViewById(R.id.btn_order)).setClickable(enabled);
		((Button)this.findViewById(R.id.btn_payment)).setClickable(enabled);
		
		LinearLayout classSvLayout  = (LinearLayout)this.findViewById(R.id.menu_class_sv_layout);
		for(int i = 0; i < classSvLayout.getChildCount(); i++) {
			View classItem = classSvLayout.getChildAt(i);
			Button classBtn = (Button)classItem.findViewById(R.id.menu_class_item_root).findViewById(R.id.menu_class_item_btn);
			classBtn.setClickable(enabled);
		}
		
	}
	
	//显示进度条
	public void showProgress() {
		this.hideProgress();
		
		if(this.mProgressView == null) {
			RelativeLayout mainRoot = (RelativeLayout)this.findViewById(R.id.menu_root);
			
			RelativeLayout.LayoutParams lp = new RelativeLayout.LayoutParams(96, 96);		
			
			DisplayMetrics dm = new DisplayMetrics();getWindowManager().getDefaultDisplay().getMetrics(dm);
		    
			lp.leftMargin = dm.widthPixels / 2;
			lp.topMargin = dm.heightPixels / 2;
			lp.addRule(RelativeLayout.ALIGN_PARENT_TOP);
			lp.addRule(RelativeLayout.ALIGN_PARENT_LEFT);
			lp.addRule(RelativeLayout.CENTER_HORIZONTAL, RelativeLayout.TRUE);
			this.mProgressView = new ImageView(this);
			this.mProgressView.setBackgroundResource(R.drawable.loading);		
			mainRoot.addView(this.mProgressView, lp);
			AnimationDrawable anim = (AnimationDrawable)this.mProgressView.getBackground();
			anim.start();	
		}
	}
	
	//隐藏（移除）进度条
	public void hideProgress() {
		RelativeLayout mainRoot = (RelativeLayout)this.findViewById(R.id.menu_root);
		if(this.mProgressView != null) {
			AnimationDrawable anim = (AnimationDrawable)this.mProgressView.getBackground();
			anim.stop();
			
			mainRoot.removeView(this.mProgressView);
			this.mProgressView = null;
		}
	}
	
}
