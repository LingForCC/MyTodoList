import ViewModule, { state, action, computed } from '../../lib/baseViewModule';

export default class Todos extends ViewModule {
  @state todos = [];

  @action
  add(text, state) {
    state.todos.push({
      text
    })
  }

  getViewProps() {
    return {
      addTodo: text => this.add(text),
    };
  }
}
