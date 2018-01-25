import { Component, OnInit, ViewChild } from '@angular/core'
import { TranslateService, LangChangeEvent } from '@ngx-translate/core'
import { SlotsApiService } from '../../shared/services/slots-api.service'
import { Slot } from '../../shared/models/Slot'

import { BlockUI, NgBlockUI } from 'ng-block-ui'
import { Router } from '@angular/router'

declare var $: any

@Component({
    selector: 'slots-calendar',
    templateUrl: './slots-calendar.component.html',
    styleUrls: ['./slots-calendar.component.less']
})

export class SlotsCalendarComponent implements OnInit {

    @BlockUI() blockUI: NgBlockUI
    errorApi: string

    private _facilityId: string
    private _loadingText: string

    constructor(
        private _translateService: TranslateService,
        private _api: SlotsApiService,
        private _router: Router
    ) {
        _translateService.get('loading').subscribe((res: string) => {
            this._loadingText = res
        })

        _translateService.onLangChange.subscribe((event: LangChangeEvent) => $('#calendar').fullCalendar('option', 'locale', event.lang))
    }

    ngOnInit() {
        this.initCalendar()
    }

    private initCalendar(): void {
        const today = new Date()

        $('#calendar').fullCalendar({
            slotLabelFormat: 'HH:mm',
            timeFormat: 'HH:mm',
            allDaySlot: false,
            defaultDate: today,
            locale: 'en',
            defaultView: 'agendaWeek',
            eventLimit: false,
            firstDay: 1,
            height: 'auto',
            contentHeight: 'auto',
            timezone: 'auto',
            events: (start, end, timezone, callback) => {
                let requestDate = new Date()
                if (start.toDate() > requestDate) {
                    requestDate = start.toDate()
                }
                this._api.getSlots(requestDate).then(response => {
                    // limit min and max hours in calendar
                    this._facilityId = response['facilityId']

                    const slots = response['slots']
                    $('#calendar').fullCalendar('option', 'minTime', this.getMinMaxHourAsString(slots, true))
                    $('#calendar').fullCalendar('option', 'maxTime', this.getMinMaxHourAsString(slots, false))

                    callback(slots)
                }).catch(error => {
                    this.blockUI.stop()
                    this.errorApi = error
                })
            },
            eventClick: (calEvent, jsEvent, view) => {
                const startDate = new Date(calEvent.start)
                const endDate = new Date(calEvent.end)
                this._router.navigate(['/reservation'], {
                    queryParams: {
                        start: startDate.toJSON(),
                        end: endDate.toJSON(),
                        facilityId: this._facilityId
                    }
                })
            },
            eventColor: '#378006',
            loading: (isLoading, view) => {
                if (isLoading) {
                    this.blockUI.start(this._loadingText)
                } else {
                    this.blockUI.stop()
                }
            },
            viewRender: (currentView) => {
                /* disable past dates */
                if (today >= currentView.start && today <= currentView.end) {
                    $('.fc-prev-button').prop('disabled', true)
                    $('.fc-prev-button').addClass('fc-state-disabled')
                } else {
                    $('.fc-prev-button').removeClass('fc-state-disabled')
                    $('.fc-prev-button').prop('disabled', false)
                }
            }
        })

        $('#calendar').fullCalendar('gotoDate', today)
    }

    private getMinMaxHourAsString(slots: Slot[], isMin: boolean): string {
        const hours = slots.map(s => new Date(isMin ? s.start : s.end).getHours())

        const minMaxHour = isMin ?
            hours.reduce((a, b) => a < b ? a : b) :
            hours.reduce((a, b) => a > b ? a : b)

        return minMaxHour < 10 ? '0' + minMaxHour + ':00:00' : minMaxHour + ':00:00'
    }
}
