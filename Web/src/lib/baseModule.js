import { applyMiddleware, createStore } from 'redux';
import logger from 'redux-logger';
import Module, { state, action, computed } from 'usm-redux';

class BaseModule extends Module {
  static _generateStore(_, { reducers }) {
    return createStore(
      reducers,
      applyMiddleware(logger)
    );
  }
}

export {
  BaseModule as default,
  state,
  action,
  computed
} 
