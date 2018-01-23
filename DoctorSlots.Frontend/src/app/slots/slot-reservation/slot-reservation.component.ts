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

	@ViewChild("form") form

	slotReservation: TakeSlot = {
		facilityId: "",
		start: new Date(),
		end: new Date(),
		comments: "",
		patient: {
			name: "",
			secondName: "",
			email: "",
			phone: ""
		}
	}

	displayStartDate: string
	displayEndDate: string

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
				let startParam = new Date(params['start'])
				let endParam = new Date(params['end'])

				this.slotReservation.facilityId = params['facilityId']
				this.slotReservation.start = startParam
				this.slotReservation.end = endParam

				this.displayStartDate = this.getUtcDate(startParam).toLocaleString()
				this.displayEndDate = this.getUtcDate(endParam).toLocaleString()
			})
	}

	onSubmit() {
		this.submitted = true
		this.success = false
		if (this.form.valid) {
			this.apiError = null
			this._api.postSlotReservation(this.slotReservation).then(() => {
				this.success = true
			}).catch(err => this.apiError = err)
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
