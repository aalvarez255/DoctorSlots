import { RouterModule, Routes } from '@angular/router'
import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'

import { TranslateModule } from '@ngx-translate/core'
import { SlotsComponent } from './slots.component';


export const moduleRoutes: Routes = [
    { path: '', component: SlotsComponent }
]

@NgModule({
    imports: [
         CommonModule,
        TranslateModule,
        RouterModule.forChild(moduleRoutes)
    ],
    declarations:
    [
        SlotsComponent
    ]
})

export class SlotsModule { }