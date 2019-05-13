import 'whatwg-fetch';
import { Exception } from 'handlebars';

export default class MindmapTodoSDK {

	constructor(baseUrl) {

		this.baseUrl = baseUrl + "/api";

	}

	async addTask(name) {
		try {
			const response = await fetch(this.baseUrl + '/task', {
				method: "POST",
				body: JSON.stringify({
					name
				}),
				headers: {
					"Content-Type": "application/json"
				}
			});
			var resp = await response.json();
			if(response.status === 200) {
				return resp;
			}
			else {
				throw new Exception(resp.message);
			}
		} catch (e) {
			throw e;
		}
	}

}