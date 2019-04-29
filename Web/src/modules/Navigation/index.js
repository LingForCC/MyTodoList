import { createBrowserApp } from '@react-navigation/web';
import { createNavigator, SwitchRouter } from "@react-navigation/core";
import Module from '../../lib/baseModule';

export default class Navigation extends Module {
  constructor(...args) {
    super(...args);
    this._createApp = createBrowserApp;
    this._createNavigator = createNavigator;
    this._createRouter = SwitchRouter;
  }

  createApp(...args) {
    return this._createApp(...args);
  }

  createNavigator(...args) {
    return this._createNavigator(...args);
  }

  createRouter(...args) {
    return this._createRouter(...args);
  }
}
