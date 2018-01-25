import { async, ComponentFixture, TestBed } from '@angular/core/testing'
import { SlotsCalendarComponent } from './slots-calendar.component'
import { TranslateModule, TranslateLoader } from '@ngx-translate/core'
import { Observable } from 'rxjs/Observable'
import 'rxjs/add/observable/of'
import { BlockUIModule } from 'ng-block-ui'
import { SlotsApiService } from '../../shared/services/slots-api.service'
import { AppHttpService } from '../../shared/services/app-http.service'
import { HttpClient } from '@angular/common/http'
import { FacilitySlots } from '../../shared/models/FacilitySlots'
import { Slot } from '../../shared/models/Slot'
import { RouterTestingModule } from '@angular/router/testing'

class FakeLoader implements TranslateLoader {
    getTranslation(lang: string): Observable<any> {
        return Observable.of({})
    }
}

class MockSlotsApiService {
    facilitySlots : FacilitySlots = {
        facilityId: "1",
        slots: [
            {
                start: new Date(),
                end: new Date()
            }
        ]
    }

    public getSlots(date: Date) {
        return Promise.resolve(this.facilitySlots)
    }
}

describe('SlotsCalendarComponent', () => {
    let component: SlotsCalendarComponent
    let fixture: ComponentFixture<SlotsCalendarComponent>

    beforeEach(async(() => {

        TestBed.configureTestingModule({
            declarations: [SlotsCalendarComponent],
            imports: [
                RouterTestingModule.withRoutes([]),
                BlockUIModule,
                TranslateModule.forRoot({
                    loader: { provide: TranslateLoader, useClass: FakeLoader },
                })
            ],
            providers: [
                { provide: SlotsApiService, useClass: MockSlotsApiService }
            ]
        })
            .compileComponents()
    }))

    beforeEach(() => {
        fixture = TestBed.createComponent(SlotsCalendarComponent)
        component = fixture.componentInstance
        fixture.detectChanges()
    })

    it('should create', () => {
        expect(component).toBeTruthy()
    })

    it('should initialize fullcalendar', () => {
        expect(fixture.nativeElement.querySelector("#calendar").childNodes.length).toBeGreaterThan(0)
    })
})
