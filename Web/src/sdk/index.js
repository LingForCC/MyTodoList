import 'whatwg-fetch';

export default class MindmapTodoSDK {
	
	constructor(baseUrl) {

		this.baseUrl = baseUrl + "/api";
       
	}
		
	async addTask(name) {
		try {
			const response = await fetch(this.baseUrl + '/task', {
				method: "POST",
				body: JSON.stringify(
					{
						name: name
					}),
				headers: {
					"Content-Type": "application/json"
				}
			});
			return response.text();
		} catch (e) {
			throw e;
		}
	}

}
