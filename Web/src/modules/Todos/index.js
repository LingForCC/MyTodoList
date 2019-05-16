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

	async init(){
		const tasks = await this.sdk.getAllTasks();
		console.log(tasks);
		this.addTaskAction(tasks);
	}

	async add(text) {
		try {
			const task = await this.sdk.addTask(text);
			this.addTaskAction([task]);
		} catch (e) {
			alert(e.message);
			console.log(e);
		}
	}

	@action
	addTaskAction(data, state){
		console.log(data);
		state.tasks.push(...data);
	}

	getViewProps() {
		return {
			addTodo: text => this.add(text),
			todos: this.state.tasks
		};
	}
}