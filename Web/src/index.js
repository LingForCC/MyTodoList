import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { PersistGate } from 'redux-persist/integration/react';
import Portal from './modules/Portal';
import Todos from './modules/Todos';
import Navigation from './modules/Navigation';
import MainView from './MainView';
import TodosView from './components/TodosPanel';
import { ModuleProvider } from './lib/moduleContext';
import MindmapTodoSDK from './sdk';

const sdk = new MindmapTodoSDK('{baseUrl}');
const todos = new Todos(sdk);
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

portal.persistor.purge();

ReactDOM.render(
  <Provider store={portal.store}>
    <PersistGate loading={null} 
      onBeforeLift={() => todos.init()} 
      persistor={portal.persistor}>
      <ModuleProvider module={portal}>
        <App />
      </ModuleProvider>
    </PersistGate>
  </Provider>,
  document.getElementById('root')
);


