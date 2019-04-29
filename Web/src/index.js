import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import Portal from './modules/Portal';
import Todos from './modules/Todos';
import Navigation from './modules/Navigation';
import MainView from './MainView';
import TodosView from './components/TodosPanel';
import { ModuleProvider } from './lib/moduleContext';

const todos = new Todos();
const navigation = new Navigation();
const portal = Portal.create({
  modules: {
    todos,
    navigation,
  },
  main: MainView,
  components: {
    Home: {
      screen: TodosView,
      path: '',
      module: todos,
    }
  }
});
const App = portal.createApp();

ReactDOM.render(
  <Provider store={portal.store}>
    <ModuleProvider module={portal}>
      <App />
    </ModuleProvider>
  </Provider>,
  document.getElementById('root')
);
