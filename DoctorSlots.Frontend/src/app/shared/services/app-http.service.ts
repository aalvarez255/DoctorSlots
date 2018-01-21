import { Injectable, Inject } from '@angular/core'
import { Http, Response, Headers, RequestOptions } from '@angular/http'
import 'rxjs/add/operator/toPromise'

@Injectable()
export class AppHttpService {
	private _headers: Headers
	private _options: RequestOptions

	constructor(
		private _http: Http) 
	{
		this._onInit()
	}

	private _extractData(response) {
		if (response.status < 200 || response.status >= 300) {
			throw new Error('Bad response status: ' + response.status)
		}
		return response.json() || {}
	}

	private _handleError(error) {
		let errMsg = "API error -> " + error
		console.error(errMsg)
		return Promise.reject(errMsg)
	}

	private _onInit() {
		this._headers = new Headers({ 
			'Content-Type': 'application/json'
		})
		this._options = new RequestOptions({ headers: this._headers })
	}

	get(route) {
		return this._http.get(route, this._options)
			.toPromise()
			.then(this._extractData)
			.catch(this._handleError)
	}

	put(route, message) {
		return this._http.put(route, message, this._options)
			.toPromise()
			.then(this._extractData)
			.catch(this._handleError)
	}

	post(route, message) {
		return this._http.post(route, message, this._options)
			.toPromise()
			.then(this._extractData)
			.catch(this._handleError)
	}

	delete(route, message) {
		let options = Object.assign({}, this._options)
		options.body = message
		return this._http.delete(route, options)
			.toPromise()
			.then(this._extractData)
			.catch(this._handleError)
	}
}