import { Injectable, Inject } from '@angular/core'
import { HttpClient, HttpHeaders } from '@angular/common/http'
import 'rxjs/add/operator/toPromise'

@Injectable()
export class AppHttpService {
    private _headers: HttpHeaders

    constructor(
        private _http: HttpClient) {
        this._onInit()
    }

    private _extractData(response) {
        return response || {}
    }

    private _handleError(error) {
        return Promise.reject(error.error && error.error.message)
    }

    private _onInit() {
        this._headers = new HttpHeaders({
            'Content-Type': 'application/json'
        })
    }

    get(route) {
        return this._http.get(route, { headers: this._headers })
            .toPromise()
            .then(this._extractData)
            .catch(this._handleError)
    }

    put(route, message) {
        return this._http.put(route, message, { headers: this._headers })
            .toPromise()
            .then(this._extractData)
            .catch(this._handleError)
    }

    post(route, message) {
        return this._http.post(route, message, { headers: this._headers })
            .toPromise()
            .then(this._extractData)
            .catch(this._handleError)
    }

    delete(route, message) {
        return this._http.delete(route, { headers: this._headers })
            .toPromise()
            .then(this._extractData)
            .catch(this._handleError)
    }
}
