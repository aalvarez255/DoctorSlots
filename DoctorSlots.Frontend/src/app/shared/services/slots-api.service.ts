import { AppHttpService } from './app-http.service'
import { Injectable, Inject } from '@angular/core'
import { BehaviorSubject, Observable } from 'rxjs'
import { Http, Headers, RequestOptions } from '@angular/http'
import { isPlatformBrowser } from '@angular/common'
import { TakeSlot } from '../models/TakeSlot'

@Injectable()
export class SlotsApiService {
    private _rootUrl = ''
    private _apiUrl = path => this._rootUrl + path
    private _send = (verb: string, path: string, data?: Object) =>
        this._http[verb.toLowerCase()](this._apiUrl(path), data)

    constructor(
        private _http: AppHttpService) {
        this._rootUrl = '/api'
    }

    getSlots(date: Date) {
        return this._send('get', '/slots/' + date.toJSON())
    }

    postSlotReservation(slotReservation: TakeSlot) {
        return this._send('post', '/slotReservation', slotReservation)
    }
}
