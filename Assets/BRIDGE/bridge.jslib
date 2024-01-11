const plugin = {
  setScore: function (score) {
    try {
      const data = { event: 'SET_SCORE', payload: { score } };
      if(window.ReactNativeWebView) window.ReactNativeWebView.postMessage(JSON.stringify(data));
	    if(window.dispatchReactUnityEvent) window.dispatchReactUnityEvent('gameEvent', JSON.stringify(data));
    } catch (e) {
      console.warn('Failed to dispatch event');
    }
  },
  vibrate: function (isLong) {
    try {
      const data = { event: 'VIBRATE', payload: { isLong } };
      if(window.ReactNativeWebView) window.ReactNativeWebView.postMessage(JSON.stringify(data));
	    if(window.dispatchReactUnityEvent) window.dispatchReactUnityEvent('gameEvent', JSON.stringify(data));
    } catch (e) {
      console.warn('Failed to dispatch event');
    }
  },
  restart: function () {
    try {
      const data = { event: 'RESTART' };
      if(window.ReactNativeWebView) window.ReactNativeWebView.postMessage(JSON.stringify(data));
	    if(window.dispatchReactUnityEvent) window.dispatchReactUnityEvent('gameEvent', JSON.stringify(data));
    } catch (e) {
      console.warn('Failed to dispatch event');
    }
  },
  buyAsset: function (assetId) {
    try {
      assetId = UTF8ToString(assetId);
      const data = { event: 'BUY_ASSET', payload: { assetId } };
      if(window.ReactNativeWebView) window.ReactNativeWebView.postMessage(JSON.stringify(data));
	    if(window.dispatchReactUnityEvent) window.dispatchReactUnityEvent('gameEvent', JSON.stringify(data));
    } catch (e) {
      console.warn('Failed to post message');
    }
  },
  updateCoins: function (coinsChange) {
    try {
      const data = { event: 'UPDATE_COINS', payload: coinsChange };
      if(window.ReactNativeWebView) window.ReactNativeWebView.postMessage(JSON.stringify(data));
	    if(window.dispatchReactUnityEvent) window.dispatchReactUnityEvent('gameEvent', JSON.stringify(data));
    } catch (e) {
      console.warn('Failed to post message');
    }
  },
  updateExp: function (expChange) {
    try {
      const data = { event: 'UPDATE_EXP', payload: { expChange } };
      if(window.ReactNativeWebView) window.ReactNativeWebView.postMessage(JSON.stringify(data));
	    if(window.dispatchReactUnityEvent) window.dispatchReactUnityEvent('gameEvent', JSON.stringify(data));
    } catch (e) {
      console.warn('Failed to post message');
    }
  },
  load: function () {
    try {
      const data = { event: 'LOAD' };
      if(window.ReactNativeWebView) window.ReactNativeWebView.postMessage(JSON.stringify(data));
	    if(window.dispatchReactUnityEvent) window.dispatchReactUnityEvent('gameEvent', JSON.stringify(data));
    } catch (e) {
      console.warn('Failed to post message');
    }
  },
};

mergeInto(LibraryManager.library, plugin);
