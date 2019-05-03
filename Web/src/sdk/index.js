import 'whatwg-fetch';

export default class MindmapTodoSDK {
	
	constructor(baseUrl) {

		this.baseUrl = baseUrl + "/api";
       
	}
		
	async addTask(name) {
		try {
			await fetch(this.baseUrl + '/task', {
				method: "POST",
				body: JSON.stringify(
					{
						name: name
					}),
				headers: {
					"Content-Type": "application/json"
				}
			});
		} catch (e) {
			throw e;
		}
	}

}
