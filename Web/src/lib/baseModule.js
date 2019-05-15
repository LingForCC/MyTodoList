import { applyMiddleware, createStore } from 'redux';
import logger from 'redux-logger';
import Module, { state, action, computed } from 'usm-redux';
import { persistStore, persistReducer } from 'redux-persist';
import * as localForage from 'localforage';

class BaseModule extends Module {
  static _generateStore(_, module) {
    const persistConfig = {
      key: 'root',
      storage: localForage,
    };
    const persistedReducer = persistReducer(persistConfig, module.reducers);
    const store = createStore(
      persistedReducer, 
      applyMiddleware(logger)
    );
    module.persistor = persistStore(store);
    return store;
  }
}

export {
  BaseModule as default,
  state,
  action,
  computed
} 
