import ViewModule, {
	state,
	action,
	computed
} from '../../lib/baseViewModule';

export default class Todos extends ViewModule {
	@state tasks = [];

	constructor(sdk) {
		super();
		this.sdk = sdk;
	}

	async add(text) {
		try {
			const resp = await this.sdk.addTask(text);
			this.addTaskAction(resp);
		} catch (e) {
			alert(e.message);
			console.log(e);
		}
	}

	@action
	addTaskAction(data, state){
		state.tasks.push(data);
	}

	getViewProps() {
		return {
			addTodo: text => this.add(text),
			todos: this.state.tasks
		};
	}
}