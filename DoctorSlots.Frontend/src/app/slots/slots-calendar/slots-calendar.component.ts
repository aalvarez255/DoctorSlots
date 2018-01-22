import { Component, OnInit, ViewChild } from '@angular/core'
import { TranslateService, LangChangeEvent } from '@ngx-translate/core'
import { SlotsApiService } from '../../shared/services/slots-api.service'
import { Slot } from '../../shared/models/Slot'

import { BlockUI, NgBlockUI } from 'ng-block-ui'

declare var $: any

@Component({
	selector: 'slots-calendar',
	templateUrl: './slots-calendar.component.html',
	styleUrls: ['./slots-calendar.component.less']
})

export class SlotsCalendarComponent implements OnInit {

	@BlockUI() blockUI: NgBlockUI

	private _loadingText: string

	constructor(
		private _translateService: TranslateService,
		private _api: SlotsApiService
	) {
		_translateService.get('loading').subscribe((res: string) => {
			this._loadingText = res
		})

		_translateService.onLangChange.subscribe((event: LangChangeEvent) => $("#calendar").fullCalendar('option', 'locale', event.lang))
	}

	ngOnInit() {
		this.initCalendar()
	}

	private initCalendar(): void {
		let today = new Date()

		$('#calendar').fullCalendar({
			allDaySlot: false,
			defaultDate: today,
			locale: "en",
			defaultView: "agendaWeek",
			eventLimit: false,
			firstDay: 1,
			height: "auto",
			contentHeight: "auto",
			events: (start, end, timezone, callback) => {
				this._api.getSlots(start).then(slots => {
					//limit min and max hours in calendar
					$('#calendar').fullCalendar('option', 'minTime', this.getMinMaxHourAsString(slots, true))
					$('#calendar').fullCalendar('option', 'maxTime', this.getMinMaxHourAsString(slots, false))

					callback(slots)
				})
			},
			eventColor: '#378006',
			loading: (isLoading, view) => {
				if (isLoading) this.blockUI.start(this._loadingText)
				else this.blockUI.stop()
			},
			viewRender: (currentView) => {
				/* disable past dates */
				if (today >= currentView.start && today <= currentView.end) {
					$(".fc-prev-button").prop('disabled', true)
					$(".fc-prev-button").addClass('fc-state-disabled')
				}
				else {
					$(".fc-prev-button").removeClass('fc-state-disabled')
					$(".fc-prev-button").prop('disabled', false)
				}
			}
		})

		$('#calendar').fullCalendar('gotoDate', today)
	}

	private getMinMaxHourAsString(slots: Slot[], isMin: boolean): string {
		let hours = slots.map(slot => new Date(isMin ? slot.start : slot.end).getHours())

		let minMaxHour = isMin ?
			hours.reduce((a, b) => a < b ? a : b) :
			hours.reduce((a, b) => a > b ? a : b)

		return minMaxHour < 10 ? "0" + minMaxHour + ":00:00" : minMaxHour + ":00:00"
	}
}
