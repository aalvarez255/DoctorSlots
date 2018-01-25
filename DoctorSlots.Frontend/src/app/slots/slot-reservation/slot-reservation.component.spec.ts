import { async, ComponentFixture, TestBed } from '@angular/core/testing'
import { SlotReservationComponent } from './slot-reservation.component'
import { TranslateModule, TranslateLoader } from '@ngx-translate/core'
import { Observable } from 'rxjs/Observable'
import 'rxjs/add/observable/of'
import { FormsModule } from '@angular/forms'
import { RouterTestingModule } from '@angular/router/testing'
import { FacilitySlots } from '../../shared/models/FacilitySlots'
import { SlotsApiService } from '../../shared/services/slots-api.service'
import { TakeSlot } from '../../shared/models/TakeSlot'
import { Routes, Router } from '@angular/router'

class FakeLoader implements TranslateLoader {
    getTranslation(lang: string): Observable<any> {
        return Observable.of({})
    }
}

class MockSlotsApiService {
    public postSlotReservation(slotReservation: TakeSlot) {
        return Promise.resolve({})
    }
}
  
describe('SlotReservationComponent', () => {
    let component: SlotReservationComponent
    let fixture: ComponentFixture<SlotReservationComponent>
    let router: Router

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [SlotReservationComponent],
            imports: [
                FormsModule,
                RouterTestingModule.withRoutes([]),
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
        fixture = TestBed.createComponent(SlotReservationComponent)
        component = fixture.componentInstance
        fixture.detectChanges()

        router = TestBed.get(Router)
        router.initialNavigation()
    })

    it('should create', () => {
        expect(component).toBeTruthy()
    })
})
