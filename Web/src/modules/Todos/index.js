import ViewModule, { state, action, computed } from '../../lib/baseViewModule';
import 'whatwg-fetch';

export default class Todos extends ViewModule {
  	@state todos = [];

  	@action
  	add(text, state) {
    	this.getRemoteData();
  	}

  	async getRemoteData() {
    	const response = await fetch("https://localhost:5001/api/task/5");
    	const statusText = response.statusText;
    	this.state.todos.push({
      		statusText
    	})
  	}

	getViewProps() {
		return {
		addTodo: text => this.add(text),
		};
	}
}
