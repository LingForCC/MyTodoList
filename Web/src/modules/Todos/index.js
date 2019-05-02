import ViewModule, { state, action, computed } from '../../lib/baseViewModule';

export default class Todos extends ViewModule {
  	@state todos = [];

		constructor(sdk) {
			super();
			this.sdk = sdk;
		}
		
  	@action
  	add(text, state) {
    	this.addTaskRemote(text);
  	}

  	async addTaskRemote(text) {
			try{
				const resp = await this.sdk.addTask(text);
			} catch(e) {
				alert(e);
			}
  	}

	getViewProps() {
		return {
		addTodo: text => this.add(text),
		};
	}
}
