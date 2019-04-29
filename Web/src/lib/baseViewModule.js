import Module, { state, action, computed } from './baseModule';

function produceErrorMessage(propertyName) {
  const moduleName = this.__proto__.constructor.name;
  return  `${moduleName} property '${propertyName}' need be overridden.`;
}

class ViewModule extends Module {
  getViewProps() {
    throw new Error(produceErrorMessage.call(this, 'getViewProps'));
  }
}

export {
  ViewModule as default,
  state,
  action,
  computed
}