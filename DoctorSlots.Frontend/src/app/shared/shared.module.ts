import { CommonModule } from '@angular/common'
import { RouterModule } from '@angular/router'
import { HttpModule } from '@angular/http'
import { NgModule } from '@angular/core'
import { HttpClientModule, HttpClient } from '@angular/common/http'

import { TranslateModule } from '@ngx-translate/core'

import { AppNavbarComponent } from './components/app-navbar/app-navbar.component'
import { SlotsApiService } from './services/slots-api.service'
import { AppHttpService } from './services/app-http.service'

@NgModule({
    imports: [ 
        RouterModule,
        CommonModule,
        HttpModule,
        TranslateModule,
        HttpClientModule
    ],
    providers: [
        AppHttpService,
        SlotsApiService
    ],
    declarations: [
        AppNavbarComponent
    ],
    exports: [
        AppNavbarComponent
    ]
})

export class SharedModule { }
