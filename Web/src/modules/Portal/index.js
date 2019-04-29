import Module from '../../lib/baseModule';
import { connectModule } from '../../lib/moduleContext';

export default class Portal extends Module {
  createApp(config = {}) {
    const { main, components } = this._arguments;
    Object.entries(components).forEach(([name, component]) => {
      component.screen = connectModule(portal => {
        if (toString.call(component.module) === '[object Object]') {
          return component.module;
        }
        if (typeof component.module === 'string') {
          return portal[component.module];
        }
        throw new Error(`${name} components must be set module.`);
      })(component.screen);
    });
    const router = this.navigation.createRouter(components);
    const navigator = this.navigation.createNavigator(main, router, config);
    return this.navigation.createApp(navigator);
  }

  get navigation() {
    return this._modules.navigation;
  }

  get todos() {
    return this._modules.todos;
  }
}
