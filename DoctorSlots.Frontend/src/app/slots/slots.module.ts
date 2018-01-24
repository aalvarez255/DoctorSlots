import { RouterModule, Routes } from '@angular/router'
import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { FormsModule } from '@angular/forms'

import { BlockUIModule } from 'ng-block-ui'
import { TranslateModule } from '@ngx-translate/core'

import { SlotsComponent } from './slots.component'
import { SlotsCalendarComponent } from './slots-calendar/slots-calendar.component';
import { SlotReservationComponent } from './slot-reservation/slot-reservation.component'

export const moduleRoutes: Routes = [
    { path: '', component: SlotsCalendarComponent },
    { path: 'reservation', component: SlotReservationComponent }
]

@NgModule({
    imports: [
        CommonModule,
        TranslateModule,
        RouterModule.forChild(moduleRoutes),
        BlockUIModule,
        FormsModule
    ],
    declarations: [
        SlotsComponent,
        SlotsCalendarComponent,
        SlotReservationComponent
    ]
})

export class SlotsModule { }
