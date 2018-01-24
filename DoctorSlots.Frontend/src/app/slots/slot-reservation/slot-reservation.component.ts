import { Component, OnInit, ViewChild } from '@angular/core'
import { Router, ActivatedRoute } from '@angular/router'
import { TakeSlot } from '../../shared/models/TakeSlot'
import { Slot } from '../../shared/models/Slot'
import { SlotsApiService } from '../../shared/services/slots-api.service';

declare var moment: any

@Component({
    selector: 'app-slot-reservation',
    templateUrl: './slot-reservation.component.html',
    styleUrls: ['./slot-reservation.component.less']
})
export class SlotReservationComponent implements OnInit {

    @ViewChild('form') form

    slotReservation: TakeSlot = {
        facilityId: '',
        start: '',
        end: '',
        comments: '',
        patient: {
            name: '',
            secondName: '',
            email: '',
            phone: ''
        }
    }

    apiError: string
    submitted: boolean
    success: boolean

    constructor(
        private _route: ActivatedRoute,
        private _api: SlotsApiService) {
    }

    ngOnInit() {
        this._route
            .queryParams
            .subscribe(params => {
                let startParam = moment(params['start'])
                let endParam = moment(params['end'])                

                this.slotReservation.facilityId = params['facilityId']
                this.slotReservation.start = startParam.format('DD/MM/YYYY HH:mm:ss')
                this.slotReservation.end = endParam.format('DD/MM/YYYY HH:mm:ss')
            })
    }

    onSubmit() {
        this.submitted = true
        this.success = false
        if (this.form.valid) {
            this.apiError = null
            this._api.postSlotReservation(this.slotReservation).then((data) => {
                console.log('success')
                this.success = true
            }).catch(err => {
                this.apiError = err
                this.success = false
            })
        }
    }

    private getUtcDate(date: Date) {
        return new Date(
            date.getUTCFullYear(),
            date.getUTCMonth(),
            date.getUTCDate(),
            date.getUTCHours(),
            date.getUTCMinutes(),
            date.getUTCSeconds())
    }
}
