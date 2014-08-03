package com.ozg.menusys;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileOutputStream;
import java.io.FileReader;
import java.io.IOException;
import java.sql.Date;
import java.text.SimpleDateFormat;
import java.util.Timer;
import java.util.TimerTask;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.ozg.menusys.R.drawable;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.AlertDialog.Builder;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.graphics.Bitmap;
import android.graphics.Color;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.util.DisplayMetrics;
import android.util.Log;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;
import android.view.ViewGroup;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.HorizontalScrollView;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.ScrollView;
import android.widget.TextView;
import de.tavendo.autobahn.WebSocketConnectionHandler;

public class AppConnHandler extends WebSocketConnectionHandler {

	public Context mContext;
	
	private View mCurrShowMainView = null;
	
	private View mMaskView = null;
	
	private int mSelectedMenuClassID = 0;
	
	 //获取菜单分类列表数据后显示时用到
	private class MenuClassItemOnClickListener implements OnClickListener {
		
		private int mMenuClassId;
		
		@Override
		public void onClick(View v) {
			
			LinearLayout menuClassSvLayout = (LinearLayout)((MenuActivity)AppConnHandler.this.mContext).findViewById(R.id.menu_class_sv_layout);
			for(int i = 0; i < menuClassSvLayout.getChildCount(); i++)
				((Button)menuClassSvLayout.getChildAt(i).findViewById(R.id.menu_class_item_btn)).setTextColor(Color.BLACK);			
			
			((Button)v).setTextColor(Color.RED);
			
			AppConnHandler.this.mSelectedMenuClassID = this.getMenuClassId();
			((MenuActivity)AppConnHandler.this.mContext).getMenuList(this.getMenuClassId());
		}

		public int getMenuClassId() {
			return mMenuClassId;
		}

		public void setMenuClassId(int menuClassId) {
			this.mMenuClassId = menuClassId;
		}
		
	}
	
	//获取菜单列表数据后显示时用到
	private class MenuItemOnClickListener implements OnClickListener {
		
		private int mMenuId;
		
		private String mName;
		private float mPrice;
		
		public MenuItemOnClickListener(String name, float price) {
			
			this.mName = name;
			this.mPrice = price;
			
		}
		
		@Override
		public void onClick(View v) {
			
			RelativeLayout rootLayout = (RelativeLayout)((MenuActivity)AppConnHandler.this.mContext).findViewById(R.id.menu_root);
			
			DisplayMetrics dm = new DisplayMetrics();
			((MenuActivity)AppConnHandler.this.mContext).getWindowManager().getDefaultDisplay().getMetrics(dm);
			
			RelativeLayout.LayoutParams lp = new RelativeLayout.LayoutParams(dm.widthPixels, dm.heightPixels);	
			
			if(AppConnHandler.this.mMaskView != null) {
				rootLayout.removeView(AppConnHandler.this.mMaskView);
				AppConnHandler.this.mMaskView = null;
			}
			AppConnHandler.this.mMaskView = View.inflate(AppConnHandler.this.mContext, R.layout.menu_detail, null);
					
			//图片
			((MenuActivity)AppConnHandler.this.mContext).getBigImage(this.mMenuId);
						
			//名称
			TextView labName = (TextView)AppConnHandler.this.mMaskView.findViewById(R.id.menu_detail_lab_name);
			labName.setText(this.mName);
			
			//价格
			String priceData = AppConnHandler.this.mContext.getResources().getString(R.string.menu_list_item_price);
			
			TextView labPrice = (TextView)AppConnHandler.this.mMaskView.findViewById(R.id.menu_detail_lab_price);
			labPrice.setText(String.format(priceData, this.mPrice));
			labPrice.setGravity(Gravity.RIGHT);
			
			//挡住层后面的事件触发
			AppConnHandler.this.mMaskView.setOnTouchListener(new OnTouchListener() { 

				@Override
				public boolean onTouch(View v, MotionEvent event) {
					return true;
				}
			});
			
			//关闭按钮
			((Button)AppConnHandler.this.mMaskView.findViewById(R.id.menu_detail_btn_close)).setOnClickListener(new OnClickListener() {

				@Override
				public void onClick(View v) {
					RelativeLayout rootLayout = (RelativeLayout)((MenuActivity)AppConnHandler.this.mContext).findViewById(R.id.menu_root);
					rootLayout.removeView(AppConnHandler.this.mMaskView);
					AppConnHandler.this.mMaskView = null;					
				}				
			});
			
			//下单按钮
			MenuDetailAddOderOnClickListener l = new MenuDetailAddOderOnClickListener();
			l.mMenuId = this.mMenuId;
			((Button)AppConnHandler.this.mMaskView.findViewById(R.id.menu_detail_btn_addorder)).setOnClickListener(l);
			
			//固定索引是为了确保显示在进度条的下面
			rootLayout.addView(AppConnHandler.this.mMaskView, 2, lp);			
		}
		
		public int getMenuId() {
			return mMenuId;
		}

		public void setMenuId(int menuId) {
			this.mMenuId = menuId;
		}
		
	}
	
	private class MenuDetailAddOderOnClickListener implements OnClickListener {
		
		private int mMenuId;
		private int mQuantity;
		
		@Override
		public void onClick(View v) {
			AlertDialog.Builder builder = new AlertDialog.Builder(AppConnHandler.this.mContext);
			builder.setMessage(R.string.menu_detail_addorder_dialog_msg);
			builder.setTitle(R.string.menu_commons_dialog_title);
			
			this.mQuantity = 1;
			
			builder.setPositiveButton(R.string.menu_commons_dialog_btn_yes, new DialogInterface.OnClickListener() {

				@Override
				public void onClick(DialogInterface dialog, int which) {
					
					((MenuActivity)AppConnHandler.this.mContext).addOrder(MenuDetailAddOderOnClickListener.this.mMenuId, MenuDetailAddOderOnClickListener.this.mQuantity);
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
				
	}
	
	@Override
	public void onClose(int code, String reason) {
		
		Log.d("ozgtest", "echo: connection lost.");
    			
		if((Activity)this.mContext instanceof MainActivity) {
						
			Button btn = (Button)((MainActivity)this.mContext).findViewById(R.id.main_btn);
	    	btn.setGravity(View.VISIBLE);
			btn.setText(R.string.main_btn_text2);		
			
			//重新链接
			ConnHelper.reconnect();
		}
		else if((Activity)this.mContext instanceof MenuActivity) {
			//退回到第一个界面再重新链接
			((MenuActivity)this.mContext).finish();
		}
		    	
	}

	@Override
	public void onOpen() {

		JSONObject data = new JSONObject();
					
		try {
			if((Activity)this.mContext instanceof MainActivity) {
				data.put("cmd", AppConfig.SERV_CHK_CLIENT);				
				ConnHelper.getConnInstance(this.mContext).sendTextMessage(data.toString());
			}
        	
		} catch (JSONException e) {
			e.printStackTrace();
			
			if((Activity)this.mContext instanceof MainActivity)
				ConnHelper.getConnInstance(this.mContext).disconnect();
			else if((Activity)this.mContext instanceof MenuActivity)
				ConnHelper.getConnInstance(this.mContext).disconnect();
			
		}

	}

	@Override
	public void onTextMessage(String payload) {
//		Log.d("ozgtest", "Got echo: " + payload);
    			
		if((Activity)this.mContext instanceof MainActivity) {
			
			//主界面		
			
			Button btn = (Button)((MainActivity)this.mContext).findViewById(R.id.main_btn);
	    	
	    	try {
				JSONObject data = new JSONObject(payload);

				if(data.has("cmd") && data.getString("cmd").equals(AppConfig.CLIENT_WANT_TOMENU)) {
					//被动接受了服务器的回应命令，跳到菜单界面
					
					SharedPreferences sp = ((MainActivity)this.mContext).getSharedPreferences(AppConfig.APP_DATA, Context.MODE_PRIVATE);
					Editor editor = sp.edit();
					editor.putString(AppConfig.CLIENT_DATA, data.getJSONObject("data").toString());
					editor.commit();
										
					Intent intent = new Intent();					
	                intent.setClass((MainActivity)this.mContext, MenuActivity.class);
	                ((MainActivity)this.mContext).startActivityForResult(intent, 1);
					
				}		
				else if(data.has("cmd") && data.getString("cmd").equals(AppConfig.CLIENT_WANT_PAYMENT)) {
					//已经结账了
										
					Bundle bundle = new Bundle(); 
					bundle.putString("cmd", AppConfig.CLIENT_WANT_PAYMENT);
					bundle.putString("message", data.getString("message"));
					
					Intent intent = new Intent();	
					intent.putExtras(bundle); 
	                intent.setClass((MainActivity)this.mContext, MenuActivity.class);
	                ((MainActivity)this.mContext).startActivityForResult(intent, 1);
				}
				else {
					TextView labMsg = (TextView)((MainActivity)this.mContext).findViewById(R.id.main_lab_msg);
					labMsg.setText(data.getString("message"));
												
					if(data.getInt("ok") == 1) {
						btn.setText(R.string.main_btn_text1);
						btn.setGravity(View.VISIBLE);
						
						SharedPreferences sp = ((MainActivity)this.mContext).getSharedPreferences(AppConfig.APP_DATA, Context.MODE_PRIVATE);
						Editor editor = sp.edit();
						editor.putString(AppConfig.CLIENT_DATA, data.getJSONObject("data").toString());
						editor.commit();
										
					}
				}
				
			} catch (JSONException e) {
				Log.d("ozgtest", e.toString());
				
				ConnHelper.getConnInstance(this.mContext).disconnect();
			}
		}
		else if((Activity)this.mContext instanceof MenuActivity) {
			
			//菜单界面
			JSONObject jsonData;
			
			try {
				jsonData = new JSONObject(payload);

				if(jsonData.has("cmd")) {
					if(jsonData.getString("cmd").equals(AppConfig.CLIENT_WANT_MENU_CLASS)) {
						//菜单分类数据
						((MenuActivity)this.mContext).hideProgress();
						
						LinearLayout menuClassSvLayout = (LinearLayout)((MenuActivity)this.mContext).findViewById(R.id.menu_class_sv_layout);
						menuClassSvLayout.removeAllViews();
						
						if(jsonData.getInt("ok") == 1) {
							
							JSONArray data = jsonData.getJSONArray("data");
							for(int i = 0; i < data.length(); i++) {
								JSONObject item = data.getJSONObject(i);
//								Log.d("ozgtest", item.getString("name"));
								
								MenuClassItemOnClickListener l = new MenuClassItemOnClickListener();
								l.setMenuClassId(item.getInt("id"));
								
								View classItem = View.inflate(this.mContext, R.layout.menu_class_item, null);
								Button classBtn = (Button)classItem.findViewById(R.id.menu_class_item_root).findViewById(R.id.menu_class_item_btn);
								classBtn.setText(item.getString("name"));
								classBtn.setOnClickListener(l);
								
								//选定一个分类
								if(i == 0 && this.mSelectedMenuClassID == 0)
									classBtn.setTextColor(Color.RED);
								else if(this.mSelectedMenuClassID > 0 && item.getInt("id") == this.mSelectedMenuClassID)
									classBtn.setTextColor(Color.RED);
								
								menuClassSvLayout.addView(classItem);
								
							}
							
							if(data.length() > 0) {
								//默认
								
								if(this.mSelectedMenuClassID == 0) {
									JSONObject item = data.getJSONObject(0);									
									this.mSelectedMenuClassID = item.getInt("id");
									((MenuActivity)this.mContext).getMenuList(item.getInt("id"));
								}
								else
									((MenuActivity)this.mContext).getMenuList(this.mSelectedMenuClassID);
								
							}
							else {
								//没有分类数据
								Commons.alertErrMsg(this.mContext, this.mContext.getString(R.string.menu_no_menuclassdata_msg));
							}
							
						}
						else {
							//请求出问题了
							
						}						
					}
					else if(jsonData.getString("cmd").equals(AppConfig.CLIENT_WANT_MENU)) {
						//菜单数据
						((MenuActivity)this.mContext).hideProgress();
						
						if(jsonData.getInt("ok") == 1) {
							
							RelativeLayout menuRoot = (RelativeLayout)((MenuActivity)this.mContext).findViewById(R.id.menu_root);
							
							if(this.mCurrShowMainView != null) {
								menuRoot.removeView(this.mCurrShowMainView);
								this.mCurrShowMainView = null;
							}
													
							RelativeLayout.LayoutParams lp = new RelativeLayout.LayoutParams(2560, 1920);		
							lp.leftMargin = 550;
							lp.topMargin = 110;
							lp.addRule(RelativeLayout.ALIGN_PARENT_TOP);
							lp.addRule(RelativeLayout.ALIGN_PARENT_LEFT);
							lp.addRule(RelativeLayout.CENTER_HORIZONTAL, RelativeLayout.TRUE);
							
							this.mCurrShowMainView = View.inflate(this.mContext, R.layout.menu_list, null);
							
							LinearLayout menuListRootLayout = ((LinearLayout)this.mCurrShowMainView.findViewById(R.id.menu_list_root_layout));
							menuListRootLayout.removeAllViews();
							
							//固定索引是为了确保显示在进度条的下面
							menuRoot.addView(this.mCurrShowMainView, 1, lp);
													
							JSONArray menuData = jsonData.getJSONArray("data");
							for(int i = 0; i < menuData.length(); i++) {
								JSONObject item = menuData.getJSONObject(i);
															
								View menuItem = View.inflate(this.mContext, R.layout.menu_item, null);
								menuListRootLayout.addView(menuItem);
								
								TextView labName = (TextView)menuItem.findViewById(R.id.menu_item_lab_name);
								labName.setText(item.getString("name"));
								
								TextView labSmallImg = (TextView)menuItem.findViewById(R.id.menu_item_lab_small_img);
								labSmallImg.setText(item.getString("small_img"));
								
								String priceData = this.mContext.getResources().getString(R.string.menu_list_item_price);
								
								TextView labPrice = (TextView)menuItem.findViewById(R.id.menu_item_lab_price);
								labPrice.setText(String.format(priceData, (float)item.getDouble("price")));
								labPrice.setGravity(Gravity.RIGHT);
															
								((MenuActivity)AppConnHandler.this.mContext).getSmallImage(item.getInt("id"), item.getString("name"), (float)item.getDouble("price"), item.getString("small_img"));
								
							}
							
							//没有数据的处理
							if(menuData.length() == 0) {
								Commons.alertErrMsg(this.mContext, this.mContext.getString(R.string.menu_no_menudata_msg));
							}
							
						}
						else {
							//请求出问题了
							
						}
						
						((MenuActivity)this.mContext).enabledView(true);
					}
					else if(jsonData.getString("cmd").equals(AppConfig.CLIENT_WANT_ORDER_LIST)) {
						//订单数据
						((MenuActivity)this.mContext).hideProgress();
						
						if(jsonData.getInt("ok") == 1) {
							
							RelativeLayout menuRoot = (RelativeLayout)((MenuActivity)this.mContext).findViewById(R.id.menu_root);
							
							if(this.mCurrShowMainView != null) {
								menuRoot.removeView(this.mCurrShowMainView);
								this.mCurrShowMainView = null;
							}
							
							RelativeLayout.LayoutParams lp = new RelativeLayout.LayoutParams(2560, 1920);		
							lp.leftMargin = 550;
							lp.topMargin = 0;
							lp.addRule(RelativeLayout.ALIGN_PARENT_TOP);
							lp.addRule(RelativeLayout.ALIGN_PARENT_LEFT);
							lp.addRule(RelativeLayout.CENTER_HORIZONTAL, RelativeLayout.TRUE);	
															
							this.mCurrShowMainView = View.inflate(this.mContext, R.layout.menu_order_list, null);
							
							//固定索引是为了确保显示在进度条的下面
							menuRoot.addView(this.mCurrShowMainView, 1, lp);
							
							LinearLayout menuOrderSvLayout = (LinearLayout)this.mCurrShowMainView.findViewById(R.id.menu_order_list_root).findViewById(R.id.menu_order_sv).findViewById(R.id.menu_order_sv_layout);
							
							int totalQuantity = 0;
							float totalPrice = 0.0f;
							
							JSONArray orderDataList = jsonData.getJSONArray("data");
							for(int i = 0; i < orderDataList.length(); i++) {
								View orderItemView = View.inflate(this.mContext, R.layout.menu_order_item, null);
								JSONObject orderDataItem = orderDataList.getJSONObject(i);
								
								TextView orderItemName = (TextView)orderItemView.findViewById(R.id.menu_order_item_root).findViewById(R.id.menu_order_item_name);
								TextView orderItemQuantity = (TextView)orderItemView.findViewById(R.id.menu_order_item_root).findViewById(R.id.menu_order_item_quantity);
								TextView orderItemPrice = (TextView)orderItemView.findViewById(R.id.menu_order_item_root).findViewById(R.id.menu_order_item_price);
								TextView orderItemTime = (TextView)orderItemView.findViewById(R.id.menu_order_item_root).findViewById(R.id.menu_order_item_time);
								
								totalQuantity += orderDataItem.getInt("quantity");
								float price = (float)(orderDataItem.getDouble("price") * (double)orderDataItem.getInt("quantity"));
								totalPrice += price;
								
								orderItemName.setText(orderDataItem.getString("menu_name"));
								orderItemQuantity.setText(String.valueOf(orderDataItem.getInt("quantity")));
								
								String priceData = this.mContext.getResources().getString(R.string.menu_order_item_price);
								orderItemPrice.setText(String.format(priceData, price));
								
								Date d = new Date((long)orderDataItem.getInt("add_time") * 1000L);
								SimpleDateFormat sf = new SimpleDateFormat("HH:mm:ss");
								orderItemTime.setText(sf.format(d));
								
								menuOrderSvLayout.addView(orderItemView);
							}
							
							//总价
							if(orderDataList.length() > 0) {
								View orderItemView = View.inflate(this.mContext, R.layout.menu_order_item, null);
								TextView orderItemName = (TextView)orderItemView.findViewById(R.id.menu_order_item_root).findViewById(R.id.menu_order_item_name);
								TextView orderItemQuantity = (TextView)orderItemView.findViewById(R.id.menu_order_item_root).findViewById(R.id.menu_order_item_quantity);
								TextView orderItemPrice = (TextView)orderItemView.findViewById(R.id.menu_order_item_root).findViewById(R.id.menu_order_item_price);
								TextView orderItemTime = (TextView)orderItemView.findViewById(R.id.menu_order_item_root).findViewById(R.id.menu_order_item_time);
								
								orderItemName.setText("总价");
								orderItemName.setTextColor(Color.RED);
								orderItemName.setTextSize(20.0f);
								
								orderItemQuantity.setText(String.valueOf(totalQuantity));
								orderItemQuantity.setTextColor(Color.RED);
								orderItemQuantity.setTextSize(20.0f);
								
								String priceData = this.mContext.getResources().getString(R.string.menu_order_item_price);
								orderItemPrice.setText(String.format(priceData, totalPrice));
								orderItemPrice.setTextColor(Color.RED);
								orderItemPrice.setTextSize(20.0f);
								
								Date d = new Date((long)orderDataList.getJSONObject(0).getInt("o_update_time") * 1000L);
								SimpleDateFormat sf = new SimpleDateFormat("HH:mm:ss");
								orderItemTime.setText(sf.format(d));
								orderItemTime.setTextColor(Color.RED);
								orderItemTime.setTextSize(20.0f);
								
								menuOrderSvLayout.addView(orderItemView);
							}
							
						}
						else {						
							//请求出问题了
							
						}
						
						((MenuActivity)this.mContext).enabledView(true);
					}
					else if(jsonData.getString("cmd").equals(AppConfig.CLIENT_WANT_BIG_IMAGE)) {
						//大图
						((MenuActivity)this.mContext).hideProgress();
						
						String imgContent = jsonData.getJSONObject("data").getString("img_base64str");

						//保存图片本地缓存							
						JSONObject menuData = jsonData.getJSONObject("data").getJSONObject("menu_data");
				    	String cacheImgFileName = "cache_" + String.valueOf(menuData.getInt("id")) + "_bigimg.png";
				    	Commons.imgCacheWrite(this.mContext, cacheImgFileName, imgContent);
						
				    	this.showBigImage(imgContent);
					}
					else if(jsonData.getString("cmd").equals(AppConfig.CLIENT_WANT_SMALL_IMAGE)) {
						//小图
						
						//保存图片本地缓存
						JSONObject menuData = jsonData.getJSONObject("data").getJSONObject("menu_data");
						String imgContent = jsonData.getJSONObject("data").getString("img_base64str");
						String cacheImgFileName = "cache_" + String.valueOf(menuData.getInt("id")) + "_smallimg.png";
				    	Commons.imgCacheWrite(this.mContext, cacheImgFileName, imgContent);
						
				    	this.showSmallImage(imgContent, menuData.getInt("id"), menuData.getString("name"), (float)menuData.getDouble("price"), menuData.getString("small_img"));	
					}
					else if(jsonData.getString("cmd").equals(AppConfig.CLIENT_WANT_ADDED_ORDER)) {
						//下单
						((MenuActivity)this.mContext).hideProgress();
						
						RelativeLayout rootLayout = (RelativeLayout)((MenuActivity)AppConnHandler.this.mContext).findViewById(R.id.menu_root);
						
						if(AppConnHandler.this.mMaskView != null) {
							rootLayout.removeView(AppConnHandler.this.mMaskView);
							AppConnHandler.this.mMaskView = null;
						}
						
						Commons.alertErrMsg(this.mContext, jsonData.getString("message"));
					}
					else if(jsonData.getString("cmd").equals(AppConfig.CLIENT_WANT_PAYMENT)) {
						//结账
						
						if(jsonData.getInt("ok") == 1) {
							//进入结账界面
							this.showPayment(jsonData.getString("message"));
						}
						else {
							//结账失败
							Commons.alertErrMsg(this.mContext, jsonData.getString("message"));
						}
											
					}
					else if(jsonData.getString("cmd").equals(AppConfig.CLIENT_WANT_TOMAIN)) {
						//服务器是否已经归档了，返回第一个界面
						
						SharedPreferences sp = this.mContext.getSharedPreferences(AppConfig.APP_DATA, Context.MODE_PRIVATE);
						if(sp.contains(AppConfig.CLIENT_DATA)) {
							Editor editor = sp.edit();
							editor.remove(AppConfig.CLIENT_DATA);
							editor.commit();
						}

						//回到第一个界面
						((MenuActivity)this.mContext).finish();			
					}
					
				}

			} catch (JSONException e) {

				e.printStackTrace();
			}					
			
		}
	}
		
	//显示大图
	public void showBigImage(String imgBase64Str) {
		Bitmap bitmap = Commons.stringToBitmap(imgBase64Str);
		Drawable d = new BitmapDrawable(this.mContext.getResources(), bitmap);
	
		ImageView bigImg = (ImageView)AppConnHandler.this.mMaskView.findViewById(R.id.menu_detail_big_img);
		bigImg.setImageDrawable(d);		
	}
	
	//显示小图
	public void showSmallImage(String imgBase64Str, int dataId, String name, float price, String smallImg) {
		LinearLayout listRootLayout = (LinearLayout)this.mCurrShowMainView.findViewById(R.id.menu_list_root_layout);
		for(int i = 0; i < listRootLayout.getChildCount(); i++) {
			View menuItem = listRootLayout.getChildAt(i);
			TextView labSmallImg = (TextView)menuItem.findViewById(R.id.menu_item_lab_small_img);
											
			if(labSmallImg.getText().equals(smallImg)) {
																
				Bitmap bitmap = Commons.stringToBitmap(imgBase64Str);
				Drawable d = new BitmapDrawable(this.mContext.getResources(), bitmap);			
				Drawable samllImgDrawable = Commons.zoomDrawable(d, Commons.getSamllImgSize(d.getIntrinsicWidth()), Commons.getSamllImgSize(d.getIntrinsicHeight()));
				ImageView samllImg = (ImageView)menuItem.findViewById(R.id.menu_item_small_img);
				samllImg.setImageDrawable(samllImgDrawable);
				
				MenuItemOnClickListener l = new MenuItemOnClickListener(name, price);
				l.setMenuId(dataId);
				menuItem.setOnClickListener(l);
				
				break;
			}								
		}
	}
	
	//显示结账界面
	public void showPayment(String message) {
		RelativeLayout rootLayout = (RelativeLayout)((MenuActivity)AppConnHandler.this.mContext).findViewById(R.id.menu_root);
		
		DisplayMetrics dm = new DisplayMetrics();
		((MenuActivity)AppConnHandler.this.mContext).getWindowManager().getDefaultDisplay().getMetrics(dm);
		
		RelativeLayout.LayoutParams lp = new RelativeLayout.LayoutParams(dm.widthPixels, dm.heightPixels);	
		
		if(this.mMaskView != null) {
			rootLayout.removeView(this.mMaskView);
			this.mMaskView = null;
		}
		
		this.mMaskView = View.inflate(this.mContext, R.layout.menu_payment_result, null);
		
		TextView paymentResultLabMsg = (TextView)this.mMaskView.findViewById(R.id.menu_payment_result_lab_msg);
		paymentResultLabMsg.setText(message);
		paymentResultLabMsg.setGravity(Gravity.CENTER);
		
		//挡住层后面的事件触发
		this.mMaskView.setOnTouchListener(new OnTouchListener() { 

			@Override
			public boolean onTouch(View v, MotionEvent event) {
				return true;
			}
		});
		
		rootLayout.addView(AppConnHandler.this.mMaskView, lp);	
	}
	
}
