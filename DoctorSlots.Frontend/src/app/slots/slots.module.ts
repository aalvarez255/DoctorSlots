import { RouterModule, Routes } from '@angular/router'
import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'

import { BlockUIModule } from 'ng-block-ui'
import { TranslateModule } from '@ngx-translate/core'

import { SlotsComponent } from './slots.component'
import { SlotsCalendarComponent } from './slots-calendar/slots-calendar.component'

export const moduleRoutes: Routes = [
    { path: '', component: SlotsComponent }
]

@NgModule({
    imports: [
        CommonModule,
        TranslateModule,
        RouterModule.forChild(moduleRoutes),
        BlockUIModule
    ],
    declarations:
    [
        SlotsComponent,
        SlotsCalendarComponent
    ]
})

export class SlotsModule { }